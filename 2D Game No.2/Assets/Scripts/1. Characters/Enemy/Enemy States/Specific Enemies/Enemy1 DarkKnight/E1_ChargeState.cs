using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_ChargeState : EnemyChargeState
{
    private E1_Controller enemyController;

    public E1_ChargeState(EnemyBaseController enemyBaseController_, EnemyData enemyData_, string animParameter_, E1_Controller enemyController_) 
        : base(enemyBaseController_, enemyData_, animParameter_)
    {
        enemyController = enemyController_;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        enemyController.SetVelocity(new Vector2(enemyController.facingDirection * enemyData.chargeSpeed, 0));
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (performShortRangeAction)
        {
            enemyStateMachine.ChangeState(enemyController.enemyMelleAttackState);
        }
        else if (isDetectingWall || !isDetectignLedge)
        {
            enemyStateMachine.ChangeState(enemyController.enemyLookForPlayerState);
        }
        else if (isChargeTimeOver)
        {
            if (isPlayerInMinRange)
            {
                enemyStateMachine.ChangeState(enemyController.enemyPlayerDetectedState);
            }
            else
            {
                enemyStateMachine.ChangeState(enemyController.enemyLookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
