using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCutscene : MonoBehaviour
{
    public Animator anim;
    bool dialogTrigger = true;
    public ObjectDialog dialog2;

    [SerializeField] Transform noah;
    [SerializeField] Transform endPos;

    public bool endCutscene1;

    void Update()
    {
        if (endCutscene1&&noah.position.x==endPos.position.x)
        {
            endCutscene1 = false;
            Debug.Log("Next Scene");
        }
    }
    public void goIdle()
    {
        anim.SetBool("Idle", true);
    }
    public void goDialog()
    {
        if (dialogTrigger)
        {
            dialogTrigger = false;
            DialogManager dialogManager = GameObject.FindObjectOfType<DialogManager>();
            dialogManager.StartDialog(dialog2);
            dialogManager.changeNameA = true;
        }
    }

   
}
