using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected float xMove = 0f;
    protected bool isGrounded = false;

    private bool dashInput = false;
    private bool jumpInput = false;
    private bool attackInput = false;

    public PlayerGroundedState(PlayerController playerController_, PlayerData playerData_, string animParameter_) : base(playerController_, playerData_, animParameter_)
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

        playerController.playerJumpState.ResetJumpTimesCounter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xMove = playerInputHandler.xMove;
        jumpInput = playerInputHandler.jumpInput;
        dashInput = playerInputHandler.dashInput;
        attackInput = playerInputHandler.attackInput;

        if (jumpInput && playerController.playerJumpState.CanJump())
        {
            playerInputHandler.UseJumpInput();
            playerStateMachine.ChangeState(playerController.playerJumpState);
            playerController.playerJumpState.ReduceJumpTimes();
        }
        else if (dashInput)
        {
            playerInputHandler.UseDashInput();
            playerStateMachine.ChangeState(playerController.playerDashState); 
        }
        else if (attackInput)
        {
            playerInputHandler.UseAttackInput();
            playerStateMachine.ChangeState(playerController.playerAttack1State);
        }
        else if (!isGrounded)
        {
            playerStateMachine.ChangeState(playerController.playerInAirState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    
}
