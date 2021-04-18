using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void SetMaxHealth(int amount)
    {
        slider.maxValue = amount;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetHealth(int amount)
    {
        slider.value = amount;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
