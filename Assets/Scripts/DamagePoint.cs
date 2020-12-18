using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePoint : MonoBehaviour
{
    public TextMeshProUGUI textMesh;


    [SerializeField] TotalDamage total;
    private Color textColor;

    private int Point=0 ;

    void Update()
    {
        
        if (Point > 1000)
        {
            textColor = new Color32(255, 0, 6, 255);

            textMesh.color = textColor;
        }
        else {
           
            textColor = new Color32(255, 230, 0, 255);
            textMesh.color = textColor;
        }
    }

    void LateUpdate()
    {
      
        textMesh.SetText(Point.ToString());
      
        if (total.duration < 0)
        {
            Point = 0;
        }
    }
    public void Setup(int damageAmount)
    {
        Point += damageAmount;
        
    }
}
