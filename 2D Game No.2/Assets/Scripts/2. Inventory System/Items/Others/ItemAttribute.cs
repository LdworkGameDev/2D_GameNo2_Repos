using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemAttribute : IModifier
{
    public StaticAttributeType attributeType;
    public int minValue;
    public int maxValue;
    public int value;

    public ItemAttribute(StaticAttributeType attributeType_, int min, int max)
    {
        attributeType = attributeType_;
        minValue = min;
        maxValue = max;
        value = Random.Range(min, max);
    }

    public void AddValue(ref int baseValue)
    {
        baseValue += value;
    }
}
