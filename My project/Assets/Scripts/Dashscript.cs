using System.Collections;
using UnityEngine;

public class Dashscript : MonoBehaviour
{
    [SerializeField] private float DashingVelocity = 14f;
    [SerializeField] private float DashingTime = 0.5f;
    [SerializeField] private float dashCooldown = 1.0f; // Cooldown duration

    private Vector2 DashingDirection;
    public Animator animator;

    private bool isDashing = false;
    private bool canDash = true; // Indicates whether the player can dash
    private bool isCooldown = false; // Indicates whether the dash is on cooldown
    private TrailRenderer trail;
    private Rigidbody2D rb;
    private float lastDashTime = 0f; // Store the time of the last dash

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trail = GetComponent<TrailRenderer>();
    }

    void Update()
    {
        if (!isCooldown && Input.GetKeyDown(KeyCode.Mouse1) && canDash)
        {
            isDashing = true;
            canDash = false;
            isCooldown = true; // Start cooldown
            trail.emitting = true;

            // Get the direction based on the player's rotation
            Vector3 playerForward = transform.right.normalized; // Assuming the player moves along its right side
            DashingDirection = new Vector2(playerForward.x, playerForward.y).normalized;

            lastDashTime = Time.time; // Store the time of the dash
            StartCoroutine(StopDashing());
        }

        // Update animator parameter
        animator.SetBool("isDashing", isDashing);
        
        if (isDashing)
        {
            rb.velocity = DashingDirection * DashingVelocity;
        }
    }

    IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(DashingTime);
        trail.emitting = false;
        isDashing = false;

        // After the dash duration, start the cooldown
        StartCoroutine(DashCooldown());
    }

    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);
        canDash = true; // Reset canDash after cooldown
        isCooldown = false; // Reset cooldown flag
    }
}
