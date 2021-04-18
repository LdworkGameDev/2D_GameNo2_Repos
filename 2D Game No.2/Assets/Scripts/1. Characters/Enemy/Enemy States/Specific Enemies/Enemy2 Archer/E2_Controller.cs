using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class E2_Controller : EnemyBaseController
{
    public E2_IdleState enemyIdleState { get; private set; }
    public E2_MoveState enemyMoveState { get; private set; }
    public E2_PlayerDetectedState enemyPlayerDetectedState { get; private set; }
    public E2_MeleeAttackState enemyMeleeAttackState { get; private set; }
    public E2_LookForPlayerState enemyLookForPlayerState { get; private set; }
    public E2_RangeAttackState enemyRangeAttackState { get; private set; }
    public E2_AttackedState enemyAttackedState { get; private set; }
    public E2_DeathState enemyDeathState { get; private set; }
    public E2_DodgeState enemyDodgeState { get; private set; }

    public MeleeAttackCircle_SO meleeAttack;
    public ArrowShooting_SO arrowShooting;

    public Transform arrowShootingPosition;
    public Transform meleeAttackPosition;

    public override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();

        Vector3 minPosition = enemyData.playerCheckPosition.position + new Vector3(enemyData.minBoxRange.x / 2.5f * facingDirection, enemyData.minBoxRange.y / 2.5f, 0);
        Vector3 maxPosition = enemyData.playerCheckPosition.position + new Vector3(enemyData.maxBoxRange.x / 2.5f * facingDirection, enemyData.maxBoxRange.y / 2.5f, 0);
        Gizmos.DrawWireCube(minPosition, enemyData.minBoxRange);
        Gizmos.DrawWireCube(maxPosition, enemyData.maxBoxRange);

        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttack.radiusRange);
    }

    protected override void Awake()
    {
        base.Awake();

        enemyIdleState = new E2_IdleState(this, enemyData, "idle", this);
        enemyMoveState = new E2_MoveState(this, enemyData, "move", this);
        enemyPlayerDetectedState = new E2_PlayerDetectedState(this, enemyData, "playerDetected", this);
        enemyMeleeAttackState = new E2_MeleeAttackState(this, enemyData, "meleeAttack", this, meleeAttack);
        enemyLookForPlayerState = new E2_LookForPlayerState(this, enemyData, "lookForPlayer", this);
        enemyRangeAttackState = new E2_RangeAttackState(this, enemyData, "rangeAttack", this);
        enemyAttackedState = new E2_AttackedState(this, enemyData, "attacked", this);
        enemyDeathState = new E2_DeathState(this, enemyData, "die", this);
        enemyDodgeState = new E2_DodgeState(this, enemyData, "dodge", this);
    }

    protected override void Start()
    {
        base.Start();

        enemyStateMachine.InitializeStartState(enemyMoveState);
    }

    public override void HandleOnAttackedEvent()
    {
        base.HandleOnAttackedEvent();

        if (enemyStateMachine.currentState is EnemyDeathState) return;
        enemyStateMachine.ChangeState(enemyAttackedState);
    }

    public override void HandleOnDeathEvent()
    {
        base.HandleOnDeathEvent();

        enemyStateMachine.ChangeState(enemyDeathState);
    }
}
