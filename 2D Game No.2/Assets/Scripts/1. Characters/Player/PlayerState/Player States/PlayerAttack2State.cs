using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack2State : PlayerAbilityState
{
    public PlayerAttack2State(PlayerController playerController_, PlayerData playerData_, string animParameter_) : base(playerController_, playerData_, animParameter_)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isGrounded)
        {
            playerController.SetPlayerVelocityX(xMove * playerData.moveSpeed / 2);
        }
    }

    public override void SetAbilityDone()
    {
        base.SetAbilityDone();

        playerInputHandler.UseAttackInput();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        Collider2D[] hittedEnemies = Physics2D.OverlapBoxAll(playerData.attack2Position.position,
            new Vector2(playerController.meleeAttack2.rangeX, playerController.meleeAttack2.rangeX), 0, playerData.whatIsEnemy);

        foreach(Collider2D enemy in hittedEnemies)
        {
            EnemyStatsController enemyStatsController = enemy.gameObject.GetComponent<EnemyStatsController>();
            EnemyAttackController enemyAttackController = enemy.gameObject.GetComponent<EnemyAttackController>();

            if (enemyAttackController != null && enemyStatsController != null)
            {
                Attack attack = playerController.meleeAttack2.PlayerCreateAttack(playerController.statsController, enemyStatsController, playerController.meleeAttack2.hitBackForce);
                enemyAttackController.OnAttacked(playerController.gameObject, attack);
            }

            //Attack attack = playerController.meleeAttack2.CreateAttack(playerController.statsController,
            //    enemy.gameObject.GetComponent<CharacterStatsController>(), playerController.meleeAttack2.hitBackForce);
            //enemy.gameObject.GetComponent<AttackHandler>().OnAttacked(playerController.gameObject, attack);
        }
    }
}
