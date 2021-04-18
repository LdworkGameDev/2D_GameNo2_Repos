using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_DodgeState : EnemyDodgeState
{
    private E2_Controller enemyController;

    public E2_DodgeState(EnemyBaseController enemyBaseController_, EnemyData enemyData_, string animParameter_, E2_Controller enemyController_)
        : base(enemyBaseController_, enemyData_, animParameter_)
    {
        enemyController = enemyController_;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            if (isPlayerInShortRangeAction)
            {
                enemyStateMachine.ChangeState(enemyController.enemyMeleeAttackState);
            }
            else if (!isPlayerInMaxBoxRange)
            {
                enemyStateMachine.ChangeState(enemyController.enemyLookForPlayerState);
            }
            else
            {
                enemyStateMachine.ChangeState(enemyController.enemyRangeAttackState);
            }
        }
    }
}
