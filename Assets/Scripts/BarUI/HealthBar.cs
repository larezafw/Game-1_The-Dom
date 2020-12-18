using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public Noah_Controller noah;
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealt(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void die()
    {
        if (slider.value <= 0 )
        {
            gameObject.SetActive(false);
        }
    }

    public void Noahdie()
    {
        if (slider.value <= 0 && noah.FirstDie)
        {
            Destroy(gameObject);
        }
    }
}
