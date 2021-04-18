using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_Controller : EnemyBaseController
{
    public E1_IdleState enemyIdleState { get; private set; }
    public E1_MoveState enemyMoveState { get; private set; }
    public E1_PlayerDetectedState enemyPlayerDetectedState { get; private set; }
    public E1_ChargeState enemyChargeState { get; private set; }
    public E1_LookForPlayerState enemyLookForPlayerState { get; private set; }
    public E1_MeleeAttackState enemyMelleAttackState { get; private set; }
    public E1_StunState enemyStunState { get; private set; }
    public E1_AttackedState enemyAttackedState { get; private set; }
    public E1_DeathState enemyDeathState { get; private set; }

    public Transform SRADetectPosition;
    public MeleeAttackCircle_SO meleeAttack;

    protected override void Awake()
    {
        base.Awake();

        enemyIdleState = new E1_IdleState(this, enemyData, "idle", this);
        enemyMoveState = new E1_MoveState(this, enemyData, "move", this);
        enemyPlayerDetectedState = new E1_PlayerDetectedState(this, enemyData, "playerDetected", this);
        enemyChargeState = new E1_ChargeState(this, enemyData, "charge", this);
        enemyLookForPlayerState = new E1_LookForPlayerState(this, enemyData, "lookForPlayer", this);
        enemyMelleAttackState = new E1_MeleeAttackState(this, enemyData, "shortAction", this, meleeAttack);
        enemyStunState = new E1_StunState(this, enemyData, "stun", this);
        enemyAttackedState = new E1_AttackedState(this, enemyData, "attacked", this);
        enemyDeathState = new E1_DeathState(this, enemyData, "die", this);
    }

    protected override void Start()
    {
        base.Start();

        enemyStateMachine.InitializeStartState(enemyMoveState);
    }

    public override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();

        Gizmos.DrawWireSphere(SRADetectPosition.position, meleeAttack.radiusRange);
        Gizmos.DrawWireSphere(enemyData.playerCheckPosition.position + new Vector3(enemyData.minDistanceLine * facingDirection, 0, 0), 0.2f);
        Gizmos.DrawWireSphere(enemyData.playerCheckPosition.position + new Vector3(enemyData.maxDistanceLine * facingDirection, 0, 0), 0.2f);
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
