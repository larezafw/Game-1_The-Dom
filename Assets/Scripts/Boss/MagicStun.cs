using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicStun : MonoBehaviour
{
    Boss_Behaviour boss;
    
    void Awake()
    {
        boss = GameObject.FindObjectOfType<Boss_Behaviour>();
    }
    public void next()
    {
        boss.FirstSkill();
        Destroy(gameObject);
    }
}
