using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDefinition_SO : ScriptableObject
{
    public float cooldown = 0f;
    public Vector2 plusDamage;
    [Range(0f, 1f)] public float criticalChange = 0f;
    public float criticalMultiplier = 0f;
    public Vector2 hitBackForce;

    public Attack PlayerCreateAttack(PlayerStatsController attackerStats, EnemyStatsController defenderStats, Vector2 hitBackForce)
    {
        float coreDamage = attackerStats.GetAttackStat();
        coreDamage += Random.Range(plusDamage.x, plusDamage.y);

        bool isCritical = Random.value < criticalChange;
        if (isCritical)
        {
            coreDamage += criticalMultiplier;
        }

        return new Attack((int)coreDamage, isCritical, hitBackForce);
    }

    public Attack EnemyCreateAttack(EnemyStatsController attackerStats, PlayerStatsController defenderStats, Vector2 hitBackForce)
    {
        float coreDamage = attackerStats.GetCurrentDamage();
        coreDamage += Random.Range(plusDamage.x, plusDamage.y);
        coreDamage -= defenderStats.GetDefenceStat();

        bool isCritical = Random.value < criticalChange;
        if (isCritical)
        {
            coreDamage += criticalMultiplier;
        }

        return new Attack((int)coreDamage, isCritical, hitBackForce);
    }
}
