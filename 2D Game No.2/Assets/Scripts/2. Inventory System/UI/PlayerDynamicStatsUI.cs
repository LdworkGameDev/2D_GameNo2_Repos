using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDynamicStatsUI : MonoBehaviour
{
    public HealthBarUI healthBar;
    public ManaBarUI manaBar;
    public CoinUI coinUI;

    public void UpdateDynamicAttributesUI(DynamicAttributeContainer container)
    {
        foreach(var attribute in container.dynamicAttributes)
        {
            if(attribute.dynamicType == DynamicAttributeType.Health)
            {
                healthBar.SetHealth(attribute.currentValue);
                healthBar.SetMaxHealth(attribute.maxValue);
            }

            if(attribute.dynamicType == DynamicAttributeType.Mana)
            {
                manaBar.SetCurrentMana(attribute.currentValue);
                manaBar.SetMaxMana(attribute.maxValue);
            }

            if(attribute.dynamicType == DynamicAttributeType.Coin)
            {
                coinUI.SetValue(attribute.currentValue);
            }
        }
    }
}
