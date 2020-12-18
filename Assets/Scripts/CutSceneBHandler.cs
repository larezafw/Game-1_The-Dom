using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneBHandler : MonoBehaviour
{
    public CameraFollow cam;

    public Transform bossPos;
    public Transform Boss;
    bool bossAppear=true;
    public Transform camshot;

    public Transform noahEnterHouse;
    public Transform noah;

    public ObjectDialog dialog1;
    public ObjectDialog dialog2;
    public ObjectDialog dialog3;
    public ObjectDialog dialog4;
    public ObjectDialog dialog5;

    DialogManager DM;

    public SpriteRenderer noahSR;

    public bool scene2;
    public bool scene3;
    public bool scene4;
    public bool scene5;


    public Transform endPos;
    Color32 color;

    void Start()
    {
        GameObject.FindObjectOfType<AudioManager>().Play("CutsceneTheme");
        cam.setup(() => transform.position);
        DM = GameObject.FindObjectOfType<DialogManager>();
        DM.StartDialog(dialog1);
        DM.chat1 = true;
        color = new Color32(255, 255, 255, 255);
    }

    void Update() {
        if (bossAppear&&Vector2.Distance(bossPos.position,camshot.position)<=1)
        {
            bossAppear = false;
            Boss.gameObject.SetActive(true);
        }

        if(scene2&& Vector2.Distance(transform.position, camshot.position) <= 1)
        {
            scene2 = false;
            DM.StartDialog(dialog2);
            DM.chat2 = true;
        }

        if (scene3&&noah.position.x==noahEnterHouse.position.x)
        {
            color.a -= 3;
            noahSR.color = color;
            if (color.a <= 0)
            {
                noah.gameObject.SetActive(false);
                scene3 = false;
                Animator playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
                playerAnim.SetBool("Skill", true);
            }
        }

        if (scene4 && Vector2.Distance(bossPos.position, camshot.position) <= 0.5f)
        {
            scene4 = false;
            GameObject.FindObjectOfType<BossCutscene>().makeTeleport();
        }
        if (scene5 && Vector2.Distance(endPos.position, camshot.position) <= 0.5f)
        {
            scene5 = false;
            Boss.gameObject.SetActive(true);
            GameObject.FindObjectOfType<BossCutscene>().AppearAgain();
            DM.StartDialog(dialog4);
            DM.chat4 = true;
        }
     }
    public void Hiya()
    {
        DM.StartDialog(dialog3);
        DM.chat3 = true;
    }

    public void camToBoss()
    {
        cam.setup(() => bossPos.position);
    }

    public void GotoEndPos()
    {
        cam.setup(() => endPos.position);
    }

    public void lastDialog()
    {
        DM.StartDialog(dialog5);
        DM.chat5 = true;
    }
}
