using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_MeleeAttackState : EnemyShortRangeActionState
{
    private E1_Controller enemyController;  
    
    public MeleeAttackCircle_SO meleeAttack;
    public float lastSRActionTime = 0f;

    public E1_MeleeAttackState(EnemyBaseController enemyBaseController_, EnemyData enemyData_, string animParameter_, E1_Controller enemyController_, MeleeAttackCircle_SO meleeAttack_)
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
            if (isPlayerInMinLineRange)
            {
                enemyStateMachine.ChangeState(enemyController.enemyPlayerDetectedState);
            }
            else
            {
                enemyStateMachine.ChangeState(enemyController.enemyLookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        Collider2D[] hittedObjects = Physics2D.OverlapCircleAll(enemyController.SRADetectPosition.position, meleeAttack.radiusRange, enemyData.whatIsPlayer);
        
        foreach(Collider2D hitted in hittedObjects)
        {
            Attack attack = meleeAttack.EnemyCreateAttack(enemyController.statsController, hitted.gameObject.GetComponent<PlayerStatsController>(), meleeAttack.hitBackForce);

            PlayerAttackController playerAttackController = hitted.GetComponent<PlayerAttackController>();
            if(playerAttackController != null)
            {
                playerAttackController.OnAttacked(enemyController.gameObject, attack);
            }
        }
    }
}
