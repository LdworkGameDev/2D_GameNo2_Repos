using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [Header("Move Stats")]
    public float moveSpeed = 0f;

    [Header("Jump Stats")]
    public int jumpTimes = 0;
    public float jummpForce = 0f;
    public Transform groundCheck;
    public float groundCheckRadius = 0f;
    public LayerMask whatIsGround;

    [Header("Dash Stats")]
    public float dashSpeed = 0f;
    public float dashTime = 0f;

    [Header("Attack Stats")]
    public LayerMask whatIsEnemy;
    public Transform attack1Position;
    public Transform attack2Position;
    public float doubleAttackTime = 0f;
}


