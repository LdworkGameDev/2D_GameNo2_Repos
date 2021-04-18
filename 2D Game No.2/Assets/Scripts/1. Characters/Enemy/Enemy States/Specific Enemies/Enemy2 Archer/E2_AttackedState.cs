using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_AttackedState : EnemyAttackedState
{
    private E2_Controller enemyController;

    public E2_AttackedState(EnemyBaseController enemyBaseController_, EnemyData enemyData_, string animParameter_, E2_Controller enemyController_) 
        : base(enemyBaseController_, enemyData_, animParameter_)
    {
        enemyController = enemyController_;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            enemyController.enemyLookForPlayerState.SetTurnImmediately(false);
            enemyStateMachine.ChangeState(enemyController.enemyLookForPlayerState);
        }
    }
}
