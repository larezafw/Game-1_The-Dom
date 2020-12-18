using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearAttack : MonoBehaviour
{
   [SerializeField] private float speed ;
    [SerializeField] float increseSpd = 7f;
    float speedStop = 3f;
    public Rigidbody2D rb;
    bool slowing;
    float stop = 0f;
   


    void start()
    {
        speed = 4f;
    }
    void Update()
    {
       
        rb.velocity = transform.right * speed;
       
    }

    public void increse()
    {
        speed = increseSpd;
    }
    public void slower()
    {
        speed = speedStop;
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void moreSlowing()
    {
        speed = stop;
    }

    public void stopSound()
    {
        GameObject.FindObjectOfType<AudioManager>().Stop("SkillO");
    }

    public void skillKsound()
    {
        GameObject.FindObjectOfType<AudioManager>().Play("SkillK");
    }
}
