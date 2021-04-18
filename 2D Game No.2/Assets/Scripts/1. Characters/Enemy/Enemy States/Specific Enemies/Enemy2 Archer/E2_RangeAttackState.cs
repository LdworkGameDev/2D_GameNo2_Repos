using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_RangeAttackState : EnemyRangeAttackState
{
    private E2_Controller enemyController;

    public E2_RangeAttackState(EnemyBaseController enemyBaseController_, EnemyData enemyData_, string animParameter_, E2_Controller enemyController_)
        : base(enemyBaseController_, enemyData_, animParameter_)
    {
        enemyController = enemyController_;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(player == null)
        {
            enemyController.enemyLookForPlayerState.SetTurnImmediately(true);
            enemyStateMachine.ChangeState(enemyController.enemyLookForPlayerState);
        }

        if (isAnimationFinished)
        {
            enemyController.enemyLookForPlayerState.SetTurnImmediately(false);
            enemyStateMachine.ChangeState(enemyController.enemyLookForPlayerState);
        }
    }

    public override void TriggerShootingProjectile()
    {
        base.TriggerShootingProjectile();

        ShootArrow();
    }

    private void ShootArrow()
    {
        GameObject newArrow = GameObject.Instantiate(enemyController.arrowShooting.arrow,
           enemyController.arrowShootingPosition.position, enemyController.arrowShootingPosition.rotation);
        Rigidbody2D rigid = newArrow.GetComponent<Rigidbody2D>();

        newArrow.GetComponent<Arrow>().arrowShootingDefinition = enemyController.arrowShooting;
        newArrow.GetComponent<Arrow>().shooterStatsController = enemyController.statsController;

        float x = player.transform.position.x - enemyController.gameObject.transform.position.x;
        float y = player.transform.position.y - enemyController.gameObject.transform.position.y + enemyController.arrowShooting.targetHeight;
        float h = Mathf.Abs(y) + enemyController.arrowShooting.maxArrowHeight;
        float g = Physics2D.gravity.y * rigid.gravityScale;

        float xVelocity = x / (Mathf.Sqrt(-2 * h / g) + Mathf.Sqrt(2 * (y - h) / g));
        float yVelocity = Mathf.Sqrt(-2 * g * h);

        rigid.velocity = new Vector2(xVelocity, yVelocity);
    }
}
