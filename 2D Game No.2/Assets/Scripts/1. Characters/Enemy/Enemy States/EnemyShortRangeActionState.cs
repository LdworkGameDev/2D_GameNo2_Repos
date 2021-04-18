using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShortRangeActionState : EnemyState
{
    protected bool isPlayerInMinLineRange = false;
    protected bool isPlayerInMinBoxRange = false;

    public EnemyShortRangeActionState(EnemyBaseController enemyBaseController_, EnemyData enemyData_, string animParameter_) : base(enemyBaseController_, enemyData_, animParameter_)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinLineRange = enemyBaseController.CheckForPlayerInMinRangeLine();
        isPlayerInMinBoxRange = enemyBaseController.CheckForPlayerInMinBoxRange();
    }

    public override void Enter()
    {
        base.Enter();

        isAnimationFinished = false;
        enemyBaseController.SetVelocity(Vector2.zero);
    }

    public virtual void TriggerAttack()
    {

    }
}
