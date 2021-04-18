using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_DeathState : EnemyDeathState
{
    private E1_Controller enemyController;

    public E1_DeathState(EnemyBaseController enemyBaseController_, EnemyData enemyData_, string animParameter_, E1_Controller enemyController_) 
        : base(enemyBaseController_, enemyData_, animParameter_)
    {
        enemyController = enemyController_;
    }
}
