using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerDynamicAttribute
{
    public DynamicAttributeType dynamicType;
    public StaticAttributeType refToStaticType;
    public int currentValue;
    public int maxValue;

    public void UpdateAttribute(PlayerDynamicAttribute attribute)
    {
        dynamicType = attribute.dynamicType;
        refToStaticType = attribute.refToStaticType;
        currentValue = attribute.currentValue;
        maxValue = attribute.maxValue;
    }
}
