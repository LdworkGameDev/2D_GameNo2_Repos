using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected bool isAbilityDone = false;
    protected bool isGrounded = false;
    protected float xMove = 0f;

    public PlayerAbilityState(PlayerController playerController_, PlayerData playerData_, string animParameter_) : base(playerController_, playerData_, animParameter_)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = playerController.CheckForGround();
    }

    public override void Enter()
    {
        base.Enter();

        isAbilityDone = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xMove = playerInputHandler.xMove;

        if (isAbilityDone)
        {
            if (isGrounded && Mathf.Abs(playerController.rigid.velocity.y) < 0.01f)
            {
                playerStateMachine.ChangeState(playerController.playerIdleState);
            }
            else if(!isGrounded)
            {
                playerStateMachine.ChangeState(playerController.playerInAirState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public virtual void SetAbilityDone()
    {
        isAbilityDone = true;
    }

    public virtual void TriggerAttack()
    {

    }
}
