using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Behaviour : MonoBehaviour
{
    public Transform player;
    
    Animator anim;
    float startWaitForSkill = 3;
    float waitForskill;
    [SerializeField] float idlingTime;
    [SerializeField] float startIdlingTime = 5;

    float moveSpeed = 1f;

    [SerializeField] float walkTime;
    float startWalkTime = 5;

    [SerializeField] bool IdleTrue;
    [SerializeField] bool moving;

    public PlayerController playerEfek;
    public Transform caster;
    public GameObject skull;
    public Transform playerBack;

    bool isAttacking;
    bool isDeath;

    public GameHandleBoss handler;

    public BossHit bossHP;
    public GameObject stunMagic;

    public Mission mission;
    public bool cutscene;
    public WinLoas winlose;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        mission.DisplayBossBattle();
        IdleTrue = true;
        idlingTime = startIdlingTime;
    
    }

    void Update()
    {
        if (bossHP.currentHP() <= 0)
        { 
            isDeath = true;
            if(!isAttacking &&!anim.GetCurrentAnimatorStateInfo(0).IsName("Boss_Attack1") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Boss_Attack2")&& !anim.GetCurrentAnimatorStateInfo(0).IsName("Boss_Attack3")&& !anim.GetCurrentAnimatorStateInfo(0).IsName("Boss_Death"))
            {
                anim.SetBool("Attack", false);
                anim.SetBool("Walk", false);
                anim.SetBool("Magic", false);
                anim.SetBool("Death", true);

                EnemyHit[] enemyskull;
                enemyskull = GameObject.FindObjectsOfType<EnemyHit>();
                if (enemyskull != null)
                {
                    foreach (EnemyHit en in enemyskull)
                    {
                        en.makeDeath();
                    }
                }
                handler.changeToBoss();
                playerEfek.isCurscene();
            }
        }

        if (IdleTrue && !isDeath && !cutscene)
        {
            IsIdle();
        }

        if (moving && !IdleTrue && !isDeath && !cutscene)
        {
            Walking();
        }
    }
    void IsIdle()
    {
        if (idlingTime > 0)
        {
            anim.SetBool("Attack", false);
            anim.SetBool("Walk", false);
            anim.SetBool("Magic", false);
            idlingTime -= Time.deltaTime;
        }
        else if (idlingTime <= 0)
        {
            IdleTrue = false;
            PrepareToAttack(); 
        }
    }


    void PrepareToAttack() 
    {
        if (Vector2.Distance(transform.position, player.position) <= 3)
        {
            TimeToAttack();
        }

        else if (Vector2.Distance(transform.position, player.position) > 3 && Vector2.Distance(transform.position, player.position) <=9 )
        {
            walkTime = startWalkTime;
            moving = true;
        }

        else if (Vector2.Distance(transform.position, player.position) > 9)
        {
            ThirdSkill();
        }
    }
    void TimeToAttack()
    {
        flip();
        int chooseSkill = Random.Range(1, 100);
        bool first = chooseSkill <= 35;
        bool second = chooseSkill > 35 && chooseSkill<=80;
        bool third = chooseSkill > 80;

        if (first)
        {
            FirstSkill();
        }
        else if (second)
        {
            SecondSkill();
        }

        else if (third)
        {
            ThirdSkill();
        }
    }

    void Walking()
    {
        if (Vector2.Distance(transform.position, player.position) <= 1)
        {
            TimeToAttack();
            moving = false;
        }
        else if (walkTime <= 0)
        {
            TimeToAttack();
            moving = false;
        }
        else
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Boss_Attack1") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Boss_Attack2"))
            {
                flip();
                anim.SetBool("Walk", true);
                Vector2 targetPosition = new Vector2(player.position.x, transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                walkTime -= Time.deltaTime;
            }
        }
    }

    public void flip()
    {

        if (transform.position.x > player.position.x)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    public void FirstSkill()
    {
        anim.SetBool("Walk", false);
        anim.SetBool("Attack", true);
    }

    public void SecondSkill()
    {
        anim.SetBool("Walk", false);
        anim.SetBool("Magic", true);
    }

    void ThirdSkill()
    {
        anim.SetBool("Walk", false);
        anim.SetBool("MagicStun", true);
        isAttacking = true;
    }

    public void setToIdle()
    {
        anim.SetBool("Walk", false);
        anim.SetBool("Magic", false);
        anim.SetBool("Attack", false);
        IdleTrue = true;
        moving = false;
        isAttacking = false;
        idlingTime = startIdlingTime;
    }

    public void callSkull()
    {
        GameObject skuller = Instantiate(skull);
        skuller.transform.position = caster.position;
    }

    public void callStunMagic()
    {
        GameObject stunner = Instantiate(stunMagic);
        stunner.transform.position = player.transform.position;
    }

    public void endMagicCasting()
    {
        anim.SetBool("MagicStun", false);
    }

    public void MakePlayerMove()
    {
        playerEfek.endCutscene();
    }
    public void TeleportToPlayer()
    {
        
        transform.position = new Vector2(playerBack.position.x,transform.position.y) ;
        flip();    
    }
    public void destroy()
    {
        playerEfek.endCutscene();
        handler.changeToPlayer();
        winlose.Win();
        Destroy(gameObject);
    }

    public void MissionSuccess()
    {
        mission.DisplayBossKilled();
    }
}
