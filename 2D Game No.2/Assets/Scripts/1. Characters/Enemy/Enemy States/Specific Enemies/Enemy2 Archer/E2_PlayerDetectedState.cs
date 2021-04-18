using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_PlayerDetectedState : EnemyPlayerDetectedState
{
    private E2_Controller enemyController;

    public E2_PlayerDetectedState(EnemyBaseController enemyBaseController_, EnemyData enemyData_, string animParameter_, E2_Controller enemyController_)
        : base(enemyBaseController_, enemyData_, animParameter_)
    {
        enemyController = enemyController_;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (performShortRangeAction)
        {
            if(Time.time > enemyData.dodgeCooldown + enemyController.enemyDodgeState.endTime)
            {
                if (shouldFlipToFacePlayer)
                {
                    enemyController.Flip();
                }
                enemyStateMachine.ChangeState(enemyController.enemyDodgeState);
            }
            else if (Time.time > enemyData.SRACooldownTime + enemyController.enemyMeleeAttackState.endTime)
            {
                if (shouldFlipToFacePlayer)
                {
                    enemyController.Flip();
                }
                enemyStateMachine.ChangeState(enemyController.enemyMeleeAttackState);
            }
        }
        else if (performLongRangeAction)
        {
            if (shouldFlipToFacePlayer)
            {
                enemyController.Flip();
            }
            enemyStateMachine.ChangeState(enemyController.enemyRangeAttackState);
        } 
        else if (!isPlayerInMaxBoxRange)
        {
            enemyController.enemyLookForPlayerState.SetTurnImmediately(true);
            enemyStateMachine.ChangeState(enemyController.enemyLookForPlayerState);
        }
    }
}
