using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStunState : EnemyState
{
    protected bool isStunTimeOver = false;
    protected bool isGrounded = false;
    
    //Used to check for enmey not to be hitted back twice (not bouncing into the air when get hitted multiple times)
    protected bool isMovementStop = false;
    protected bool isPlayerInMinRange = false;
    protected bool isPlayerInSRAction = false;
    protected bool performShortRangeAction = false;

    public EnemyStunState(EnemyBaseController enemyBaseController_, EnemyData enemyData_, string animParameter_) : base(enemyBaseController_, enemyData_, animParameter_)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = enemyBaseController.CheckForGround();
        isPlayerInMinRange = enemyBaseController.CheckForPlayerInMinRangeLine();
        isPlayerInSRAction = enemyBaseController.CheckForPlayerInShortRangeAction();
    }

    public override void Enter()
    {
        base.Enter();

        isStunTimeOver = false;
        isGrounded = false;
        isMovementStop = false;
        performShortRangeAction = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isPlayerInSRAction)
        {
            performShortRangeAction = true;
        }

        if (Time.time > startTime + enemyData.stunTime)
        {
            isStunTimeOver = true;
        }

        if (isGrounded && Time.time > startTime + enemyData.stunHittedBackTime && !isMovementStop)
        {
            isMovementStop = true;
            enemyBaseController.SetVelocity(Vector2.zero);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
