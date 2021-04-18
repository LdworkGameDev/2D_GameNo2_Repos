using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AttackController : MonoBehaviour, IAttackable
{
    public Events.EventInteger OnAttackedDirectionEvent;
    private Rigidbody2D rigid;

    private int direction = 0;

    protected virtual void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public virtual void OnAttacked(GameObject attacker, Attack attack)
    {
        direction = (gameObject.transform.position.x > attacker.transform.position.x) ? 1 : -1;
        rigid.velocity = attack.hitBackForce * new Vector2(direction, 1);

        if (OnAttackedDirectionEvent != null)
        {
            OnAttackedDirectionEvent.Invoke(direction);
        }
    }
}
