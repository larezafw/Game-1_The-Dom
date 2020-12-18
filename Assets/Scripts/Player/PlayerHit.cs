using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    #region Health
     int maxHealt = 3000;
     int currentHP;
    public HealthBar healthBar;
    bool isDeath;
 
    [SerializeField] Testing pfDamagePopUp;
    [SerializeField] SkillCaster skill;
    [SerializeField] PlayerController player;

    public WinLoas winLose;
    #endregion

    void Start()
    {
        currentHP = maxHealt;
        healthBar.SetMaxHealth(maxHealt);
    }

    void Update()
    {
        if (currentHP <= 0 && !isDeath)
        {
            isDeath = true;
            player.Death();
           
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Hit")
        {
            if (!player.isDashing)
            {
                int thedamage = Random.Range(250, 400);
                bool critHit = Random.Range(1, 100) <= 30;
                if (critHit)
                {
                    thedamage += (thedamage / 2);
                }
                TakeDamage(thedamage);

                pfDamagePopUp.CreatePopUp(transform.position, thedamage, critHit);

            }
        }

        if (col.gameObject.tag == "StunHit")
        {
           
                player.isCurscene();
                int thedamage = Random.Range(200, 300);
                bool critHit = Random.Range(1, 100) < 30;

                if (critHit) { thedamage += (thedamage / 2); }
                TakeDamage(thedamage);

                pfDamagePopUp.CreatePopUp(transform.position, thedamage, critHit);
            
        }

        if (col.gameObject.tag == "BossHit")
        {
            if (!player.isDashing)
            {
                int thedamage = Random.Range(400, 600);
                bool critHit = Random.Range(1, 100) < 70;

                if (critHit) { thedamage += (thedamage / 2); }
                TakeDamage(thedamage);

                pfDamagePopUp.CreatePopUp(transform.position, thedamage, critHit);
            }
        }
    }

    void TakeDamage(int damage)
    {
        currentHP -= damage;
        healthBar.SetHealt(currentHP);
    }

    public int saveHP()
    {
        return currentHP;
    }

    public void changeUI()
    {
        currentHP = skill.HPrecord;
        healthBar.SetHealt(currentHP);
    }
}
