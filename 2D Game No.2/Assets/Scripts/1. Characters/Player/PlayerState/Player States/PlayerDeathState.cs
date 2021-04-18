using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerState
{
    public PlayerDeathState(PlayerController playerController_, PlayerData playerData_, string animParameter_) : base(playerController_, playerData_, animParameter_)
    {

    }

    public override void Enter()
    {
        base.Enter();

        playerController.SetPlayerVelocity(Vector2.zero);
        playerController.rigid.bodyType = RigidbodyType2D.Kinematic;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            playerController.gameObject.SetActive(false);
        }
    }
}
