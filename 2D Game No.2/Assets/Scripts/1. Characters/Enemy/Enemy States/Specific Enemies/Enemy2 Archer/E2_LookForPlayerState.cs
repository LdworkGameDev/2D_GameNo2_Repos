using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_LookForPlayerState : EnemyLookForPlayerState
{
    private E2_Controller enemyController;

    public E2_LookForPlayerState(EnemyBaseController enemyBaseController_, EnemyData enemyData_, string animParameter_, E2_Controller enemyController_) 
        : base(enemyBaseController_, enemyData_, animParameter_)
    {
        enemyController = enemyController_;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPlayerInMinBoxRange)
        {
            enemyStateMachine.ChangeState(enemyController.enemyPlayerDetectedState);
        }

        else if (isStateOver)
        {
            enemyStateMachine.ChangeState(enemyController.enemyMoveState);
        }
    }
}