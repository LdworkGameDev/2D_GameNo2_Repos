using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackedState : PlayerState
{
    private bool isGrounded = false;

    public PlayerAttackedState(PlayerController playerController_, PlayerData playerData_, string animParameter_) : base(playerController_, playerData_, animParameter_)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = playerController.CheckForGround();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            if (isGrounded)
            {
                playerStateMachine.ChangeState(playerController.playerIdleState);
            }
            else
            {
                playerStateMachine.ChangeState(playerController.playerInAirState);
            }
        }
    }
}
