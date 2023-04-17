using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public CharacterStats stats;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    void Start()
    {
        if (stats == null)
        {
            SetMaxHealth(100);
            SetHealth(100);
            return;
        }
        SetMaxHealth(stats.maxHp);
        SetHealth(stats.currentHp);
    }

    void Update()
    {
        if (stats == null)
        {
            SetHealth(100);
            return;
        }
        SetHealth(stats.currentHp);
    }
}
