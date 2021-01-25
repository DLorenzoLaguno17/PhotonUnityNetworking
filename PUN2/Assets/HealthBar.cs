using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1.0f);
    }

    public void SetHealth(int health)
    {
        if (health > 0)
            slider.value = health;
        else
            slider.value = 0;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
