using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCaster : MonoBehaviour
{
    //ref
    public Transform fireCasterPlaze;
    public GameObject player;
    [SerializeField] PlayerHit playerHit;


    //skill
    public GameObject SpearSkill;
    public GameObject firstDimension;
    public GameObject secondDimension;
    public GameObject slowerBox;
    public GameObject firePill;

    //skill Cooldown
    float SpearCD;
    private float startSpearCD = 1;

    private float dimensionCD;
    private float startDimensionCD = 60;

    private float slowerCD;
    private float startSlowerCD = 5;
    private float startSlowerCD2 = 7;

    private float laserCD;
    private float startLaserCD = 15;

    float firePillarCD;
    float startFirePillarCD = 5;

    float buffCD;
    float startBuffCD = 20;

    //etc
    private bool dimensioneed;

    public HealthBar ManaBar;
    [SerializeField] int manaPoint;
    int currentMana;
    float manaRegen;
    float waitManaRegen = 1;

    [HideInInspector] public int HPrecord;
    int MANArecord;

    public ManaPoinUI manaUI;
    [SerializeField] Testing ManaPopUP;

    public Cooldown sKillO_CdUI;
    public Cooldown skillU_CdUI;
    public Cooldown sKillK_CdUI;
    public Cooldown skillJ_CdUI;
    public Cooldown sKillI_CdUI;
    public Cooldown skillL_CdUI;

    #region Critical 
    [HideInInspector] public float critRate;
    private float startCritRate = 20;
    private float critBuff;

    #endregion

    /*
     Laser = 200;
     Buble = 100;
     Fire Pillar = 150;
     Teleport = 50;

    Basic Attack = +10;
    */
    void Awake()
    {
        critRate = startCritRate;
    }
    void Start()
    {
        
        critBuff = 40;

        manaRegen = 0;

        currentMana = manaPoint;
        ManaBar.SetMaxHealth(manaPoint);
        dimensioneed = false;

        manaUI.setupManaText(manaPoint);

        sKillO_CdUI.setMaxTime(startFirePillarCD);
        skillU_CdUI.setMaxTime(startBuffCD);
        sKillK_CdUI.setMaxTime(startSpearCD);
        skillJ_CdUI.setMaxTime(startDimensionCD);
        sKillI_CdUI.setMaxTime(startSlowerCD2);
        skillL_CdUI.setMaxTime(startLaserCD);
    }

    void Update()
    {
        PlayerController players = player.GetComponent<PlayerController>();

        if (players.buffEffect)
        {
            critRate = (startCritRate + critBuff);
        }
        else if (!players.buffEffect)
        {
            critRate = startCritRate;
        }

        if (manaRegen <= 0)
        {
            if (currentMana < 300)
            {
                if (players.buffEffect)
                {
                    ManaPopUP.CreatePopUpMana(player.transform.position, 20, true);
                    manaRegenerating(20);
                    manaRegen = waitManaRegen;
                }
                else
                {
                    manaRegenerating(2);
                    manaRegen = waitManaRegen;
                }
            }
        }

        else if (manaRegen > 0)
        {
            manaRegen -= Time.deltaTime;
        }

        //Skill CD
        if (SpearCD > 0)
        {
            SpearCD -= Time.deltaTime;
        }

        if (dimensionCD > 0)
        {
            dimensionCD -= Time.deltaTime;
        }

        if (slowerCD > 0)
        {
            slowerCD -= Time.deltaTime;
        }

        if (laserCD > 0)
        {
            laserCD -= Time.deltaTime;
        }

        if (firePillarCD > 0)
        {
            firePillarCD -= Time.deltaTime;
        }

        if (buffCD > 0)
        {
            buffCD -= Time.deltaTime;
        }

        #region Skill Input
        //skill Input
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (slowerCD <= 0 && players.ableToMove && !players.isDashing && !players.Cutscene && currentMana >= 100)
            {
                
                manaConsumption(100);

                if (!players.anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Jump"))
                {
                    players.forBubble();
                    slowerCD = startSlowerCD;
                    sKillI_CdUI.setTime(startSlowerCD);
                }

                else if (players.anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Jump"))
                {
                    castSlowerBox();
                    slowerCD = startSlowerCD2;
                    sKillI_CdUI.setTime(startSlowerCD2);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            if (firePillarCD <= 0 && players.ableToMove && !players.isDashing && !players.Cutscene && !players.anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Jump") && currentMana >= 150)
            {
                
                manaConsumption(150);

                players.forFirePil();
                firePillarCD = startFirePillarCD;
                sKillO_CdUI.setTime(startFirePillarCD);
            }
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            if (buffCD <= 0 && players.ableToMove && !players.isDashing && !players.Cutscene && !players.anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Jump") && currentMana >= 30)
            {
                manaConsumption(30);

                players.forBuff();
                buffCD = startBuffCD;
                skillU_CdUI.setTime(startBuffCD);
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            if (SpearCD <= 0 && players.ableToMove && !players.isDashing && currentMana >= 5 && !players.Cutscene)
            {
                
                manaConsumption(5);
                if (!players.anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Jump"))
                {
                    CastSpear();
                    players.forSpear();
                }
                else if (players.anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Jump"))
                {
                    CastSpear();
                }
                SpearCD = startSpearCD;
                sKillK_CdUI.setTime(startSpearCD);
            }
        }

        if (Input.GetKeyDown(KeyCode.J))
        {

            if (dimensionCD <= 0 && players.ableToMove && !players.Cutscene)
            {
                if (!dimensioneed&&currentMana>=50)
                {
                    manaConsumption(50);
                    firstD();
                    recordManaHP();
                }
                else if (dimensioneed)
                {
                    GameObject.FindObjectOfType<AudioManager>().Play("SkillJ");
                    activateRecord();

                    GameObject firstDm = GameObject.FindGameObjectWithTag("Dimension");
                    player.transform.position = firstDm.transform.position;
                    dimensioneed = false;
                    dimensionCD = startDimensionCD;
                    Destroy(firstDm.gameObject);
                    
                    skillJ_CdUI.setTime(startDimensionCD);
                }

            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            
            if (players.IsGrounded && laserCD <= 0 && !players.Cutscene && players.ableToMove && !players.isDashing && currentMana >= 200)
            {
                GameObject.FindObjectOfType<AudioManager>().Play("CastSkill");
                manaConsumption(200);

                players.forLaser();
                laserCD = startLaserCD;
                skillL_CdUI.setTime(startLaserCD);
            }
        }

        #endregion
    }

    #region Skill Method
    //firePillar
    public void castFirePillar()
    {
        GameObject fireP = (GameObject)Instantiate(firePill);
        fireP.transform.position = fireCasterPlaze.position;
        fireP.transform.rotation = fireCasterPlaze.rotation;
    }


    //Spear
    public void CastSpear()
    {
        GameObject spear = (GameObject)Instantiate(SpearSkill);
        spear.transform.position = transform.position;
        spear.transform.rotation = transform.rotation;


    }

    //Teleport
    void firstD()
    {
        GameObject firstDim = (GameObject)Instantiate(firstDimension);
        firstDim.transform.position = player.transform.position;
        dimensioneed = true;

    }

    //Slower
    public void castSlowerBox()
    {
        GameObject slower = (GameObject)Instantiate(slowerBox);
        slower.transform.position = transform.position;

    }


    public void manaConsumption(int mana)
    {
        currentMana -= mana;
        ManaBar.SetHealt(currentMana);
        manaUI.setupManaText(currentMana);
    }

    public void manaRegenerating(int mana)
    {
        currentMana += mana;
        ManaBar.SetHealt(currentMana);
        manaUI.setupManaText(currentMana);
    }

    public int ShowMana() {
        return currentMana;
    }
    #endregion

    void recordManaHP()
    {
        MANArecord = currentMana;
        HPrecord = playerHit.saveHP();
    }

    void activateRecord()
    {
        currentMana = MANArecord;
        ManaBar.SetHealt(currentMana);
        manaUI.setupManaText(currentMana);
        playerHit.changeUI();
    }

}