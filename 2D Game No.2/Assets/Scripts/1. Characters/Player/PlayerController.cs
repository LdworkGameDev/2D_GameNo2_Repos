using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator playerAnim { get; private set; }
    public Rigidbody2D rigid { get; private set; }
    public PlayerStateMachine playerStateMachine { get; private set; }
    public PlayerInputHandler playerInputHandler { get; private set; }
    public PlayerAttackController attackController { get; private set; }

    public PlayerIdleState playerIdleState { get; private set; }
    public PlayerMoveState playerMoveState { get; private set; }
    public PlayerLandState playerLandState { get; private set; }

    public PlayerJumpState playerJumpState { get; private set; }
    public PlayerDashState playerDashState { get; private set; }
    public PlayerAttack1State playerAttack1State { get; private set; }
    public PlayerAttack2State playerAttack2State { get; private set; }

    public PlayerAttackState playerPrimaryAttackState { get; private set; }
    public PlayerAttackState playerSecondaryAttackState { get; private set; }

    public PLayerInAirState playerInAirState { get; private set; }

    public PlayerAttackedState playerAttackedState { get; private set; }

    public PlayerDeathState playerDeathState { get; private set; }


    public bool isFacingRight { get; private set; }

    public PlayerStatsController statsController;
    public MeleeAttackCircle_SO meleeAttack1;
    public MeleeAttackBox_SO meleeAttack2;

    [SerializeField] private PlayerData playerData;


    private void Awake()
    {
        playerStateMachine = new PlayerStateMachine();

        playerIdleState = new PlayerIdleState(this, playerData, "idle");
        playerMoveState = new PlayerMoveState(this, playerData, "move");
        playerLandState = new PlayerLandState(this, playerData, "land");

        playerJumpState = new PlayerJumpState(this, playerData, "inAir");
        playerDashState = new PlayerDashState(this, playerData, "dash");
        playerAttack1State = new PlayerAttack1State(this, playerData, "attack1");
        playerAttack2State = new PlayerAttack2State(this, playerData, "attack2");

        playerPrimaryAttackState = new PlayerAttackState(this, playerData, "attack");
        playerSecondaryAttackState = new PlayerAttackState(this, playerData, "attack");

        playerInAirState = new PLayerInAirState(this, playerData, "inAir");

        playerAttackedState = new PlayerAttackedState(this, playerData, "attacked");

        playerDeathState = new PlayerDeathState(this, playerData, "die");
    }

    private void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerInputHandler = GetComponent<PlayerInputHandler>();
        rigid = GetComponent<Rigidbody2D>();
        attackController = GetComponent<PlayerAttackController>();

        playerStateMachine.InitializeStartState(playerIdleState);
        statsController = GetComponent<PlayerStatsController>();

        isFacingRight = true;
        attackController.OnAttackedDirectionEvent.AddListener(HandleOnAttackedDirectionEvent);
        statsController.OnPlayerAttackedEvent.AddListener(HandleOnAttackedEvent);
        statsController.OnPlayerDeathEvent.AddListener(HandleOnDeathEvent);
    }

    private void Update()
    {
        playerStateMachine.currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        playerStateMachine.currentState.PhysicsUpdate();
    }



    #region Set Functions

    public void SetPlayerVelocityX(float xVelocity)
    {
        rigid.velocity = new Vector2(xVelocity, rigid.velocity.y);
    }

    public void SetPlayerVelocityY(float yVelocity)
    {
        rigid.velocity = new Vector2(rigid.velocity.x, yVelocity);
    }

    public void SetPlayerVelocity(Vector2 velocity)
    {
        rigid.velocity = velocity;
    }

    public void SetAnimtionStart() => playerStateMachine.currentState.SetAnimationStart();

    public void SetAnimationFinish() => playerStateMachine.currentState.SetAnimationFinish();

    public void SetAbilityDone()
    {
        if (!(playerStateMachine.currentState is PlayerAbilityState)) return;

        ((PlayerAbilityState)playerStateMachine.currentState).SetAbilityDone();
    }

    #endregion



    #region Check Functions

    public void CheckForFlip(float xMove)
    {
        if (xMove < 0 && isFacingRight)
        {
            Flip();
        }
        if (xMove > 0 && !isFacingRight)
        {
            Flip();
        }
    }

    public void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector2 tempScale = transform.localScale;
        tempScale.x *= -1;
        transform.localScale = tempScale;
    }

    public bool CheckForGround()
    {
        return Physics2D.OverlapCircle(playerData.groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }

    #endregion



    #region Other Functions

    public void CanDoubleAttack()
    {
        if (!(playerStateMachine.currentState is PlayerAttack1State)) return;

        if (((PlayerAttack1State)playerStateMachine.currentState).CanDoubleAttack())
        {
            playerStateMachine.ChangeState(playerAttack2State);
        }
    }

    public void TriggerAttack()
    {
        if (!(playerStateMachine.currentState is PlayerAbilityState)) return;

        ((PlayerAbilityState)playerStateMachine.currentState).TriggerAttack();
    }

    public void HandleOnAttackedEvent()
    {
        if (playerStateMachine.currentState is PlayerDeathState) return;

        playerStateMachine.ChangeState(playerAttackedState);
    }

    public void HandleOnAttackedDirectionEvent(int direction)
    {
        if ((direction == 1 && isFacingRight) || (direction == -1 && !isFacingRight))
        {
            Flip();
        }
    }

    public void HandleOnDeathEvent()
    {
        playerStateMachine.ChangeState(playerDeathState);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(playerData.groundCheck.position, playerData.groundCheckRadius);
        Gizmos.DrawWireSphere(playerData.attack1Position.position, meleeAttack1.radiusRange);
        Gizmos.DrawWireCube(playerData.attack2Position.position, new Vector2(meleeAttack2.rangeX, meleeAttack2.rangeY));
    }

    #endregion
}
