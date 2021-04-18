using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBarUI : MonoBehaviour
{
    [SerializeField] private Image fill;
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void SetMaxMana(int amount)
    {
        slider.maxValue = amount;
    }

    public void SetCurrentMana(int amount)
    {
        slider.value = amount;
    }
}
