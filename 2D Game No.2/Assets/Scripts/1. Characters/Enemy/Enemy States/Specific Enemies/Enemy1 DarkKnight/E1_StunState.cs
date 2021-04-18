using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_StunState : EnemyStunState
{
    private E1_Controller enemyController;

    public E1_StunState(EnemyBaseController enemyBaseController_, EnemyData enemyData_, string animParameter_, E1_Controller enemyController_) 
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
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isStunTimeOver)
        {
            if (performShortRangeAction)
            {
                enemyStateMachine.ChangeState(enemyController.enemyMelleAttackState);
            }
            else if (isPlayerInMinRange)
            {
                enemyStateMachine.ChangeState(enemyController.enemyPlayerDetectedState);
            }
            else
            {
                enemyController.enemyLookForPlayerState.SetTurnImmediately(true);
                enemyStateMachine.ChangeState(enemyController.enemyLookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
