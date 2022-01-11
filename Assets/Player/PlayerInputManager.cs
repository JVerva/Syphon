using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] private KeyCode moveForward;
    [SerializeField] private KeyCode moveBackwards;
    [SerializeField] private KeyCode moveRight;
    [SerializeField] private KeyCode moveLeft;
    [SerializeField] private KeyCode Run;
    [SerializeField] private KeyCode Jump;
    [SerializeField] private KeyCode Inventory;
    [SerializeField] private KeyCode Interact;

    private Vector2 moveInput;

    //set events
    public event Action<Vector2> moveChange;
    public event Action runToglle;
    public event Action jumpStart;
    public event Action inventoryToggle;

    private void Awake()
    {
        //set default values
        moveInput = new Vector2(0, 0);
    }

    private void Update()
    {
        //search for bind input and set off the events when apropriate
        if (Input.GetKeyDown(Inventory))
        {
            inventoryToggle?.Invoke();
        }
        GetMoveInput();
        if (Input.GetKeyDown(Run) || Input.GetKeyUp(Run)) {
                runToglle?.Invoke();
        }
        if (Input.GetKeyDown(Jump)) {
                jumpStart?.Invoke();
        }
    }

    //return the movement binds in the form of a vector2
    private Vector2 GetInputDirection(bool F, bool B, bool R, bool L)
    {
        return new Vector2(BoolToFloat(F) - BoolToFloat(B), BoolToFloat(R) - BoolToFloat(L)).normalized;
    }

    //transforms boolean values to float values
    private float BoolToFloat(bool a)
    {
        if (a)
            return 1;
        else
            return 0;
    }

    //determine wether the movement bind  directions have changed, setting off the "moveChange" event when they do
    private void GetMoveInput()
    {
        Vector2 lastMoveInput = moveInput;
        moveInput = GetInputDirection(Input.GetKey(moveForward), Input.GetKey(moveBackwards), Input.GetKey(moveRight), Input.GetKey(moveLeft));
        if (lastMoveInput != moveInput)
                moveChange?.Invoke(moveInput);
        return;
    }
    
}
