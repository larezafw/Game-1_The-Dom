using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoas : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Win()
    {
        GameObject.FindObjectOfType<AudioManager>().Stop("BattleTheme");
        GameObject.FindObjectOfType<AudioManager>().Play("Win");
        gameObject.SetActive(true);
     
    }

    public void forscene3()
    {
        
        gameObject.SetActive(true);
        anim = GetComponent<Animator>();
        anim.SetBool("Scene3", true);
    }

    public void gotoscene3()
    {
        SceneManager.LoadScene(3);
    }
    public void lose()
    {
        GameObject.FindObjectOfType<AudioManager>().Stop("BattleTheme");
        GameObject.FindObjectOfType<AudioManager>().Play("Lose");
        gameObject.SetActive(true);
        anim = GetComponent<Animator>();
        anim.SetBool("Lose", true);
    }
}
