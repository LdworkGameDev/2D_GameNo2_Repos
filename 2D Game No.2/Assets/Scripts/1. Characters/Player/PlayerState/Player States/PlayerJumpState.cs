using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private int jumpTimesCounter = 0;

    public PlayerJumpState(PlayerController playerController_, PlayerData playerData_, string animParameter_) : base(playerController_, playerData_, animParameter_)
    {
        jumpTimesCounter = playerData.jumpTimes;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        playerController.SetPlayerVelocityY(playerData.jummpForce);
        isAbilityDone = true;
    }

    public bool CanJump()
    {
        return (jumpTimesCounter > 0) ? true : false;
    }

    public void ReduceJumpTimes()
    {
        jumpTimesCounter--;
    }

    public void ResetJumpTimesCounter()
    {
        jumpTimesCounter = playerData.jumpTimes;
    }

}
