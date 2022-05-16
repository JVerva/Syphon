using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private float walkStepDist;
    [SerializeField] private float runStepDist;
    [SerializeField] private int animStepsPSecond;
    [SerializeField] private Animator animator;
    [Space]
    [SerializeField] private float walkAnimTreshHold;
    [SerializeField] private float walkAnimSpeed;
    [SerializeField] private float runAnimSpeed;
    private PlayerMovement playerMovementScript;
    private PlayerStats playerStats;
    private float strideLength;
    private float strideFrequency;
    private float radius;
    private float angularSpeed;
    private float speed;

    private void Awake()
    {
        playerStats = GetComponentInParent<PlayerStats>();
        playerMovementScript = GetComponentInParent<PlayerMovement>();
        walkAnimTreshHold = playerStats.walkSpeed / playerStats.runSpeed;
        walkAnimSpeed = (playerStats.walkSpeed / walkStepDist) / animStepsPSecond;
        runAnimSpeed = (playerStats.runSpeed / runStepDist) / animStepsPSecond;
    }

    //Displays 4 rays perpenducular to each other in the forward plane
    private void FixedUpdate()
    {
        speed = playerMovementScript.velocity.magnitude;
        GetStride();
        radius = 2 * strideLength / Mathf.PI;
        angularSpeed = speed / radius;
    }

    private void Update()
    {
        animator.SetFloat("speed", speed / playerStats.runSpeed);
        animator.SetBool("isGrounded", playerMovementScript.isGrounded);
        animator.SetBool("isSliding", playerMovementScript.isSliding);
    }

    //sets stride length and stride frequency based on speed
    private void GetStride()
    {
        if (speed <= playerStats.walkSpeed)
            strideLength = speed * walkStepDist / playerStats.walkSpeed;
        else
            strideLength = speed * (runStepDist - walkStepDist)/(playerStats.runSpeed - playerStats.walkSpeed)+(walkStepDist -((runStepDist - walkStepDist)/((playerStats.runSpeed - playerStats.walkSpeed) * playerStats.walkSpeed)));
        strideFrequency = speed/strideLength;
    }
}
