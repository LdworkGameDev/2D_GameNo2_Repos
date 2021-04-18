using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected PlayerStateMachine playerStateMachine;
    protected PlayerController playerController;
    protected PlayerInputHandler playerInputHandler;
    protected PlayerData playerData;

    protected float startTime = 0f;
    protected bool isAnimationFinished = false;
    protected bool hasChangeState = false;

    private string animParameter;

    public PlayerState(PlayerController playerController_, PlayerData playerData_, string animParameter_)
    {
        playerController = playerController_;
        animParameter = animParameter_;
        playerData = playerData_;
    }

    public virtual void Enter()
    {
        playerStateMachine = playerController.playerStateMachine;
        playerInputHandler = playerController.playerInputHandler;

        DoChecks();
        startTime = Time.time;
        playerController.playerAnim.SetBool(animParameter, true);
        
        //Debug.Log(animParameter);

        isAnimationFinished = false;
        hasChangeState = false;
    }

    public virtual void Exit()
    {
        playerController.playerAnim.SetBool(animParameter, false);
        hasChangeState = true;
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

    }

    public virtual void SetAnimationStart()
    {

    }

    public virtual void SetAnimationFinish()
    {
        isAnimationFinished = true;
    }

}
