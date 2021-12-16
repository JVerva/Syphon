using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventManager : MonoBehaviour
{
    [SerializeField] private KeyCode moveForward;
    [SerializeField] private KeyCode moveBackwards;
    [SerializeField] private KeyCode moveRight;
    [SerializeField] private KeyCode moveLeft;
    [SerializeField] private KeyCode Run;
    [SerializeField] private KeyCode Jump;
    private Vector2 moveDir;

    public delegate void MovementChanged(Vector2 dir);
    public event MovementChanged moveChange;
    public delegate void RunToglle();
    public event RunToglle runToglle;
    public delegate void JumpStart();
    public event JumpStart jumpStart;

    private void Awake()
    {
        moveDir = new Vector2(0, 0);
    }

    private void Update()
    {
        moveDir = Move(moveDir);
        if (Input.GetKeyDown(Run) || Input.GetKeyUp(Run))
            runToglle();
        if (Input.GetKeyDown(Jump))
            jumpStart();
    }

    Vector2 GetMovementDirection(bool F, bool B, bool R, bool L)
    {
        return new Vector2(BoolToFloat(R) - BoolToFloat(L), BoolToFloat(F) - BoolToFloat(B)).normalized;
    }

    float BoolToFloat(bool a)
    {
        if (a)
            return 1;
        else
            return 0;
    }

    Vector2 Move(Vector2 lastMoveVector)
    {
        Vector2 moveVector = GetMovementDirection(Input.GetKey(moveForward), Input.GetKey(moveBackwards), Input.GetKey(moveRight), Input.GetKey(moveLeft));
        if (lastMoveVector != moveVector)
            if (moveChange!=null)
                moveChange(moveVector);
        return moveVector;
    }
    
}
