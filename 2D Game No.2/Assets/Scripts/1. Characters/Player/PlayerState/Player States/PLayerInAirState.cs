using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerInAirState : PlayerState
{
    private float xMove = 0f;
    private bool isGrounded = false;
    private bool jumpInput = false;
    private bool dashInput = false;
    private bool attackInput = false;

    public PLayerInAirState(PlayerController playerController_, PlayerData playerData_, string animParameter_) : base(playerController_, playerData_, animParameter_)
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

        if (isGrounded && Mathf.Abs(playerController.rigid.velocity.y) < 0.01f)
        {
            playerStateMachine.ChangeState(playerController.playerLandState);
        }
        else if(jumpInput && playerController.playerJumpState.CanJump())
        {
            playerInputHandler.UseJumpInput();
            playerController.playerJumpState.ReduceJumpTimes();
            playerStateMachine.ChangeState(playerController.playerJumpState);
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
        else
        {
            playerController.CheckForFlip(xMove);
            playerController.SetPlayerVelocityX(xMove * playerData.moveSpeed);

            playerController.playerAnim.SetFloat("yVelocity", playerController.rigid.velocity.y);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
