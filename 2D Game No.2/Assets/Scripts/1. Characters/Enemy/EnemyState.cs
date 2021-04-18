using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    protected EnemyStateMachine enemyStateMachine;
    protected EnemyBaseController enemyBaseController;
    protected EnemyData enemyData;

    public float startTime { get; protected set; }
    public float endTime { get; protected set; }

    protected bool isAnimationFinished = false;
    protected GameObject player;

    private string animParameter;

    public EnemyState(EnemyBaseController enemyBaseController_, EnemyData enemyData_, string animParameter_)
    {
        enemyBaseController = enemyBaseController_;
        enemyData = enemyData_;
        animParameter = animParameter_;
    }

    public virtual void Enter()
    {
        enemyStateMachine = enemyBaseController.enemyStateMachine;
        DoChecks();

        startTime = Time.time;
        enemyBaseController.enemyAnim.SetBool(animParameter, true);
        isAnimationFinished = false;
    }

    public virtual void Exit()
    {
        endTime = Time.time;
        enemyBaseController.enemyAnim.SetBool(animParameter, false);
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {
        player = enemyBaseController.FindPlayer();
    }

    public virtual void SetAnimationFinish()
    {
        isAnimationFinished = true;
    }
}
