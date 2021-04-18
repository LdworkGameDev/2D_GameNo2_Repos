using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(PlayerController playerController_, PlayerData playerData_, string animParameter_) : base(playerController_, playerData_, animParameter_)
    {

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (hasChangeState) return;

        if(xMove != 0)
        {
            playerStateMachine.ChangeState(playerController.playerMoveState);
        }
        else if (isAnimationFinished) 
        {
            playerStateMachine.ChangeState(playerController.playerIdleState);
        }
    }
}
