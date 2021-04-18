using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;
using UnityEngine.Events;

public class Arrow : MonoBehaviour
{

    [SerializeField] private LayerMask whatIsTarget;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform targetCheckPosition;
    [SerializeField] private float targetCheckRadius;
    [SerializeField] private float hittedTimeDisable;

    public EnemyStatsController shooterStatsController;
    public ArrowShooting_SO arrowShootingDefinition;

    private Rigidbody2D rigid;
    private float angle;

    private bool hasHitted = false;
    private bool hitGround = false;

    private Collider2D target;
    private float hittedTime = 0f;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        hitGround = false;
        hasHitted = false;
    }

    private void Update()
    {
        if (!hasHitted)
        {
            angle = Mathf.Atan2(rigid.velocity.y, rigid.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);

            if (hitGround)
            {
                hasHitted = true;
                hittedTime = Time.time;

                rigid.gravityScale = 0;
                rigid.velocity = Vector2.zero;
            }
            else if (target != null)
            {
                hasHitted = true;
                hittedTime = Time.time;

                rigid.gravityScale = 0;
                rigid.velocity = Vector2.zero;
                transform.parent = target.gameObject.transform;
                rigid.isKinematic = true;

                CreateAttack();
            }
        }
        
        if(hasHitted && Time.time > hittedTime + hittedTimeDisable)
        {
            Destroy(gameObject);
        }
        
    }

    private void FixedUpdate()
    {
        if (!hasHitted)
        {
            target = Physics2D.OverlapCircle(targetCheckPosition.position, targetCheckRadius, whatIsTarget);
            hitGround = Physics2D.OverlapCircle(targetCheckPosition.position, targetCheckRadius, whatIsGround);
        }
    }

    private void CreateAttack()
    {
        PlayerStatsController targetStatsController = target.gameObject.GetComponent<PlayerStatsController>();
        PlayerAttackController targetAttackHandler = target.gameObject.GetComponent<PlayerAttackController>();

        if (targetStatsController == null || targetAttackHandler == null) return;
      
        Attack attack = arrowShootingDefinition.EnemyCreateAttack(shooterStatsController, targetStatsController , arrowShootingDefinition.hitBackForce);
        targetAttackHandler.OnAttacked(this.gameObject, attack);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(targetCheckPosition.position, targetCheckRadius);
    }
}
