using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TotalDamage : MonoBehaviour
{
    public Transform totalDmgInt;
    public Transform totalDmgText;
    [SerializeField] DamagePoint damagePoint;

    public float duration;   

    void Start()
    {
      
        totalDmgInt.gameObject.SetActive(false);
        totalDmgText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (duration > 0)
        {
            duration -= Time.deltaTime;
            totalDmgInt.gameObject.SetActive(true);
            totalDmgText.gameObject.SetActive(true);
        }
        else if (duration <= 0)
        {
            totalDmgInt.gameObject.SetActive(false);
            totalDmgText.gameObject.SetActive(false);
        }
    }
     public void totalDmgActive(int damage)
    {
        damagePoint.Setup(damage);
        duration = 3;
    }
}
