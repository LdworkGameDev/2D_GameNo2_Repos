using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : AttackController
{
    private EnemyStatsController statsController;

    protected override void Start()
    {
        base.Start();
        statsController = GetComponent<EnemyStatsController>();
    }

    public override void OnAttacked(GameObject attacker, Attack attack)
    {
        base.OnAttacked(attacker, attack);
        statsController.DescreaseHealth(attack.damage);
    }
}
