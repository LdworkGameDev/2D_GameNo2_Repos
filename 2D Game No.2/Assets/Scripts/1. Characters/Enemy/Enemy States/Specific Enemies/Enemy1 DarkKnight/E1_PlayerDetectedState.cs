using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_PlayerDetectedState : EnemyPlayerDetectedState
{
    private E1_Controller enemyController;

    public E1_PlayerDetectedState(EnemyBaseController enemyBaseController_, EnemyData enemyData_, string animParameter_, E1_Controller enemyController_) 
        : base(enemyBaseController_, enemyData_, animParameter_)
    {
        enemyController = enemyController_;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (performShortRangeAction && Time.time > enemyData.SRACooldownTime + enemyController.enemyMelleAttackState.endTime)
        {
            enemyStateMachine.ChangeState(enemyController.enemyMelleAttackState);
        }
        else if (performLongRangeAction)
        {
            enemyStateMachine.ChangeState(enemyController.enemyChargeState);
        }
        else if (!isPlayerInMaxLineRange)
        {
            enemyStateMachine.ChangeState(enemyController.enemyLookForPlayerState);
        }
    }
}
