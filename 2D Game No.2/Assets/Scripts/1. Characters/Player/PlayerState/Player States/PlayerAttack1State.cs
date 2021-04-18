using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerAttack1State : PlayerAbilityState
{
    private bool attackInput = false;
    private bool canDoubleAttack = false;

    public PlayerAttack1State(PlayerController playerController_, PlayerData playerData_, string animParameter_) : base(playerController_, playerData_, animParameter_)
    {

    }

    public override void Enter()
    {
        base.Enter();

        canDoubleAttack = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (hasChangeState) return;

        attackInput = playerInputHandler.attackInput;

        if (isGrounded)
        {
            playerController.SetPlayerVelocityX(xMove * playerData.moveSpeed / 2);
            if (attackInput && startTime + playerData.doubleAttackTime > Time.time)
            {
                canDoubleAttack = true;
            }
        }
    }

    public override void SetAbilityDone()
    {
        base.SetAbilityDone();

        playerInputHandler.UseAttackInput();
    }

    public bool CanDoubleAttack()
    {
        return canDoubleAttack;
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        Collider2D[] hittedEnemies = Physics2D.OverlapCircleAll(playerData.attack1Position.position, playerController.meleeAttack1.radiusRange, playerData.whatIsEnemy);
        foreach(Collider2D enemy in hittedEnemies)
        {
            EnemyStatsController enemyStatsController = enemy.gameObject.GetComponent<EnemyStatsController>();
            EnemyAttackController enemyAttackHandler = enemy.gameObject.GetComponent<EnemyAttackController>();

            if(enemyAttackHandler != null && enemyStatsController != null)
            {
                Attack attack = playerController.meleeAttack1.PlayerCreateAttack(playerController.statsController, enemyStatsController, playerController.meleeAttack1.hitBackForce);
                enemyAttackHandler.OnAttacked(playerController.gameObject, attack);
            }
        }
    }
}
