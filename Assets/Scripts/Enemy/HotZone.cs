using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotZone : MonoBehaviour
{

    public Enemy_Behaviour enemy;
    private float wait;
    private float startWait=1;
    private BoxCollider2D colz;
    public int guess;
    [SerializeField] bool onattackNoah;
    [SerializeField] bool onattackEscar;
    
    void Awake()
    {
        colz = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
     
        guess = 0;
        wait = 0;
        
    }
    void Update()
    {
        if (wait > 0)
        {
            wait -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D zol)
    {
        if (zol.gameObject.tag == "Player")
        {
            guess += 1;

            if (!onattackNoah)
            {
               
                {
                    colz.size = new Vector2(2f, 1.5f);
                    onattackEscar = true;
                }
            }
        }

        if (zol.gameObject.tag == "Noah")
        {
            guess += 1;
           
            if (!onattackEscar)
            {
                colz.size = new Vector2(2f, 1.5f);
                onattackNoah = true;
            }
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Noah"  )
        {
            if (!onattackNoah && !onattackEscar)
            {
                onattackNoah=true;
            }

            if (wait <= 0 && onattackNoah)
            {
                enemy.target = col.transform;
                enemy.inRange = true;

                if (! enemy.anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack")&&enemy.ableToMove)
                {
                   enemy.flip();
                }
            }
        }

        else if(col.gameObject.tag == "Player")
        {
            if (!onattackNoah && !onattackEscar)
            {
                onattackEscar = true;
            }
            if (wait <= 0 && onattackEscar)
            {
                enemy.target = col.transform;
                enemy.inRange = true;

                if (!enemy.anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack") && enemy.ableToMove)
                {
                    enemy.flip();
                }
            }
        }

       
    }


    void OnTriggerExit2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Noah"  )
        {
            onattackNoah = false;
            if (guess <= 1)
            {
                enemy.SelectTarget();
                enemy.inRange = false;
                wait = startWait;
                colz.size = new Vector2(1.3f, 1.5f);
            }
            guess -= 1;
        }

        if (trig.gameObject.tag == "Player")
        {
            onattackEscar = false;
            if (guess <= 1)
            {
                enemy.SelectTarget();
                enemy.inRange = false;
                wait = startWait;
                colz.size = new Vector2(1.3f, 1.5f);
            }
            guess -= 1;
        }
    }

    public void decreaseGuess()
    {
        guess -= 1;
    }

    public void newMechan()
    {
        onattackEscar = false;
        onattackNoah = false;
        wait = startWait;
        colz.size = new Vector2(1.3f, 1.5f);
        guess = 0;
    }
}
