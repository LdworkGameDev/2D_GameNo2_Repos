using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_DeathState : EnemyDeathState
{
    private E2_Controller enemyController;

    public E2_DeathState(EnemyBaseController enemyBaseController_, EnemyData enemyData_, string animParameter_, E2_Controller enemyController_) 
        : base(enemyBaseController_, enemyData_, animParameter_)
    {
        enemyController = enemyController_;
    }
}
