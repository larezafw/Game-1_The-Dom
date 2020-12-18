using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noah_Controller : MonoBehaviour
{
    #region Public Variable
    public float attackDistance;
    public float moveSpeed;
    public float timer;
  
    public Transform PatrolArea;
    public Transform PatrolArea2;
    public Transform TruePatrol;

    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange;
    public Noah_HitPoint hitpoint;
    public bool ableToMove;

    public PlayerController Mainplayer;

    public Noah_HotZone hotzone;
   
    #endregion


    #region Private Variable
    public Animator anim;
    float distance;
    bool AttackMode;
    bool cooling;
    float intTime=3;
    bool itIsDying;
    Color iceColor;
    public bool haveToMove;

    public GameHandler handler;
    bool dramaticRess;
    public Transform cam;

    public WinLoas loseCondition;

    public bool OnCutscene;
    #endregion

   public bool FirstDie;
    void Awake()
    {
        SelectTarget();
        ableToMove = true;
        haveToMove = false;
    }

    
    void Update()
    {
        if (target == null)
        {
            SelectTarget();
            inRange = false;
            haveToMove = false;
            hotzone.GUESS = 0;
        }

        if (dramaticRess && Vector2.Distance(transform.position, cam.position) < 2 && FirstDie)
        {
            anim.SetBool("FirstDeathA", true);
            dramaticRess = false;
        }
        #region Death Logic

        if (hitpoint.currentHP() <=0 && !FirstDie)
        {
            itIsDying = true;
            inRange = false;

            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Noah_Attact"))
            {
                handler.goToNoah();
                Enemy_Behaviour[] enemy;
                enemy = GameObject.FindObjectsOfType<Enemy_Behaviour>();
                
                foreach(Enemy_Behaviour en in enemy)
                {
                    en.stopMovement();
                }
                
                Mainplayer.isCurscene();

                ableToMove = false;
                anim.SetBool("Move", false);
                anim.SetBool("Attack", false);
                

                hitpoint.FullRecovery();
                FirstDie = true;
                dramaticRess = true;
                OnCutscene = true;
            }
        }
        if (hitpoint.currentHP() <= 0 && FirstDie)
        {
            
            itIsDying = true;
            inRange = false;

            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Noah_Attack"))
            {
                anim.SetBool("Move", false);
                anim.SetBool("Attack", false);
                anim.SetBool("Death", true);
            }
        }

        #endregion

        #region Move Logic



        if (!AttackMode && !itIsDying && ableToMove)
        {
           

            if (!Inlimit() || haveToMove)
            {
                Move();
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Noah_Attack"))
                {
                    if (!Inlimit())
                    {
                        flip();
                    }
                }
            }

            else if(Inlimit() && !haveToMove)
            {
                anim.SetBool("Move",false);
            }
        }


        if ( !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Noah_Attack"))
        {
            SelectTarget();
        }

        if (inRange)
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

        else if (distance <= attackDistance && cooling == false && ableToMove)
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
        
            anim.SetBool("Move", true);
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Noah_Attack"))
            {
                Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }
        
    }

    void cooldown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && cooling && AttackMode)
        {
            cooling = false;
            timer = intTime;
        }
    }
    void Attack()
    {
        timer = intTime;
        AttackMode = true;

        
        anim.SetBool("Move", false);
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

    public void SelectTarget()
    {
        target = TruePatrol;
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

    private bool Inlimit()
    {
        return transform.position.x == TruePatrol.position.x ;
    }
    #endregion

    public void nextFirstDeath()
    {
        
        anim.SetBool("FirstDeathA", false);
        anim.SetBool("FirstDeathA1", true);

        GameObject.FindObjectOfType<GameHandler>().noahFIrstDeath();
    }

    public void UpdateRess()
    {
        hitpoint.UpdateUIHP();
    }

    public void makeEnemyMove()
    {
        OnCutscene = false;
        anim.SetBool("FirstDeathB", false);

        Enemy_Behaviour[] enemy;
        enemy = GameObject.FindObjectsOfType<Enemy_Behaviour>();

        foreach (Enemy_Behaviour en in enemy)
        {
            en.endCutscene();
        }

        Mainplayer.endCutscene();

        handler.goToPlayer();

        ableToMove = true;
        itIsDying = false;
    }


    public void destroy()
    {
        Enemy_Behaviour[] enemy;
        enemy = GameObject.FindObjectsOfType<Enemy_Behaviour>();

        if (enemy != null)
        {
            foreach (Enemy_Behaviour en in enemy)
            {
                en.stopMovement();
            }
        }

        Mainplayer.isCurscene();

        loseCondition.lose();
        Destroy(gameObject);
    }

    public void FirstDeathB()
    {
        anim.SetBool("FirstDeathA1", false);
        anim.SetBool("FirstDeathB", true);
    }
}
