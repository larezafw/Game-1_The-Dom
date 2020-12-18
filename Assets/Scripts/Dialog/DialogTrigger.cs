using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogTrigger : MonoBehaviour
{
    public ObjectDialog dialog1;
    public ObjectDialog dialog2;
    [SerializeField] Transform noah;
    public Transform noahPos;
    public bool first;

    void Start()
    {
        GameObject.FindObjectOfType<AudioManager>().Play("CutsceneTheme");
    }
    void Update()
    {
        if (first && transform.position.x == noah.transform.position.x)
        {
            first = false;

           DialogManager dialogManager= GameObject.FindObjectOfType<DialogManager>();
            dialogManager.StartDialog(dialog1);
            dialogManager.triggerCutsceneA = true;

        }

        if(noah.position.x == noahPos.position.x)
        {
            SceneManager.LoadScene(2);
        }
        
    }
}
