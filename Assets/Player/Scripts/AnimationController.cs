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
    private float strideLength;
    private float strideFrequency;
    private float radius;
    private float angularSpeed;
    private float speed;

    private void Awake()
    {
        playerMovementScript = GetComponentInParent<PlayerMovement>();
        walkAnimTreshHold = PlayerStats.walkSpeed / PlayerStats.runSpeed;
        walkAnimSpeed = (PlayerStats.walkSpeed / walkStepDist) / animStepsPSecond;
        runAnimSpeed = (PlayerStats.runSpeed / runStepDist) / animStepsPSecond;
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
        animator.SetFloat("speed", speed / PlayerStats.runSpeed);
        animator.SetBool("isGrounded", playerMovementScript.isGrounded);
        animator.SetBool("isSliding", playerMovementScript.isSliding);
    }

    //sets stride length and stride frequency based on speed
    private void GetStride()
    {
        if (speed <= PlayerStats.walkSpeed)
            strideLength = speed * walkStepDist / PlayerStats.walkSpeed;
        else
            strideLength = speed * (runStepDist - walkStepDist)/(PlayerStats.runSpeed - PlayerStats.walkSpeed)+(walkStepDist -((runStepDist - walkStepDist)/((PlayerStats.runSpeed - PlayerStats.walkSpeed) * PlayerStats.walkSpeed)));
        strideFrequency = speed/strideLength;
    }
}
