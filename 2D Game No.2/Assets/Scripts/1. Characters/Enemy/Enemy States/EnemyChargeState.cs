using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChargeState : EnemyLongRangeAction
{
    protected bool isChargeTimeOver = false;

    public EnemyChargeState(EnemyBaseController enemyBaseController_, EnemyData enemyData_, string animParameter_) : base(enemyBaseController_, enemyData_, animParameter_)
    {

    }

    public override void Enter()
    {
        base.Enter();

        isChargeTimeOver = false;
        enemyBaseController.SetVelocity(new Vector2(enemyData.chargeSpeed, 0));
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(startTime + enemyData.chargeTime < Time.time)
        {
            isChargeTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
