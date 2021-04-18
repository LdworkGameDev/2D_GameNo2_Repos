using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_IdleState : EnemyIdleState
{
    private E1_Controller enemyController;

    public E1_IdleState(EnemyBaseController enemyBaseController_, EnemyData enemyData_, string animParameter_, E1_Controller enemyController_)
        : base(enemyBaseController_, enemyData_, animParameter_)
    {
        enemyController = enemyController_;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPlayerInMinRangeLine)
        {
            enemyStateMachine.ChangeState(enemyController.enemyPlayerDetectedState);
        }
        if (isIdleTimeOver)
        {
            enemyStateMachine.ChangeState(enemyController.enemyMoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
