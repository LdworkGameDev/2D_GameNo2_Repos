using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLookForPlayerState : EnemyState
{
    protected bool turnImmediately = false;

    protected bool isPlayerInMinLineRange = false;
    protected bool isPlayerInMinBoxRange = false;
    protected bool isAllTurnsDone = false;
    protected bool isStateOver = false;

    protected float lastTurnTime = 0f;
    protected int amountOfTurnsDone = 0;

    public EnemyLookForPlayerState(EnemyBaseController enemyBaseController_, EnemyData enemyData_, string animParameter_) : base(enemyBaseController_, enemyData_, animParameter_)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinLineRange = enemyBaseController.CheckForPlayerInMinRangeLine();
        isPlayerInMinBoxRange = enemyBaseController.CheckForPlayerInMinBoxRange();
    }

    public override void Enter()
    {
        base.Enter();

        enemyBaseController.SetVelocity(Vector2.zero);

        isAllTurnsDone = false;
        isStateOver = false;

        lastTurnTime = startTime;
        amountOfTurnsDone = 0;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (turnImmediately)
        {
            enemyBaseController.Flip();
            lastTurnTime = Time.time;
            amountOfTurnsDone++;
            SetTurnImmediately(false);
        }
        else if((lastTurnTime + enemyData.timeBetweenTurns < Time.time) && !isAllTurnsDone)
        {
            enemyBaseController.Flip();
            lastTurnTime = Time.time;
            amountOfTurnsDone++;
        }

        if(amountOfTurnsDone >= enemyData.amountOfTurns)
        {
            isAllTurnsDone = true;
        }

        if(lastTurnTime + enemyData.timeBetweenTurns < Time.time && isAllTurnsDone)
        {
            isStateOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetTurnImmediately(bool flip)
    {
        turnImmediately = flip;
    }
}
