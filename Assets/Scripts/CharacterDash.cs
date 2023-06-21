using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDash : MonoBehaviour
{

    public float dashForce = 5f;
    public float dashDuration = 0.25f;
    public float dashCooldown = 1f;
    private float dashTimer;
    private float cooldownTimer;
    private bool isDashing = false;
    private bool isCooldown = false;

    public Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(isCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if(cooldownTimer<=0)
            {
                isCooldown = false;
            }
        }
        if(Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && !isCooldown)
        {
            StartDash();
        }
        if(isDashing)
        {
            dashTimer -= Time.deltaTime;
            if(dashTimer <= 0)
            {
                StopDash();
            }
        }
    }
    private void FixedUpdate(){
        if(isDashing)
        {
            Vector3 dashDirection = body.velocity.normalized;
            body.AddForce(dashDirection * dashForce, ForceMode2D.Impulse);
        }
    }
     private void StartDash()
    {
        isDashing = true;
        dashTimer = dashDuration;
        cooldownTimer = dashCooldown;
        isCooldown = true;
    }

    private void StopDash()
    {
        isDashing = false;
        body.velocity = Vector3.zero;
    }
}
