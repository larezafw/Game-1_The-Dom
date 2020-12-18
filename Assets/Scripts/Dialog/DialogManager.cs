using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour
{
    Queue <string> sentences;
    [SerializeField] Text NameText;
    [SerializeField] Text dialogText; 
    public Animator anim;
    public bool triggerCutsceneA;
    public Transform player;
    public bool changeNameA;
    public Transform noahPos;
    public Noah_Controller noah;
    public CameraFollow cam;


    public Transform bossPos;

    public bool chat1;
    public bool chat2;
    public bool chat3;
    public bool chat4;
    public bool chat5;

    public bool noahChat;
    void Awake()
    {
        sentences = new Queue<string>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextSentence();
        }
    }
    public void StartDialog(ObjectDialog dialog)
    {
        anim.SetBool("isOpen", true);
        Debug.Log("Starting conversation with " + dialog.name);
        NameText.text = dialog.name;
        sentences.Clear();
        foreach(string sentence in dialog.sentence)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }
        if (changeNameA)
        {
            if (sentences.Count == 10)
            {
                NameText.text = "Noah";
            }
            else if (sentences.Count == 7)
            {
                NameText.text = "Escar";
            }
            else if (sentences.Count == 5)
            {
                NameText.text = "Noah";
            }
            else if (sentences.Count == 3)
            {
                NameText.text = "Escar";
            }
            else if (sentences.Count == 1)
            {
                NameText.text = "Noah";
            }
        }

        if (chat1)
        {
            if (sentences.Count == 1)
            {
                NameText.text = "Escar";
            }
        }

        if (chat2)
        {
            if (sentences.Count == 2)
            {
                NameText.text = "Noah";
            }

            if (sentences.Count == 1)
            {
                NameText.text = "Escar";
            }
        }
        if (chat5)
        {
            if (sentences.Count == 1)
            {
                NameText.text = "King";
            }
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(typeSentence(sentence));
       
    }

    IEnumerator typeSentence(string sentence)
    {
        dialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return null;
        }
    }
    public void EndDialog()
    {
        anim.SetBool("isOpen", false);
        Debug.Log("End of conversation");
    
        if(triggerCutsceneA)
        {
            triggerCutsceneA = false;
            player.gameObject.SetActive(true);
        }

        if (changeNameA)
        {
            changeNameA = false;
            noah.TruePatrol = noahPos;
            noah.SelectTarget();
        }

        if (chat1)
        {
            chat1 = false;
            cam.setup(() => bossPos.position);
        }
        if (chat2)
        {
            chat2 = false;
            noah.TruePatrol = noahPos;
            noah.SelectTarget();
            GameObject.FindObjectOfType<CutSceneBHandler>().scene3 = true;
        }

        if (chat3)
        {
            chat3 = false;
            GameObject.FindObjectOfType<PlayerCutsceneB>().BackToIdle();
        }

        if (chat4)
        {
            chat4 = false;
            GameObject.FindObjectOfType<PlayerCutsceneB>().flipping();
        }
        if (chat5)
        {
            chat5 = false;
            SceneManager.LoadScene(4);
           
        }

        if (noahChat)
        {
            noahChat = false;
            GameObject.FindObjectOfType<Noah_Controller>().FirstDeathB();
        }

    }


}
