using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    

    #region Movement
    private float speed;
    public bool IsGrounded;
    private Rigidbody2D rb;
    public Transform feetPost;
    public float checkRadius;
    public LayerMask whatIsGround;
    public float jumpForce;
    private float jumpTimeCounter;
    public float jumpTime;
    public bool isJumping;
    public bool ableToMove;
    private bool skilling;
    #endregion

    public WinLoas loseCOndition;

    #region Etc.
    public Animator anim;

    public bool isDashing;
    public float dashTime;
    public float dsatTImeWhileJump;
    public float dashSpeed;
    public float distanceBetweenImage;
    public float startDashCooldown;

    private float dashTImeLeft;
    private float lastImageXpos;
    private float dashCooldown;
    private float facing;
    public bool canDash;

    public BoxCollider2D playercol;
    public SkillCaster skill;
    private bool spearAtk;
    private bool BubbleAtk;
    private bool firePilAtk;

    bool buffing;
    public Transform buffUI;
    public bool buffEffect;
    float buffTime;
    float startBuffTime = 8;

    public Cooldown dashingCdUI;

    public bool Cutscene;
    #endregion

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        Cutscene = false;
        ableToMove = true;
        rb = GetComponent<Rigidbody2D>();
        canDash = true;
       
        dashCooldown = 0;
        isDashing = false;

        dashingCdUI.setMaxTime(startDashCooldown);
    }
    void Update()
    {
        

        if (buffTime > 0)
        {
            buffTime -= Time.deltaTime;
        }
        else if (buffTime <= 0)
        {
            buffUI.gameObject.SetActive(false);
            buffEffect = false;
        }

 

        checkDashing();

        if (dashCooldown > 0)
        {
            dashCooldown -= Time.deltaTime;
        }

        speed = 2.7f;
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            if (ableToMove&&!Cutscene)
            {
                moveX = -1f;

                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (ableToMove && !Cutscene)
            {
                moveX = +1f;

                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
               
            }
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (dashCooldown <= 0 && canDash && ableToMove && !skilling && !Cutscene && !anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Idle"))
            {
                AttempToDash();
                GameObject.FindObjectOfType<AudioManager>().Play("SkillM");
            }
        }

        Vector3 move = new Vector3(moveX, moveY).normalized;
     
        Vector3 targetMovePosition = transform.position + move * speed * Time.deltaTime;
        transform.position = targetMovePosition;

        IsGrounded = Physics2D.OverlapCircle(feetPost.position, checkRadius, whatIsGround);

        if (IsGrounded && moveX == 0)
        {
            anim.SetBool("Move", false);
            anim.SetBool("Jump", false);
        }
        else if(IsGrounded&& moveX != 0)
        {
            anim.SetBool("Move", true);
            anim.SetBool("Jump", false) ;
        }
        
        if (IsGrounded == true && ableToMove == true && !Cutscene && Input.GetKeyDown(KeyCode.W))
        {
         
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;

           
        }
         
        if(Input.GetKey(KeyCode.W)&& isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }

        }

        if (isJumping)
        {
            anim.SetBool("Move", false);
            anim.SetBool("Jump", true);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            isJumping = false;
        }
    }

    #region animation Method
    public void forLaser()
    {
        ableToMove = false;
        skilling = true;
        anim.SetBool("Move", false);
        anim.SetBool("Laser", true);
       
    }
    

    public void canMove()
    {
        spearAtk = false;
        BubbleAtk = false;
        firePilAtk = false;
        buffing = false;

        anim.SetBool("Laser", false);
        anim.SetBool("GeneralAttack", false);
        ableToMove = true;
        skilling = false;
    }


    public void forSpear()
    {
        spearAtk = true;
        ableToMove = false;
        skilling = true;
        anim.SetBool("GeneralAttack", true);
        anim.SetBool("Move", false);
    }

    public void forBubble()
    {
        BubbleAtk = true;
        ableToMove = false;
        skilling = true;
        anim.SetBool("GeneralAttack", true);
        anim.SetBool("Move", false);
    }

    public void forFirePil()
    {
        firePilAtk = true;
        ableToMove = false;
        skilling = true;
        anim.SetBool("GeneralAttack", true);
        anim.SetBool("Move", false);
    }

    public void forBuff()
    {
        buffing = true;
        ableToMove = false;
        skilling = true;
        anim.SetBool("GeneralAttack", true);
        anim.SetBool("Move", false);
    }

    public void spearSpawn()
    {
        GameObject.FindObjectOfType<AudioManager>().Play("CastSkill");
        if (spearAtk)
        {
            skill.CastSpear();
            
        }

        else if (BubbleAtk)
        {
            skill.castSlowerBox();
            GameObject.FindObjectOfType<AudioManager>().Play("SkillI");
        }

        else if (firePilAtk)
        {
            skill.castFirePillar();
            GameObject.FindObjectOfType<AudioManager>().Play("SkillO");
        }

        else if (buffing)
        {
            
            buffEffect = true;
            buffUI.gameObject.SetActive(true);
            buffTime = startBuffTime;
        }
    }
    #endregion
    #region Dashing Method
    void checkDashing()
    {
        if (isDashing)
        {
            if (dashTImeLeft > 0)
            {
                

                    if (!IsGrounded || isJumping)
                    {
                        ableToMove = false;
                        rb.velocity = transform.right * (dashSpeed + 2);
                        rb.gravityScale = 0;
                         playercol.isTrigger=true;
                    }

                    else if (IsGrounded)
                    {
                        ableToMove = true;
                        rb.velocity = transform.right * dashSpeed;
                        rb.gravityScale = 0;
                    playercol.isTrigger=true;
                    }


                    dashTImeLeft -= Time.deltaTime;

                    if (Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImage)
                    {
                        PlayerAfterImagePool.Instance.GetFromPool();
                        lastImageXpos = transform.position.x;
                    }
                
            }
        }

        else if (!isDashing)
        {
            dashTImeLeft = 0;
        }

        if (dashTImeLeft <= 0)
        {
            playercol.isTrigger=false;
            isDashing = false;
            rb.gravityScale = 3;

            if (!skilling &&IsGrounded)
            {
                ableToMove = true;
            }
        }
    }
    void AttempToDash()
    {
        isDashing = true;

        if (IsGrounded)
        {
            dashTImeLeft = dashTime;
        }
        else if (!IsGrounded || isJumping)
        {
            dashTImeLeft = dsatTImeWhileJump;
        }
        dashCooldown = startDashCooldown;
        dashingCdUI.setTime(startDashCooldown);
        
        PlayerAfterImagePool.Instance.GetFromPool();
        lastImageXpos = transform.position.x;
    }

    public void isCurscene()
    {
       
        Cutscene = true;
        anim.SetBool("Move", false);
        anim.SetBool("Jump", false);
        anim.SetBool("GeneralAttack", false);
        anim.SetBool("Laser", false);
    }

    public void endCutscene()
    {
        Cutscene = false;
        ableToMove = true;
    }

    public void Death()
    {
        anim.SetBool("GeneralAttack", false);
        anim.SetBool("Laser", false);
        anim.SetBool("Move", false);
        anim.SetBool("Death", true);
        Cutscene = true;

        Boss_Behaviour bos = GameObject.FindObjectOfType<Boss_Behaviour>();
        if (bos != null)
        {
            bos.cutscene = true;
        }

        Enemy_Behaviour[] skeleton; 
        skeleton = GameObject.FindObjectsOfType<Enemy_Behaviour>();

        if (skeleton != null)
        {
            foreach (Enemy_Behaviour skel in skeleton)
            {
                skel.stopMovement();
            }
        }
    }

    public void laserSound()
    {
        GameObject.FindObjectOfType<AudioManager>().Play("SkillL");
    }
    public void losingDisplay()
    {
        loseCOndition.lose();
    }
    #endregion
}
