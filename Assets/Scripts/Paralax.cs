using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    float lenght, startPostX;
    public GameObject cam;
    public float paralaxSpd;
    public bool looping;
    //cloud color ref A35E5B
    void Start()
    {
        startPostX = transform.position.x;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - paralaxSpd));
        float dist = (cam.transform.position.x * paralaxSpd);

        transform.position = new Vector3(startPostX + dist, transform.position.y, transform.position.z);

        if (looping && temp > startPostX + lenght)
        {
            startPostX += lenght;
        }

        else if( looping&& temp < startPostX - lenght)
        {
            startPostX -= lenght;
        }
     }
}
