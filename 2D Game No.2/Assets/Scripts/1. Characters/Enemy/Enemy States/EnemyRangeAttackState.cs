using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttackState : EnemyLongRangeAction
{
    public EnemyRangeAttackState(EnemyBaseController enemyBaseController_, EnemyData enemyData_, string animParameter_) : base(enemyBaseController_, enemyData_, animParameter_)
    {

    }

    public override void Enter()
    {
        base.Enter();

        enemyBaseController.SetVelocity(Vector2.zero);
    }

    public virtual void TriggerShootingProjectile()
    {

    }
}
