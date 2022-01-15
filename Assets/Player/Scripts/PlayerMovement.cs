using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float airResistance;
    [SerializeField] private float maxWalkAngle;
    [SerializeField] private float g;
    [Space]
    [SerializeField] private bool debug;
    private CharacterController characterController;
    private PlayerStats playerStats;
    private PlayerInputManager playerInputManager;
    private Camera mainCamera;
    private Vector3 groundNormal;
    private Vector3 moveDirection;
    private Vector2 moveInput;
    private Vector3 pushDown;
    private Vector3 velocity;
    private Vector3 lastVelocity;
    private Vector3 accelerationVector;
    private Vector3 gravityDirection;
    private Vector3 targetVelocity;
    private Vector3 targetGravityVelocity;
    private Vector2 aerialMoveVelocity;
    private bool isJumping;
    private float accelaration;
    private float groundAngle;
    private float gravityAcceleration;
    private float moveSpeed;
    private float targetMoveSpeed;
    private float targetGravitySpeed;
    private float jumpVelocity;


    private void OnValidate()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        playerInputManager = FindObjectOfType<PlayerInputManager>();
        mainCamera = FindObjectOfType<Camera>();
        characterController = FindObjectOfType<CharacterController>();
    }

    private void Awake()
    {
        //listen to Inputs
        playerInputManager.moveChange += GetInput;
        playerInputManager.runToglle += RunToggle;
        playerInputManager.jumpStart += Jump;
        //set default values
        targetMoveSpeed = playerStats.walkSpeed;
        accelaration = 0f;
        groundNormal = Vector3.zero;
        moveDirection = Vector3.zero;
        moveInput = Vector3.zero;
        pushDown = Vector3.zero;
        velocity = Vector3.zero;
        lastVelocity = Vector3.zero;
        accelerationVector = Vector3.zero;
        targetVelocity = Vector3.zero;
        aerialMoveVelocity = Vector2.zero;
        groundAngle = 90;
        moveSpeed = playerStats.walkSpeed;
        jumpVelocity = 0;
        isJumping = false;
    }

    void FixedUpdate()
    {
        targetMoveSpeed = moveSpeed;
        GetGroundAngle();
        GetMovementDirection();
        if (groundAngle >= maxWalkAngle)
            GetGravityVelocity();
        GetVelocity();
        if (debug)
        {
            DrawDebugLines();
        }
        //move the player
        characterController.Move((velocity + pushDown) * Time.deltaTime);
        RotateCharacter();
    }

    //when character controller hits something
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
            groundNormal = hit.normal;      
    }

    //listens to Input manager's input change
    private void GetInput(Vector2 input)
    {
        moveInput = input;
    }


    //gets the movement direction based on input and camera direction;
    private void GetMovementDirection()
    {
        Vector3 forwardDirection = mainCamera.transform.forward;
        Vector3 rightDirection = mainCamera.transform.right;
        moveDirection = moveInput.x * forwardDirection + moveInput.y * rightDirection;
        moveDirection = new Vector3(moveDirection.x, 0, moveDirection.z).normalized;
    }

    private void GetVelocity()
    {
        //when free falling, applies garvity, air resistance, and a bit of player movement
        if(characterController.collisionFlags == CollisionFlags.None)
        {
            Vector2 horizontalSpeed = Vector2.Lerp(new Vector2(velocity.x-aerialMoveVelocity.x, velocity.z-aerialMoveVelocity.y), Vector2.zero, airResistance / new Vector2(velocity.x, velocity.z).magnitude * Time.deltaTime);
            float verticalSpeed = Mathf.Lerp(velocity.y, -67, g * Time.deltaTime / (velocity.y + 67));
            targetVelocity = moveDirection * playerStats.walkSpeed * playerStats.arealControl;
            if (targetVelocity.magnitude > aerialMoveVelocity.magnitude)
                accelaration = playerStats.accelaration;
            else
                accelaration = playerStats.stopAccelaration;
            aerialMoveVelocity = Vector2.Lerp(aerialMoveVelocity, new Vector2(targetVelocity.x, targetVelocity.z), accelaration * Time.deltaTime / (aerialMoveVelocity - new Vector2(targetVelocity.x, targetVelocity.z)).magnitude);
            velocity = new Vector3(horizontalSpeed.x+aerialMoveVelocity.x, verticalSpeed, horizontalSpeed.y+aerialMoveVelocity.y);
            return;
        }
        //sets movement vector's Y component to "zero"
        if ((characterController.collisionFlags & CollisionFlags.Above) !=0)
        {
            velocity = new Vector3(velocity.x, 0, velocity.z);
        }

        //determines ground slope apllying a transformation to the move direction determining velocity's magnitude based on the slope
        if ((characterController.collisionFlags & CollisionFlags.Below) != 0)
        {
            aerialMoveVelocity = Vector2.zero;
            pushDown = -groundNormal  * 0.1f;
            float planeY = (-groundNormal.x * moveDirection.x - groundNormal.z * moveDirection.z) / groundNormal.y;
            if (planeY < 0) 
            {
                moveDirection = new Vector3(moveDirection.x, planeY, moveDirection.z).normalized;
                targetVelocity = moveDirection * targetMoveSpeed;
            }
            else
            {
                targetVelocity = moveDirection * targetMoveSpeed * Mathf.Pow(Mathf.Cos(groundAngle/maxWalkAngle),2);
            }
        }
        //determines if the player what acceleration and target velocity the player will get based on the ground slope and if its accelerationg or decelarating
        if (groundAngle>=maxWalkAngle)
        {
            accelaration = gravityAcceleration;
            targetVelocity = targetGravityVelocity;
        }
        else
        {
            if (targetVelocity.magnitude > velocity.magnitude&&Vector3.Angle(velocity,targetVelocity)<45)
                accelaration = playerStats.accelaration;
            else
                accelaration = playerStats.stopAccelaration;
        }
        velocity = Vector3.Lerp(velocity, targetVelocity, accelaration/(velocity-targetVelocity).magnitude * Time.deltaTime);
        accelerationVector = (velocity - lastVelocity) / Time.deltaTime;
        lastVelocity = velocity;
        if (isJumping)
        {
            velocity = new Vector3(velocity.x, jumpVelocity, velocity.z);
            isJumping = false;
        }
    }


    //gets the gravity velocity, depends on slopes
    private void GetGravityVelocity()
    {
        gravityDirection = Vector3.Slerp(groundNormal, Vector3.down, 90 / (180 - groundAngle));
        gravityAcceleration = g * Mathf.Sin(Mathf.Deg2Rad * groundAngle);
        targetGravitySpeed = 67 * Mathf.Sin(Mathf.Deg2Rad * groundAngle);
        targetGravityVelocity = gravityDirection * targetGravitySpeed;
    }

    //gets the ground angle
    private void GetGroundAngle()
    {
        if (!characterController.isGrounded)
        {
            groundAngle = 90;
            return;
        }
        groundAngle = Vector3.Angle(Vector3.up, groundNormal);
    }

    //sets jump velocity and sets is jumping to true
    private void Jump()
    {
        if (characterController.isGrounded && groundAngle < maxWalkAngle)
        {
            jumpVelocity = Mathf.Sqrt(2 * g * playerStats.jumpHeight);
            isJumping = true;
        }
    }

    //changes player's max speed depending on if the run button is pressed
    private void RunToggle(bool run)
    {
        if (run)
            moveSpeed = playerStats.runSpeed;
        else
            moveSpeed = playerStats.walkSpeed;
    }

    //rotate character to movement direction
    private void RotateCharacter()
    {
        float control = 1;
        if (!characterController.isGrounded)
        {
            control = playerStats.arealControl;
        }
        float Angle = Vector3.Angle(transform.forward, new Vector3(moveDirection.x,0,moveDirection.z));
        if (Vector3.Cross(transform.forward, new Vector3(moveDirection.x, 0, moveDirection.z)).y < 0)
        Angle *= -1;
        Vector3 DeltaYRotation = new Vector3(0, Angle, 0) * Time.deltaTime * playerStats.turnSpeed*control;
        characterController.transform.Rotate(DeltaYRotation);
        //body.transform.rotation =Quaternion.Lerp(body.transform.rotation, Quaternion.Euler(new Vector3(accelerationVector.z, 0, -accelerationVector.x)),Time.deltaTime);
    }
    //draw debug lines 
    private void DrawDebugLines()
    {
        Debug.DrawLine(transform.position + Vector3.up, transform.position + targetVelocity*2 + Vector3.up, Color.green, 0);
        Debug.DrawLine(transform.position + Vector3.up, transform.position + targetGravityVelocity/4 + Vector3.up, Color.red, 0);
    }
}