using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noah_HitPoint : MonoBehaviour
{
    #region Health Variable

    private int currentHealth;
    [SerializeField] private int maxHealt;
    public HealthBar healthBar;
    public Noah_Controller noah;

    [SerializeField] Testing pfDamagePopUp;


    #endregion

    void Start()
    {
        currentHealth = maxHealt;
        healthBar.SetMaxHealth(maxHealt);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Hit")
        {
            int damage = Random.Range(350, 500);
            bool crit = Random.Range(1, 100) <= 20;
            if (noah.FirstDie)
            {
                damage -= (damage /4);
            }

            if (crit&&!noah.FirstDie)
            {
                damage += (damage / 2);
            }

            if (currentHealth > 0)
            {
                pfDamagePopUp.CreatePopUp(transform.position, damage, crit);

                takeDamage(damage);
            }
        }
    }

    void takeDamage(int damage)
    {
        currentHealth -= damage;
        if (!noah.OnCutscene)
        {
            healthBar.SetHealt(currentHealth);
        }
    }

    public int currentHP()
    {
        return currentHealth;
    }

    public void UpdateUIHP()
    {
        healthBar.SetHealt(currentHealth);
    }

    public void FullRecovery()
    {
        currentHealth = maxHealt;
    }
}
