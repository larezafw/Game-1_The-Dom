using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission : MonoBehaviour
{
    int portalkilled;
    Animator anim;
    public WinLoas winCondition;

    void Start()
    {
        portalkilled = 0;
        anim = GetComponent<Animator>();
            
    }

    public void DisplayMission()
    {
        gameObject.SetActive(true);
    }

    public void DisplayKilled()
    {
        portalkilled += 1;
        if (portalkilled == 1)
        {
            gameObject.SetActive(true);
            anim.SetBool("Kill1", true);
        }
        else if (portalkilled == 2)
        {
            gameObject.SetActive(true);
            anim.SetBool("Kill2", true);
            GameObject.FindObjectOfType<AudioManager>().Play("MissionClear");
        }
    }

    public void DisplayBossBattle()
    {
        gameObject.SetActive(true);
        anim.SetBool("BossKill1", true);
    }

    public void DisplayBossKilled()
    {
        GameObject.FindObjectOfType<AudioManager>().Play("MissionClear");
        gameObject.SetActive(true);
        anim.SetBool("BossKill2", true);
    }

    public void setToFalse()
    {
        gameObject.SetActive(false);
    }

    public void isWin()
    {
        
        winCondition.Win();
        gameObject.SetActive(false);
    }
    public void goToNext()
    {
        gameObject.SetActive(false);
        winCondition.forscene3();
    }
}
