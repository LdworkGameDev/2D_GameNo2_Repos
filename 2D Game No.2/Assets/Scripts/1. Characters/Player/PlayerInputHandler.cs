using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInputHandler : MonoBehaviour
{
    public float xMove { get; private set; }
    public bool jumpInput { get; private set; }
    public bool dashInput { get; private set; }
    public bool attackInput { get; private set; }

    [SerializeField] private float jumpInputDelayTime = 0f;
    [SerializeField] private float dashCoolDown = 0f;

    private float jumpInputDelayCounter = 0f;
    private float dashCoolDownCounter = 0f;

    private void Update()
    {
        xMove = 0f;
        if (PlayerManager.canReceivePlayerActionInput == false) return;

        xMove = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            jumpInput = true;
            jumpInputDelayCounter = Time.time;
        }
        CheckJumpInputHoldTime();

        if (Input.GetButtonDown("Dash") && CheckDashCoolDown())
        {
            dashInput = true;
            dashCoolDownCounter = Time.time;
        }

        if (Input.GetButtonDown("Attack") && PlayerManager.canReceiveAttackInput)
        {
            attackInput = true;
        }
    }

    public void UseJumpInput() => jumpInput = false;
    public void UseDashInput() => dashInput = false;
    public void UseAttackInput() => attackInput = false;

    private void CheckJumpInputHoldTime()
    {
        if (jumpInputDelayCounter + jumpInputDelayTime < Time.time)
        {
            UseJumpInput();
        }
    }

    private bool CheckDashCoolDown()
    {
        return (dashCoolDownCounter + dashCoolDown < Time.time) ? true : false;
    }
}
