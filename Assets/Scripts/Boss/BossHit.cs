using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHit : MonoBehaviour
{
    #region Health Variable

    private int currentHealth = 3000;
    [SerializeField] private int maxHealt;
    public HealthBar healthBar;
    #endregion


    #region Take Damage
    float waitLaserDmg;
    float startWaitLaserDmg = 0.1f;
    float waitFireDMG;
    float startWaitFireDMG = 0.2f;

    [SerializeField] Testing pfDamagePopUp;
    TotalDamage damageScoring;

    #endregion


    #region Effect
    bool Slowing;
    SkillCaster mpRegen;
    Transform player;
    bool freezing;
    float freezeTime;
    float startFreezeTime = 3;
    Color iceColor;
    Color trueColor;
    public SpriteRenderer SR;


    bool firstAnim;

    #endregion

    void Start()
    {
        
        firstAnim = true;
        trueColor = new Color(255f, 255f, 255f, 255f);
        iceColor = new Color(0f, 255f, 255f, 255f);
        player = GameObject.FindGameObjectWithTag("ThePlayer").GetComponent<Transform>();
        mpRegen = GameObject.FindGameObjectWithTag("SkillCaster").GetComponent<SkillCaster>();
        damageScoring = GameObject.FindGameObjectWithTag("TotalDamage").GetComponent<TotalDamage>();
        currentHealth = maxHealt;
        healthBar.SetMaxHealth(maxHealt);
    }
    void Update()
    {
       
        if (freezeTime > 0)
        {
            SR.color = iceColor;
            freezing = true;
            freezeTime -= Time.deltaTime;
        }
        else if (freezeTime <= 0)
        {

            freezing = false;
            SR.color = trueColor;
        }

        #region Damaging
        if (waitLaserDmg > 0)
        {
            waitLaserDmg -= Time.deltaTime;
        }

        if (waitFireDMG > 0)
        {
            waitFireDMG -= Time.deltaTime;
        }


        #endregion
    }


    void OnTriggerEnter2D(Collider2D trig)
    {


        if (trig.gameObject.tag == "BubleAttack")
        {
            int damage = Random.Range(700, 900);
            if (freezing)
            {
                damage += (damage / 2);
            }
            bool crit = Random.Range(1, 100) <= mpRegen.critRate;
            if (crit)
            {
                damage += (damage / 2);
            }
            if (currentHealth > 0)
            {
                freezeTime = startFreezeTime;

                pfDamagePopUp.CreatePopUp(transform.position, damage, crit);
                damageScoring.totalDmgActive(damage);
            }

            takeDamage(damage);
        }

        if (trig.gameObject.tag == "SpearAtk")
        {
            int damage = Random.Range(100, 200);
            if (freezing)
            {
                damage += (damage / 2);
            }
            bool crit = Random.Range(1, 100) <= mpRegen.critRate;
            if (crit)
            {
                damage += (damage / 2);
            }

            if (currentHealth > 0)
            {

                pfDamagePopUp.CreatePopUp(transform.position, damage, crit);
                damageScoring.totalDmgActive(damage);
                pfDamagePopUp.CreatePopUpMana(player.transform.position, 4, false);
            }

            takeDamage(damage);

            if (mpRegen.ShowMana() < 300)
            {
                mpRegen.manaRegenerating(4);
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Laser")
        {
            if (waitLaserDmg <= 0)
            {
                int damage = Random.Range(800, 1100);
                if (freezing)
                {
                    damage += (damage / 2);
                }
                bool crit = Random.Range(1, 100) <= mpRegen.critRate;
                if (crit)
                {
                    damage += (damage / 2);
                }

                if (currentHealth > 0)
                {
                    pfDamagePopUp.CreatePopUp(transform.position, damage, crit);
                    damageScoring.totalDmgActive(damage);
                }

                takeDamage(damage);
                waitLaserDmg = startWaitLaserDmg;
            }
        }

        if (col.gameObject.tag == "FirePil")
        {
            if (waitFireDMG <= 0)
            {
                int damage = Random.Range(400, 600);
                if (freezing)
                {
                    damage += (damage / 2);
                }
                bool crit = Random.Range(1, 60) <= mpRegen.critRate;
                if (crit)
                {
                    damage += damage;
                }

                if (currentHealth > 0)
                {
                    pfDamagePopUp.CreatePopUp(transform.position, damage, crit);
                    damageScoring.totalDmgActive(damage);
                }

                takeDamage(damage);
                waitFireDMG = startWaitFireDMG;
            }
        }

    }



    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealt(currentHealth);
    }

    public int currentHP()
    {
        return currentHealth;
    }

    public void destroy()
    {
        Destroy(gameObject);
    }
}
