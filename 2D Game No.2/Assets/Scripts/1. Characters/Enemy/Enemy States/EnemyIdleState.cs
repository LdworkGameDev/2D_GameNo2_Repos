using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    protected float idleTime = 0f;
    protected bool flipAfterIdle = false;
    protected bool isIdleTimeOver = false;

    protected bool isPlayerInMinRangeLine = false;
    protected bool isPlayerInMaxRangeLine = false;

    protected bool isPlayerInBoxRange = false;

    public EnemyIdleState(EnemyBaseController enemyBaseController_, EnemyData enemyData_, string animParameter_) : base(enemyBaseController_, enemyData_, animParameter_)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinRangeLine = enemyBaseController.CheckForPlayerInMinRangeLine();
        isPlayerInMaxRangeLine = enemyBaseController.CheckForPlayerInMaxRangeLine();

        isPlayerInBoxRange = enemyBaseController.CheckForPlayerInMinBoxRange();
    }

    public override void Enter()
    {
        base.Enter();

        enemyBaseController.SetVelocity(Vector2.zero);
        isIdleTimeOver = false;
        SetRandomIdleTime();
    }

    public override void Exit()
    {
        base.Exit();

        if (flipAfterIdle)
        {
            enemyBaseController.Flip();
        }

        SetFlipAfterIdle(false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(startTime + idleTime < Time.time)
        {
            isIdleTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetFlipAfterIdle(bool flip)
    {
        flipAfterIdle = flip;
    }

    private void SetRandomIdleTime()
    {
        idleTime = Random.Range(enemyData.minIdleTime, enemyData.maxIdleTime);
    }
}
