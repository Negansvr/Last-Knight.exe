using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charactermove : MonoBehaviour
{
    // Start is called before the first frame update
    public float movespeed;
    private Animator anim;
    private Rigidbody2D rb2d;
    float moveHozrizontal;

    public bool facingRight;
    public float jumpforce;
    public bool isGrounded;
    public bool canDouleJump;

    PlayerCombat playercombat;

    public bool characterattack;

    public float charactertimer;


    void Start()
    {
        movespeed = 5;
        moveHozrizontal=Input.GetAxis("Horizontal");
        anim= GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        playercombat = GetComponent<PlayerCombat>();
        charactertimer=0.7f;
    }

    // Update is called once per frame
    void Update()
    {
        Charactermovement();
        characteranimation();
        CharacterAttack();
        CharacterRunAttack();
        CharacterJump();
        CharacterAttackSpacing();
    }

    void Charactermovement()
    {
        moveHozrizontal=Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(moveHozrizontal*movespeed,rb2d.velocity.y);
    }
    void characteranimation()
    {
        if (moveHozrizontal>0)
        {
            anim.SetBool("isRunning", true);
        }
        if(moveHozrizontal==0)
        {
            anim.SetBool("isRunning",false);
        }
        if  (moveHozrizontal<0)
        {
            anim.SetBool("isRunning", true);
        }
        if (facingRight==false && moveHozrizontal>0)
        {
            characterflip();
        }
        if (facingRight==true && moveHozrizontal<0)
        {
            characterflip();
        }
    }
    void characterflip()
    {
        facingRight=!facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x*=-1;
        transform.localScale=Scaler;
    }
    void CharacterAttack()
    {
        if (Input.GetKeyDown(KeyCode.E)&&moveHozrizontal==0)
        {
            
            if (characterattack)
            {
                anim.SetTrigger("isAttack");
                playercombat.DamageEnemy();
                characterattack=false;
            }
            
            FindObjectOfType<AudioManager>().Play("swordsound1");
        }
    }
    void CharacterRunAttack()
    {
        if (Input.GetKeyDown(KeyCode.E)&&moveHozrizontal>0||Input.GetKeyDown(KeyCode.E)&&moveHozrizontal<0)
        {
            
            if (characterattack)
            {
                anim.SetTrigger("isRunAttack");
                playercombat.DamageEnemy();
                characterattack=false;
            }
            FindObjectOfType<AudioManager>().Play("swordsound1");
        }
    }
    void CharacterJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("isJumping",true);

            if (isGrounded)
            {
                rb2d.velocity=Vector2.up*jumpforce;
                canDouleJump=true;
            }
            
            else if (canDouleJump)
            {
                jumpforce=jumpforce/2.5f;
                rb2d.velocity=Vector2.up*jumpforce;

                canDouleJump=false;
                jumpforce=jumpforce*2.5f;
            }
            
        }
    }


    void CharacterAttackSpacing()
    {
        if(characterattack==false)
        {
            charactertimer-= Time.deltaTime;
        }
        if (charactertimer<0)
        {
            charactertimer=0f;
        }
        if(charactertimer==0f)
        {
            characterattack=true;
            charactertimer=0.7f;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        anim.SetBool("isJumping",false);
        if (col.gameObject.tag=="Grounded")
        {
            isGrounded=true;
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        anim.SetBool("isJumping",false);
        if (col.gameObject.tag=="Grounded")
        {
            isGrounded=true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        anim.SetBool("isJumping",true);
        if (col.gameObject.tag=="Grounded")
        {
            isGrounded= false;
        }
    }
    
    
    
}
