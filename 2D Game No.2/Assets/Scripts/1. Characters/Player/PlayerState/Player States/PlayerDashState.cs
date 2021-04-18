using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    private float gravityScale = 0f;
    public PlayerDashState(PlayerController playerController_, PlayerData playerData_, string animParameter_) : base(playerController_, playerData_, animParameter_)
    {
    }

    public override void Enter()
    {
        base.Enter();

        gravityScale = playerController.rigid.gravityScale;
        playerController.rigid.gravityScale = 0f;

        int direction = (playerController.isFacingRight) ? 1 : -1;
        playerController.SetPlayerVelocity(new Vector2(playerData.dashSpeed * direction, 0));
    }

    public override void Exit()
    {
        base.Exit();

        playerController.rigid.gravityScale = gravityScale;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (startTime + playerData.dashTime < Time.time)
        {
            playerController.SetPlayerVelocityX(0);
            playerController.rigid.gravityScale = gravityScale;
            isAbilityDone = true;
        }
    }
}
