using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_LookForPlayerState : EnemyLookForPlayerState
{
    private E1_Controller enemyController;
    
    public E1_LookForPlayerState(EnemyBaseController enemyBaseController_, EnemyData enemyData_, string animParameter_, E1_Controller enemyController_)
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

        enemyBaseController.SetVelocity(Vector2.zero);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPlayerInMinLineRange)
        {
            enemyStateMachine.ChangeState(enemyController.enemyPlayerDetectedState);
        }
        else if (isStateOver)
        {
            enemyStateMachine.ChangeState(enemyController.enemyMoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
