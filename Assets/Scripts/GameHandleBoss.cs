using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandleBoss : MonoBehaviour
{
    public CameraFollow cameraFollow;
    public Transform player;
    public Transform boss;
    void Start()
    {
        GameObject.FindObjectOfType<AudioManager>().Play("MissionAppear");
        GameObject.FindObjectOfType<AudioManager>().Play("BattleTheme");
        cameraFollow.setup(() => player.position);

    }

    // Update is called once per frame
    public void changeToBoss()
    {
        cameraFollow.setup(() => boss.position);
    }

    public void changeToPlayer()
    {
        cameraFollow.setup(() => player.position);
    }

}
