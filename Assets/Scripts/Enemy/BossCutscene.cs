using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCutscene : MonoBehaviour
{
    bool firstAppear = true;
    [SerializeField] CameraFollow cam;
    public Transform noah;
    Animator anim;
    public Transform endPos;
    
    void Start()
    {
        anim= GetComponent<Animator>();
    }

    public void backToNoah()
    {
        if (firstAppear)
        {
            firstAppear = false;
            cam.setup(() => noah.position);
            GameObject.FindObjectOfType<CutSceneBHandler>().scene2 = true;
            anim.SetBool("TrueIdle", true);
        }
    }

    public void makeTeleport()
    {
        anim.SetBool("TrueIdle", false);
        anim.SetBool("Teleport", true);
    }

    public void EndTeleport()
    {

        transform.position = new Vector2(endPos.position.x, transform.position.y);
        gameObject.SetActive(false);
        GameObject.FindObjectOfType<CutSceneBHandler>().scene5 = true;
        GameObject.FindObjectOfType<CutSceneBHandler>().GotoEndPos();
        anim.SetBool("Teleport", false);
    }

    public void AppearAgain()
    {
        anim.SetBool("Teleport", false);
        anim.SetBool("EndTeleport", true);
    }

    public void BackAgaiToIdle()
    {
        anim.SetBool("EndTeleport", false);
        
    }
}
