using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stats", menuName = "Data/Enemy Stats")]
public class EnemyStats_SO : ScriptableObject
{
    public int maxHealth = 0;
    public int currentHealth = 0;
   
    public int baseDamage = 0;
    public int currentDamage = 0;

    public int baseDef = 0;
    public int currentDef = 0;

    public float currentCriticalChange = 0f;
    public float baseCriticalChange = 0f;
}

