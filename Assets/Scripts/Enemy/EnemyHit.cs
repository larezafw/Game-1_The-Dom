using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{

    #region Health Variable

     private int currentHealth= 3000;
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
    [SerializeField] Enemy_Behaviour enemy;
    #endregion

    #region Effect
    bool Slowing;
   SkillCaster mpRegen;

  Transform player;
    #endregion

    void Awkae()
    {
        
    }
    void Start()
    {
        mpRegen = GameObject.FindObjectOfType<SkillCaster>();
        player = GameObject.FindGameObjectWithTag("ThePlayer").GetComponent<Transform>();
        
        damageScoring = GameObject.FindGameObjectWithTag("TotalDamage").GetComponent<TotalDamage>();
        currentHealth = maxHealt;
        healthBar.SetMaxHealth(maxHealt);
    }
    void Update()
    {
     

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
            if (enemy.freeze)
            {
                damage += (damage / 4);
            }
            bool crit = Random.Range(1, 100) <= mpRegen.critRate;
            if (crit)
            {
                damage += (damage / 2);
            }
            if (currentHealth > 0)
            {
                if (!enemy.anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack"))
                { enemy.freezing(); }
                if (!enemy.anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack") && !enemy.anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_OnHit") && !enemy.freeze)
                {
                    enemy.Hitted();
                }
                pfDamagePopUp.CreatePopUp(transform.position, damage, crit);
                damageScoring.totalDmgActive(damage);
            }

            takeDamage(damage);
        }

        if (trig.gameObject.tag == "SpearAtk")
        {
            int damage = Random.Range(100, 200);
            if (enemy.freeze)
            {
                damage += (damage / 4);
            }
            bool crit = Random.Range(1, 100) <= mpRegen.critRate;
            if (crit)
            {
                damage += (damage / 2);
            }

            if (currentHealth > 0)
            {

                if (!enemy.anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack") && !enemy.anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_OnHit") && !enemy.freeze)
                {
                    enemy.Hitted();
                }
                pfDamagePopUp.CreatePopUp(transform.position, damage, crit);
                damageScoring.totalDmgActive(damage);
            }

            takeDamage(damage);
            pfDamagePopUp.CreatePopUpMana(player.transform.position, 4,false);
            if (mpRegen.ShowMana() < 300)
            {
                mpRegen.manaRegenerating(4);
            }
        }

        if (trig.gameObject.tag == "NoahHit")
        {
            int damage = Random.Range(100,250);
            bool crit = Random.Range(1, 100) <= 20;
            if (crit) damage += (damage / 2);
            if (enemy.freeze) damage += (damage / 4);
               
            if (currentHealth > 0)
            {
                if (!enemy.anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack") && !enemy.anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_OnHit") && !enemy.freeze)
                {
                    enemy.Hitted();
                }
                pfDamagePopUp.CreatePopUp(transform.position, damage, crit);
              
            }
            takeDamage(damage);
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Laser")
        {
            if (waitLaserDmg <= 0)
            {
                int damage = Random.Range(800, 1100);
                if (enemy.freeze)
                {
                    damage += (damage / 4);
                }
                bool crit = Random.Range(1, 100) <= mpRegen.critRate;
                if (crit)
                {
                    damage += (damage / 2);
                }

                if (currentHealth > 0)
                {
                    if (!enemy.anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack") && !enemy.anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_OnHit")&&!enemy.freeze)
                    {
                        enemy.Hitted();
                    }
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
                if (enemy.freeze)
                {
                    damage += (damage/4);
                }
                bool crit = Random.Range(1, 60) <= mpRegen.critRate;
                if (crit)
                {
                    damage += damage;
                }

                if (currentHealth > 0)
                {
                    if (!enemy.anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack") && !enemy.anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_OnHit") && !enemy.freeze)
                    {
                        enemy.Hitted();
                    }
                    pfDamagePopUp.CreatePopUp(transform.position, damage, crit);
                    damageScoring.totalDmgActive(damage);
                }

                takeDamage(damage);
                waitFireDMG=startWaitFireDMG;
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

    public void makeDeath()
    {
        currentHealth = 0;
        healthBar.SetHealt(currentHealth);
    }
}
