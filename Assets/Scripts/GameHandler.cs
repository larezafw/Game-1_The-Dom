using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public CameraFollow cameraFollow;
    public Transform player;
    public Transform portal1;
    public Transform portal2;
    public Transform noah;

    public ObjectDialog dialog;

    private void Start()
    {
        
        cameraFollow.setup(() => portal1.position);
    }
    
  

    public void goToPortal2()
    {
        
        cameraFollow.setup(() => portal2.position);
    }
    public void goToPlayer()
    {
        cameraFollow.setup(() => player.position);
    }

    public void goToNoah()
    {
        cameraFollow.setup(() => noah.position);
    }

    public void noahFIrstDeath()
    {
        GameObject.FindObjectOfType<DialogManager>().StartDialog(dialog);
        GameObject.FindObjectOfType<DialogManager>().noahChat = true;
    }
}
