using UnityEngine;

public class EnemyData : MonoBehaviour
{
    [Header("Check Stats")]
    public Transform wallCheckPosition;
    public Transform ledgeCheckPosition;
    public Transform groundCheckPosition;
    public float wallCheckDistance = 0f;
    public float ledgeCheckDistance = 0f;
    public float groundCheckRadius = 0f;
    public LayerMask whatIsGround;

    [Header("Idle Stats")]
    public float minIdleTime = 0f;
    public float maxIdleTime = 0f;

    [Header("Move State")]
    public float moveSpeed = 0f;

    [Header("Player Detect Stats")]
    public Transform playerCheckPosition;
    public float minDistanceLine = 0f;
    public float maxDistanceLine = 0f;
    public Vector2 minBoxRange;
    public Vector2 maxBoxRange;
    public LayerMask whatIsPlayer;

    // Wait time to transition to long range attack from detected player state
    public float LRActionWaitTime = 1.5f;

    [Header("Charge Stats")]
    public float chargeSpeed = 0f;
    public float chargeTime = 0f;

    [Header("Look For Player Stats")]
    public int amountOfTurns = 0;
    public float timeBetweenTurns = 0f;

    [Header("Action Stats")]
    public Transform SRADetectPosition;

    //Range radius to detect player to perform short range action
    public float SRACooldownTime = 0f;
    public float SRADetectRadius = 0f;

    [Header("Stun Stats")]
    public float stunTime = 0f;

    //Time for enemy to be hitted into the air
    public float stunHittedBackTime = 0f;
    public Vector2 stunForce;

    [Header("Dodge Stats")]
    public Vector2 dodgeForce;
    public float dodgeCooldown = 0f;
}
