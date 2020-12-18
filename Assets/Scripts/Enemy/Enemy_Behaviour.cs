using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Behaviour : MonoBehaviour
{
    #region Public Variable
    public float attackDistance;
    public float moveSpeed;
    public float timer;
    Transform leftPatrol;
    Transform rightPatrol;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange;
    public EnemyHit hitpoint;
    public bool ableToMove;
    public SpriteRenderer playerSR;

    public Transform theCollider;
    public Rigidbody2D rb;

    public HotZone hotZone;

    #endregion

    #region Private Variable
    [HideInInspector] public Animator anim;
    float distance;
    bool AttackMode;
   
    bool cooling;
    float intTime;
    bool itIsDying;

    bool cutSCene;
    Color iceColor;
    public bool freeze;
    #endregion




    void Awake()
    {
        leftPatrol = GameObject.FindGameObjectWithTag("LeftLimit").GetComponent<Transform>();
        rightPatrol = GameObject.FindGameObjectWithTag("RightLimit").GetComponent<Transform>();
        SelectTarget();
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("IsDie", false);
        ableToMove = true;
        iceColor = new Color(8f, 255f, 255f, 255f);
    }
    void Update()
    {
        if (target == null)
        {
            SelectTarget();
            inRange = false;

            hotZone.newMechan();

        }

        if (cutSCene)
        {
            ableToMove = false;
        }
     

        if (hitpoint.currentHP() <= 0)
        {
            freeze = false;
            itIsDying = true;
            inRange = false;

            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack"))
            {
                anim.SetBool("canWalk", false);
                anim.SetBool("Attack", false);
                anim.SetBool("IsDie", true);
            }
        }


        #region Move Logic
       
        
        if (!AttackMode&&!itIsDying&&ableToMove&&!cutSCene)
        {
            Move();
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack")&&!freeze)
            {
                flip();
            }
            
            
        }

        if (!insideOfLimit()&&!inRange&& !anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack"))
        {
            SelectTarget();
        }

        if (inRange&& !cutSCene)
        {
            EnemyLogic();
        }

        if (inRange == false)
        {
           
            StopAttack();
        }
        #endregion


    }

    #region Move Method
    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);
        if (distance > attackDistance || !ableToMove)
        {
            
            StopAttack();
        }

        else if(distance<=attackDistance && cooling == false&&ableToMove)
        {
            Attack();
          
        }

        if (cooling)
        {
            anim.SetBool("Attack", false);
            cooldown();
        }
    }

    void Move()
    {
        anim.SetBool("canWalk", true);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x,transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void cooldown()
    {
        timer -= Time.deltaTime;

        if(timer<=0 && cooling && AttackMode)
        {
            cooling = false;
            timer = intTime;
        }
    }
    void Attack()
    {
        timer = intTime;
        AttackMode = true;

        anim.SetBool("canWalk", false);
        anim.SetBool("Attack", true);

    }

    void StopAttack()
    {
        cooling = false;
        AttackMode = false;
        anim.SetBool("Attack", false);
    }
  

    public void TriggerCooling()
    {
        cooling = true;

    }

    private bool insideOfLimit()
    {
        return transform.position.x > leftPatrol.position.x && transform.position.x < rightPatrol.position.x;
    }

    public void SelectTarget()
    {
        float leftDistance = Vector2.Distance(transform.position, leftPatrol.position);
        float rightDistance = Vector2.Distance(transform.position, rightPatrol.position);

        if (leftDistance > rightDistance)
        {
            target = leftPatrol;
        }

        else
        {
            target = rightPatrol;
        }

       
    }

    public void flip()
    {

        if (transform.position.x > target.position.x)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

   
    }

    #endregion

    public void removeCOmponent()
    {
        rb.gravityScale = 0f;
        Destroy(theCollider.gameObject);

    }
   public void Dying()
    {
        Destroy(gameObject);
    }

    public void Hitted()
    {
        anim.SetBool("canWalk", false);
        anim.SetBool("Hitted", true);
        ableToMove = false;
       
        
    }
    public void canMove()
    {
        if (!freeze)
        {
            ableToMove = true;
        }

        anim.SetBool("Hitted", false);
    }

    public void freezing()
    {
        freeze = true;
        anim.SetBool("canWalk", false);
        anim.SetBool("FreezeOut", true);   
    
        ableToMove = false;
        
    }

    public void EndFreeze()
    {
        freeze = false;
        anim.SetBool("FreezeOut", false);
        ableToMove = true;
    }

    public void endCutscene()
    {
        cutSCene = false;
        ableToMove = true;
    }
    public void stopMovement()
    {
        cutSCene = true;
        anim.SetBool("canWalk", false);
        anim.SetBool("Attack", false);
    }
}
