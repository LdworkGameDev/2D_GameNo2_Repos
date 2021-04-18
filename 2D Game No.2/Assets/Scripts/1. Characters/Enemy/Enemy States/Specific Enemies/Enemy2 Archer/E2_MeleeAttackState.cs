using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_MeleeAttackState : EnemyShortRangeActionState
{
    private E2_Controller enemyController;
    private MeleeAttackCircle_SO meleeAttack;

    public E2_MeleeAttackState(EnemyBaseController enemyBaseController_, EnemyData enemyData_, string animParameter_, E2_Controller enemyController_, MeleeAttackCircle_SO meleeAttack_) 
        : base(enemyBaseController_, enemyData_, animParameter_)
    {
        enemyController = enemyController_;
        meleeAttack = meleeAttack_;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            if (isPlayerInMinBoxRange)
            {
                enemyStateMachine.ChangeState(enemyController.enemyPlayerDetectedState);
            }
            else
            {
                enemyController.enemyLookForPlayerState.SetTurnImmediately(false);
                enemyStateMachine.ChangeState(enemyController.enemyLookForPlayerState);
            }
        }
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        Collider2D[] hittedEnemies = Physics2D.OverlapCircleAll(enemyController.meleeAttackPosition.position, meleeAttack.radiusRange, enemyData.whatIsPlayer);
        foreach(Collider2D enemy in hittedEnemies)
        {
            Attack attack = meleeAttack.EnemyCreateAttack(enemyController.statsController, enemy.gameObject.GetComponent<PlayerStatsController>(), meleeAttack.hitBackForce);
            PlayerAttackController playerAttackController = enemy.gameObject.GetComponent<PlayerAttackController>();
            if(playerAttackController != null)
            {
                playerAttackController.OnAttacked(enemyController.gameObject, attack);
            }
        }
    }
}
