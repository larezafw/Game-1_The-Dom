using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitDash : MonoBehaviour
{
    public PlayerController player;
    void OnTriggerEnter2D(Collider2D col) {
    
    if(col.gameObject.tag == "Wall")
        {
            player.canDash = false;
            player.isDashing = false;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        player.canDash = true;
    }
}
