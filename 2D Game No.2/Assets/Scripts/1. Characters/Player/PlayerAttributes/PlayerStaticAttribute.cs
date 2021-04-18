using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStaticAttribute
{
    public PlayerStatsController statsController;
    public StaticAttributeType staticType;
    public ModifiableValue value;

    public void CreateAttribute(PlayerStatsController statsController_, int baseValue)
    {
        statsController = statsController_;
        value = new ModifiableValue(baseValue);
    }
}
