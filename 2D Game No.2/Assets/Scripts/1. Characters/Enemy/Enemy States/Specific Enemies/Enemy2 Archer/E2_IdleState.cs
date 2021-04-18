using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_IdleState : EnemyIdleState
{
    private E2_Controller enemyController;

    public E2_IdleState(EnemyBaseController enemyBaseController_, EnemyData enemyData_, string animParameter_, E2_Controller enemyController_) 
        : base(enemyBaseController_, enemyData_, animParameter_)
    {
        enemyController = enemyController_;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPlayerInBoxRange)
        {
            enemyStateMachine.ChangeState(enemyController.enemyPlayerDetectedState);
        }
        else if (isIdleTimeOver)
        {
            enemyStateMachine.ChangeState(enemyController.enemyMoveState);
        }
    }
}
