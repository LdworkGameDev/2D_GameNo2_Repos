using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : AttackController
{
    private PlayerStatsController statsController;

    protected override void Start()
    {
        base.Start();

        statsController = GetComponent<PlayerStatsController>();
    }

    public override void OnAttacked(GameObject attacker, Attack attack)
    {
        base.OnAttacked(attacker, attack);
        statsController.DescreaseHealth(attack.damage);
    }
}
