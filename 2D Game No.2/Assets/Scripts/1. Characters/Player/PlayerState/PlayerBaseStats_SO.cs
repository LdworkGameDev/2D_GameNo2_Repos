using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stats", menuName = "Data/Player Base Stats")]
public class PlayerBaseStats_SO : ScriptableObject
{
    public BaseStats[] baseStats;
}

[System.Serializable]
public class BaseStats 
{
    public StaticAttributeType staticType;
    public int baseValue;
}
