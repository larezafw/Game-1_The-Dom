using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Transform creditGrub;
    public Transform mainMenuGrub;

    bool ready;
    float goingToPlay;
    float startGoingToPlay = 1f;
    void Start()
    {
        GameObject.FindObjectOfType<AudioManager>().Play("MainMenuTheme");
    }

    void Update()
    {
        if (ready)
        {
            if (goingToPlay > 0)
            {
                goingToPlay -= Time.deltaTime;
            }
            else if (goingToPlay <= 0)
            {
                SceneManager.LoadScene(1);
            }
        }
    }
    public void goToCredits()
    {
        GameObject.FindObjectOfType<AudioManager>().Play("ButtonClick");
        creditGrub.gameObject.SetActive(true);
        mainMenuGrub.gameObject.SetActive(false);
    }

    public void BackToMainMenu()
    {
        GameObject.FindObjectOfType<AudioManager>().Play("ButtonClick");
        mainMenuGrub.gameObject.SetActive(true);
        creditGrub.gameObject.SetActive(false);
    }

    public void playTheGame()
    {
        if (!ready)
        {
            GameObject.FindObjectOfType<AudioManager>().Play("ButtonClick");
            ready = true;
            goingToPlay = startGoingToPlay;
        }
    }

    public void quitGame()
    {
        GameObject.FindObjectOfType<AudioManager>().Play("ButtonClick");
        Debug.Log("Quit");
        Application.Quit();
    }

}
