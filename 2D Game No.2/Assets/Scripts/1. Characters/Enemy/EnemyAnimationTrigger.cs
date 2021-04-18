using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationTrigger : MonoBehaviour
{
    private EnemyBaseController enemyController;

    private void Start()
    {
        enemyController = GetComponent<EnemyBaseController>();
    }

    public void SetAnimationFinished()
    {
        enemyController.enemyStateMachine.currentState.SetAnimationFinish();
    }

    public void TriggerAttack()
    {
        if (!(enemyController.enemyStateMachine.currentState is EnemyShortRangeActionState)) return;

        ((EnemyShortRangeActionState)enemyController.enemyStateMachine.currentState).TriggerAttack();
    }

   public void TriggerShootingProjectile()
    {
        if (!(enemyController.enemyStateMachine.currentState is EnemyRangeAttackState)) return;

        ((EnemyRangeAttackState)enemyController.enemyStateMachine.currentState).TriggerShootingProjectile();
    }
}
