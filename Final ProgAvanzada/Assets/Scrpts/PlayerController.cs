using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;


    [Header("Movimento")]
    public float moveSpeed;

    [Header("Salto")]
    private bool canDoubleJump;
    public float jumpForce;
    public float bounceForce;

    [Header("Componentes")]
    public Rigidbody2D theRB;

    [Header("Grounded")]
    private bool isGrounded;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    [Header("Animator")]
    public Animator anim;
    private SpriteRenderer theSR;

    public float knockbackLength, knockbackForce;
    private float knockBackCounter;

    public bool stopInput;
        

    private void Awake()
    {
        instance = this;
    }

        void Start()
    {
        anim = GetComponent<Animator>();
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!PauseMenu.instance.isPaused && !stopInput){
            if(knockBackCounter <= 0)   
        {
            theRB.velocity = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal") , theRB.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

        if(isGrounded)
        {
            canDoubleJump = true;
        }

        if(Input.GetButtonDown("Jump"))
        {
            if(isGrounded)
            {
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                AudioManager.instance.PlaySFX(10);
            }else
            {
                if(canDoubleJump)
                {
                    theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                    AudioManager.instance.PlaySFX(10);
                    canDoubleJump = false;
                }
            }
        }

        if(theRB.velocity.x < 0)    
        {
            theSR.flipX = false;             
        }else if(theRB.velocity.x > 0)  
        {
            theSR.flipX = true;             
        }

        }
        else
        {
            knockBackCounter -= Time.deltaTime;
            if(!theSR.flipX)
            {
                theRB.velocity = new Vector2(-knockbackForce, theRB.velocity.y);    
            }else    
            {
                theRB.velocity = new Vector2(knockbackForce, theRB.velocity.y);    
            }
        }
        }

        
        
        anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x));
        anim.SetBool("isGrounded", isGrounded);   
    }

    public void Knockback()
    {
        knockBackCounter = knockbackLength; 
        theRB.velocity = new Vector2(0f, knockbackForce);
    }

    public void Bounce()
    {
        theRB.velocity = new Vector2(theRB.velocity.x, bounceForce);
    }

}
