using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    public float walkSpeed = 40f;  // Default walk speed
    public float runSpeedMultiplier = 2.5f;  // Multiplier for run speed
    private float speed;  // Current speed

    bool crouch = false;
    bool jump = false;

    float horizontalMove = 0f;

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");

        // Set speed based on whether running or walking
        speed = walkSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("isRunning", true);
            speed *= runSpeedMultiplier;
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        // Update animator parameters
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove) * speed);

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("Jumping", true);
        }
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
    }

    public void OnLanding()
    {
        animator.SetBool("Jumping", false);
    }

    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    void FixedUpdate()
    {
        controller.Move(horizontalMove * speed * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
