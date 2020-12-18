using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCutsceneB : MonoBehaviour
{
    Animator playerAnim;
    public bool ForBossCamera;
    public Animator houseAnim;
    private float nextDial;
    private float startNextDial = 1f;
    bool DialogBool;

    void Start()
    {
        playerAnim = GetComponent<Animator>();
    }

    void Update()
    {
        if (DialogBool && nextDial > 0)
        {
            nextDial -= Time.deltaTime;
        }
        else if (DialogBool && nextDial <= 0)
        {
            DialogBool = false;
            GameObject.FindObjectOfType<CutSceneBHandler>().lastDialog();
        }
    }
    public void PlayerSkillinFreeze()
    {
        playerAnim.SetBool("Skill", false);
        playerAnim.SetBool("SkillFreeze", true);
        
        GameObject.FindObjectOfType<CutSceneBHandler>().Hiya();
        houseAnim.SetBool("DIsappear", true);
    }
    public void ChangeCamera()
    {
        if (ForBossCamera)
        {
            ForBossCamera = false;
            GameObject.FindObjectOfType<CutSceneBHandler>().camToBoss();
            GameObject.FindObjectOfType<CutSceneBHandler>().scene4 = true;
        }
    }

    public void BackToIdle()
    {
        playerAnim.SetBool("SkillFreeze", false);
        ForBossCamera = true;
    }
    public void flipping()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        nextDial = startNextDial;
        DialogBool = true;
    }
}
