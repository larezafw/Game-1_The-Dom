using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamagePopUp : MonoBehaviour
{
    private TextMeshPro textMesh;
    private float timer;
    private Color textColor;

    public Transform critSpace;
    public TextMeshPro critText;
    private Color critColor;
    void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    void Start()
    {

        timer = 0.6f;
       
    }
    public void Setup(int damageAmount,bool crit) 
        {
        textMesh.SetText(damageAmount.ToString());
        if (crit)
        {
            critSpace.gameObject.SetActive(true);
            textMesh.fontSize = 5;
            textColor = new Color32(255, 0, 6, 255);
            critColor = new Color32(255, 255, 255, 255);
        }
        else
        {
            critSpace.gameObject.SetActive(false);
            textMesh.fontSize = 4;
            textColor = new Color32(255, 230, 0, 255);
            critColor = new Color32(255, 255, 255, 255);
        }

        textMesh.color=textColor;
        critText.color = critColor;
        }


    public void setupManaRegen(int manaAMount,bool buff)
    {
        critSpace.gameObject.SetActive(false);

        if (buff)
        {
            textMesh.fontSize = 3;
        }
        else if (!buff)
        {
            textMesh.fontSize = 2;
        }
        textColor = new Color32(0, 255, 255, 255);
        textMesh.SetText("MP " + manaAMount.ToString());
        textMesh.color = textColor;
    }
    

    
    void Update()
    {
        

        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            float IncreaseScale = 1f;
            transform.localScale += Vector3.one * IncreaseScale * Time.deltaTime;
        }

        if (timer <= 0f)
        {
            critColor = new Color32(255, 255, 255, 255);


            float disappear = 2f;
            textColor.a -= disappear * Time.deltaTime;
            textMesh.color = textColor;

            
            critColor.a -= disappear * Time.deltaTime;
            critText.color = critColor;


            float IncreaseScale = 1f;
            transform.localScale -= Vector3.one * IncreaseScale * Time.deltaTime;

            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }

        float speed = 1.5f;
        transform.position += new Vector3(0, speed) * Time.deltaTime;
    }
}
