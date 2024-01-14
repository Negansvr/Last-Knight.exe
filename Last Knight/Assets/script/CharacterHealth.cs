using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    //health
    public int maxHealt=100;
    public int currentHealth;
    public HealthBar healthBar;

    //Enemy Spacing
    public bool enemyattack;

    public float enemytimer;

    public Animator anim;


    void Start()
    {
        currentHealth=maxHealt;
        enemytimer=1.5f;

        anim=GetComponent<Animator>();

    }
   
    //düşmanın zarar verme aralığı
    void EnemyAttackSpacing()
    {
        if (enemyattack==false)
        {
            enemytimer-=Time.deltaTime;
        
        }
        if (enemytimer<0)
        {
            enemytimer= 0f;
        }
        if (enemytimer==0f)
        {
            enemyattack=true;
            enemytimer=1.5f;
        }
    }
    
    //düşmanı kitlemek
    void CharacterDamage()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            enemyattack=false;
        }
    }



    //karakterin zarar görmesi
    public void TakeDamage(int damage)
    {
        if(enemyattack)
        {
            currentHealth-=20;
            enemyattack=false;
            anim.SetTrigger("isHurt");
        }

        healthBar.SetHealth (currentHealth);

        if(currentHealth<=0)
        {
            currentHealth=0;
            Die();
        }
    } 

    void Die()
    {
        anim.SetBool("isDead",true);

        GetComponent<Charactermove>().enabled=false;

        Destroy(gameObject,2f);
    }






    // Update is called once per frame
    void Update()
    {
        EnemyAttackSpacing();
        CharacterDamage();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            TakeDamage(20);
        }
    }
}
