using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noah_HotZone : MonoBehaviour
{
    public BoxCollider2D hotZone;
    private float wait;
    private float startWait = 1;
    public Noah_Controller noah;

    public int GUESS;

    void Start()
    {
        wait = 0;
    }

    void Update()
    {
        if (wait > 0)
        {
            wait -= Time.deltaTime;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            GUESS += 1;
            noah.haveToMove = true;
            hotZone.size = new Vector2(2.5f, 1f);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            
                if (wait <= 0)
                {
                    GameObject[] gos;
                    gos = GameObject.FindGameObjectsWithTag("Enemy");
                    GameObject closest = null;
                    float distance = Mathf.Infinity;
                    Vector3 position = transform.position;
                    foreach(GameObject go in gos)
                    {
                    Vector3 diff = go.transform.position - position;
                    float curDistance = diff.sqrMagnitude;
                    if (curDistance < distance)
                         {
                        closest = go;
                        distance = curDistance;
                         }
                    }

                    noah.target = closest.transform;
                    noah.inRange = true;

                    if (!noah.anim.GetCurrentAnimatorStateInfo(0).IsName("Noah_Attack") && noah.ableToMove)
                    {
                        noah.flip();
                    }
                }
            

        
        }
    }

    void OnTriggerExit2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Enemy")
        {
            if (GUESS <= 1)
            {
               
               
                wait = startWait;
                hotZone.size = new Vector2(2f, 1f);
                
            }
            GUESS -= 1;
        }
    }
}
