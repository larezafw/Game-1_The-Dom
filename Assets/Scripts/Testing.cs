using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField]Transform pfDamagePopUp;

    public void CreatePopUp(Vector2 position, int damagePoint,bool crit)
    {
        Transform damagePopUpTransform = Instantiate(pfDamagePopUp, position, Quaternion.identity);
        DamagePopUp damagePop = damagePopUpTransform.GetComponent<DamagePopUp>();
        damagePop.Setup(damagePoint,crit);
    }

    public void CreatePopUpMana(Vector2 position, int ManaAmount,bool buff)
    {
        Transform damagePopUpTransform = Instantiate(pfDamagePopUp, position, Quaternion.identity);
        DamagePopUp damagePop = damagePopUpTransform.GetComponent<DamagePopUp>();
        damagePop.setupManaRegen(ManaAmount,buff);
    }

}
