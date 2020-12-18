using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPortal : MonoBehaviour
{
    public GameObject TheEnemy;
    float maxTime = 10f;
   
    void Start()
    {
        Invoke("spawnEnemy", 5f);
    }

    void spawnEnemy()
    {
        GameObject enemy = Instantiate(TheEnemy);
        enemy.transform.position = transform.position;

        nextEnemy();
    }

    void nextEnemy()
    {
        float nextTime;
        nextTime = Random.Range(6f, maxTime);

        Invoke("spawnEnemy", nextTime);

    }

 
}
