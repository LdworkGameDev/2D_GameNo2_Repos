using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerDetectedState : EnemyState
{
    protected bool isPlayerInMinLineRange = false;
    protected bool isPlayerInMaxLineRange = false;
    protected bool isPlayerInMinBoxRange = false;
    protected bool isPlayerInMaxBoxRange = false;


    protected bool performShortRangeAction = false;
    protected bool performLongRangeAction = false;

    protected bool shouldFlipToFacePlayer = false;

    public EnemyPlayerDetectedState(EnemyBaseController enemyBaseController_, EnemyData enemyData_, string animParameter_) : base(enemyBaseController_, enemyData_, animParameter_)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinLineRange = enemyBaseController.CheckForPlayerInMinRangeLine();
        isPlayerInMaxLineRange = enemyBaseController.CheckForPlayerInMaxRangeLine();
        isPlayerInMinBoxRange = enemyBaseController.CheckForPlayerInMinBoxRange();
        isPlayerInMaxBoxRange = enemyBaseController.CheckForPlayerInMaxBoxRange();

        performShortRangeAction = enemyBaseController.CheckForPlayerInShortRangeAction();

        shouldFlipToFacePlayer = enemyBaseController.CheckForFlipToFacePlayer();
    }

    public override void Enter()
    {
        base.Enter();

        enemyBaseController.SetVelocity(Vector2.zero);
        performLongRangeAction = false;
        performShortRangeAction = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (startTime + enemyData.LRActionWaitTime < Time.time && !performShortRangeAction)
        {
            performLongRangeAction = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
