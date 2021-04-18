using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyState
{
    protected bool isDetectingWall = false;
    protected bool isDetectingLedge = false;

    protected bool isPlayerInMinRangeLine = false;
    protected bool isPlayerInMaxRangeLine = false;

    protected bool isPlayerInMinBoxRange = false;

    public EnemyMoveState(EnemyBaseController enemyBaseController_, EnemyData enemyData_, string animParameter_) : base(enemyBaseController_, enemyData_, animParameter_)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();

        isDetectingWall = enemyBaseController.CheckForWall();
        isDetectingLedge = enemyBaseController.CheckForLedge();

        isPlayerInMinRangeLine = enemyBaseController.CheckForPlayerInMinRangeLine();
        isPlayerInMaxRangeLine = enemyBaseController.CheckForPlayerInMaxRangeLine();

        isPlayerInMinBoxRange = enemyBaseController.CheckForPlayerInMinBoxRange();
    }

    public override void Enter()
    {
        base.Enter();

        enemyBaseController.SetVelocity(new Vector2(enemyBaseController.facingDirection * enemyData.moveSpeed, 0));
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
