using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public BasicStats stats;


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
            Debug.Log("Stats is null");
            gameObject.SetActive(false);
            return;
        }
        SetMaxHealth(stats.maxHp);
        SetHealth(stats.currentHp);
    }

    void Update()
    {
        if (stats == null)
        {
            Destroy(gameObject);
            return;
        }
        SetHealth(stats.currentHp);
    }
}
