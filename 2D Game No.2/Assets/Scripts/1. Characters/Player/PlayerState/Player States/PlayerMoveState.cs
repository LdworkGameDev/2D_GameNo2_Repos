using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(PlayerController playerController_, PlayerData playerData_, string animParameter_) : base(playerController_, playerData_, animParameter_)
    {

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (hasChangeState) return;

        playerController.CheckForFlip(xMove);
        playerController.SetPlayerVelocityX(playerData.moveSpeed * xMove);

        if (xMove == 0f)
        {
            playerStateMachine.ChangeState(playerController.playerIdleState);
        }
    }
}
