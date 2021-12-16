using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    [SerializeField] GameObject lookTarget;
    CharacterController characterControler;
    PlayerEventManager playerEventManager;
    Vector2 moveBindDir;
    Vector2 forwadDirection;
    Vector2 rightDirection;
    Vector2 moveDirection;
    private float moveSpeed;
    private float velocityY;
    private float g;

    private void Awake()
    {
        moveSpeed = playerStats.walkSpeed;
        velocityY = 0f;
        Mathf.Clamp(velocityY, -67,67);
        g = -9.8f;
        moveBindDir = new Vector2(0, 0);
        playerEventManager = FindObjectOfType<PlayerEventManager>();
        playerStats = FindObjectOfType<PlayerStats>();
        characterControler = FindObjectOfType<CharacterController>();

    }
    private void Start()
    {
        playerEventManager.moveChange += GetMoveDir;
        playerEventManager.jumpStart += Jump;
        playerEventManager.runToglle += RunToggle;
    }

    void Update()
    {
        if (!characterControler.isGrounded)
            velocityY += g * Time.deltaTime;
        forwadDirection = new Vector2(lookTarget.transform.forward.z, lookTarget.transform.forward.x).normalized;
        rightDirection = new Vector2(lookTarget.transform.right.z, lookTarget.transform.right.x).normalized;
        moveDirection = forwadDirection * moveBindDir.y + rightDirection * moveBindDir.x;
        characterControler.Move(new Vector3(moveDirection.y * moveSpeed * Time.deltaTime, velocityY*Time.deltaTime, moveDirection.x * moveSpeed * Time.deltaTime));
    }

    private void GetMoveDir(Vector2 dir)
    {
        moveBindDir = dir;
    }

    private void Jump()
    {
        if(characterControler.isGrounded)
            velocityY = Mathf.Sqrt(-2 * g * playerStats.jumpHeight);
    }

    private void RunToggle()
    {
        if (moveSpeed == playerStats.walkSpeed)
            moveSpeed = playerStats.runSpeed;
        else
            moveSpeed = playerStats.walkSpeed;
    }
}
