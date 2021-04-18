using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDodgeState : EnemyState
{
    protected bool isPlayerInShortRangeAction = false;
    protected bool isPlayerInMaxBoxRange = false;

    public EnemyDodgeState(EnemyBaseController enemyBaseController_, EnemyData enemyData_, string animParameter_) : base(enemyBaseController_, enemyData_, animParameter_)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInShortRangeAction = enemyBaseController.CheckForPlayerInShortRangeAction();
        isPlayerInMaxBoxRange = enemyBaseController.CheckForPlayerInMaxBoxRange();
    }

    public override void Enter()
    {
        base.Enter();

        enemyBaseController.SetVelocity(enemyData.dodgeForce * new Vector2(-enemyBaseController.facingDirection, 1));
    }

    public override void SetAnimationFinish()
    {
        base.SetAnimationFinish();
    }
}
