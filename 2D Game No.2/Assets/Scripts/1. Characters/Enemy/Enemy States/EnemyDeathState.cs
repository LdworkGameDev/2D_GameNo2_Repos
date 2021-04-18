using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyState
{
    public EnemyDeathState(EnemyBaseController enemyBaseController_, EnemyData enemyData_, string animParameter_) : base(enemyBaseController_, enemyData_, animParameter_)
    {

    }

    public override void Enter()
    {
        base.Enter();

        enemyBaseController.SetVelocity(Vector2.zero);
        enemyBaseController.rigid.bodyType = RigidbodyType2D.Kinematic;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            enemyBaseController.spawner.SpawnItem();
            enemyBaseController.gameObject.SetActive(false);
        }
    }
}
