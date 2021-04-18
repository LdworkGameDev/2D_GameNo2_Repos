using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLongRangeAction : EnemyState
{
    protected bool isPlayerInMinRange = false;
    protected bool isDetectingWall = false;
    protected bool isDetectignLedge = false;
    protected bool performShortRangeAction = false;

    public EnemyLongRangeAction(EnemyBaseController enemyBaseController_, EnemyData enemyData_, string animParameter_) : base(enemyBaseController_, enemyData_, animParameter_)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinRange = enemyBaseController.CheckForPlayerInMinRangeLine();
        isDetectingWall = enemyBaseController.CheckForWall();
        isDetectignLedge = enemyBaseController.CheckForLedge();
        performShortRangeAction = enemyBaseController.CheckForPlayerInShortRangeAction();
    }

    public override void Enter()
    {
        base.Enter();

        isAnimationFinished = false;
    }
}
