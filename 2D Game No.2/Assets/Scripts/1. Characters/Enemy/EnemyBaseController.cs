using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public abstract class EnemyBaseController : MonoBehaviour
{
    public Rigidbody2D rigid { get; private set; }
    public EnemyStateMachine enemyStateMachine { get; private set; }
    public Animator enemyAnim { get; private set; }
    public EnemyAnimationTrigger animationTrigger { get; private set; }
    public ItemSpawner spawner;
    public EnemyAttackController attackController;

    public EnemyData enemyData;
    public EnemyStatsController statsController;

    public int facingDirection { get; private set; }

    protected virtual void Awake()
    {
        enemyStateMachine = new EnemyStateMachine();
    }

    protected virtual void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        enemyAnim = GetComponent<Animator>();
        animationTrigger = GetComponent<EnemyAnimationTrigger>();
        statsController = GetComponent<EnemyStatsController>();
        attackController = GetComponent<EnemyAttackController>();
     
        facingDirection = 1;
        statsController.OnAttackedEvent.AddListener(HandleOnAttackedEvent);
        statsController.OnDeathEvent.AddListener(HandleOnDeathEvent);
        attackController.OnAttackedDirectionEvent.AddListener(HandleOnAttackedDirectionEvent);
    }

    private void Update()
    {
        enemyStateMachine.currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        enemyStateMachine.currentState.PhysicsUpdate();
    }



    public virtual void SetVelocity(Vector2 velocity)
    {
        rigid.velocity = velocity;
    }

    public virtual void SetVelocity(Vector2 velocity, int direction)
    {
        rigid.velocity = (velocity * new Vector2(direction, 1));
    }

    public void Flip()
    {
        facingDirection *= -1;

        Vector2 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    public virtual bool CheckForGround()
    {
        return Physics2D.OverlapCircle(enemyData.groundCheckPosition.position, enemyData.groundCheckRadius, enemyData.whatIsGround);
    }

    public virtual bool CheckForWall()
    {
        return Physics2D.Raycast(enemyData.wallCheckPosition.position, Vector2.right * facingDirection, enemyData.wallCheckDistance, enemyData.whatIsGround);
    }

    public virtual bool CheckForLedge()
    {
        return Physics2D.Raycast(enemyData.ledgeCheckPosition.position, Vector2.down , enemyData.ledgeCheckDistance, enemyData.whatIsGround);
    }

    public virtual bool CheckForPlayerInMinRangeLine()
    {
        return Physics2D.Raycast(enemyData.playerCheckPosition.position, Vector2.right * facingDirection, enemyData.minDistanceLine, enemyData.whatIsPlayer);
    }

    public virtual bool CheckForPlayerInMaxRangeLine()
    {
        return Physics2D.Raycast(enemyData.playerCheckPosition.position, Vector2.right * facingDirection, enemyData.maxDistanceLine, enemyData.whatIsPlayer);
    }

    public virtual bool CheckForPlayerInMinBoxRange()
    {
        Vector3 position = enemyData.playerCheckPosition.position + new Vector3(enemyData.minBoxRange.x / 2.5f * facingDirection, enemyData.minBoxRange.y / 2.5f, 0);
        return Physics2D.OverlapBox(position, enemyData.minBoxRange, 0, enemyData.whatIsPlayer);
    }

    public virtual bool CheckForPlayerInMaxBoxRange()
    {
        Vector3 position = enemyData.playerCheckPosition.position + new Vector3(enemyData.maxBoxRange.x / 2.5f * facingDirection, enemyData.maxBoxRange.y / 2.5f, 0);
        return Physics2D.OverlapBox(position, enemyData.maxBoxRange, 0, enemyData.whatIsPlayer);
    }

    public virtual bool CheckForPlayerInShortRangeAction()
    {
        return Physics2D.OverlapCircle(enemyData.SRADetectPosition.position, enemyData.SRADetectRadius, enemyData.whatIsPlayer);
    }

    public virtual bool CheckForFlipToFacePlayer()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) return false;

        float position = player.transform.position.x - gameObject.transform.position.x;
        
        if((position > 0 && facingDirection == -1) || (position < 0 && facingDirection == 1))
        {
            return true;
        }

        return false;
    }

    public GameObject FindPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            return player;
        }
        return null;
    }

    public virtual void HandleOnAttackedEvent()
    {

    }

    public virtual void HandleOnAttackedDirectionEvent(int direction)
    {
        if ((direction == 1 && facingDirection == 1) || (direction == -1 && facingDirection == -1))
        {
            Flip();
        }
    }

    public virtual void HandleOnDeathEvent()
    {

    }

    public virtual void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(enemyData.wallCheckPosition.position, enemyData.wallCheckPosition.position + (Vector3)(Vector2.right * facingDirection * enemyData.wallCheckDistance));
        Gizmos.DrawLine(enemyData.ledgeCheckPosition.position, enemyData.ledgeCheckPosition.position + (Vector3)(Vector2.down * enemyData.ledgeCheckDistance));
        Gizmos.DrawWireSphere(enemyData.SRADetectPosition.position, enemyData.SRADetectRadius);
    }
}
