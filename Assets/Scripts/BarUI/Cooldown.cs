using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    Color color;
    Color OnCooldownColor;
    public Slider slider;
    public Image fill;

    void Start()
    {
        color = new Color(255f, 255f,255f, 255f);
        OnCooldownColor= new Color(100f, 100f, 100f, 255f);
    }


    void Update()
    {
        if (slider.value > 0)
        {
            slider.value -= Time.deltaTime;
        }
    }
    public void setMaxTime(float cooldown)
    {
        slider.maxValue = cooldown;
        slider.value = 0;
    }

    public void setTime(float cooldown)
    {
        Color32 coloring = new Color32(170, 170, 170, 255);
        slider.value = cooldown;

        fill.color = coloring;
        
    }

    public void EndCooldown()
    {
        if (slider.value <= 0)
        {
            fill.color = color;
        }

    }


}
