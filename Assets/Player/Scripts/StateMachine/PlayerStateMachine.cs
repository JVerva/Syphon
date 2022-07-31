using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    //private variables
    private CharacterController _characterController;
    private PlayerStateFactory _playerStateFactory;
    private PlayerState _currentState;
    private PlayerInput _playerInput;
    private Vector2 _moveInputDir;
    private bool _isMovePressed;
    private bool _isRunPressed;
    private bool _isCrouchPressed;
    private Vector3 _velocity;

    //getters and setters
    public Vector2 MoveInputDir { get{return _moveInputDir;} }
    public bool IsMovePressed { get {return _isMovePressed;} }
    public bool IsRunPressed { get {return _isRunPressed;} }
    public bool IsCrouchPressed { get {return _isCrouchPressed;} }
    public bool IsGrounded {get {return _characterController.isGrounded;} }
    public PlayerState CurrentState { get { return _currentState; } set { _currentState = value; } }
    private Vector3 Velocity{ get {return _velocity;} set {_velocity = Velocity;} }

    private void Awake(){
        //set default values
        _playerStateFactory = new PlayerStateFactory(this);
        _playerInput = new PlayerInput();
        _moveInputDir = new Vector2(0,0);
        _isMovePressed = false;
        _characterController = gameObject.GetComponent<CharacterController>();
        if(_characterController.isGrounded){
            _currentState = _playerStateFactory.getGroundedState();
        }else{
            _currentState = _playerStateFactory.getAerialState();
        }

        //set listeners for player inthrow new System.NotImplementedException();put
        //move
        _playerInput.PlayerMovement.Move.started += OnMove;
        _playerInput.PlayerMovement.Move.canceled += OnMove;
        _playerInput.PlayerMovement.Move.performed += OnMove;
        //run
        _playerInput.PlayerMovement.Run.started += OnRun;
        _playerInput.PlayerMovement.Run.canceled += OnRun;
        //crouch
        _playerInput.PlayerMovement.Crouch.started += OnCrouch;
        _playerInput.PlayerMovement.Crouch.canceled += OnCrouch;
        //jump
        _playerInput.PlayerMovement.Jump.started += OnJump;
        _playerInput.PlayerMovement.Jump.canceled += OnJump;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        _currentState.UpdateStates();
    }

    //called when movement input is changed or pressed
    private void OnMove(InputAction.CallbackContext context){
        _moveInputDir = context.ReadValue<Vector2>();
        if(_moveInputDir.magnitude == 0){
            _isMovePressed = false;
        }else{
            _isMovePressed = true;
        }
    }

    //called when jump input is changed
    private void OnJump(InputAction.CallbackContext context){
        
    }

    //called when run input is changed or pressed
    private void OnRun(InputAction.CallbackContext context){
        _isRunPressed = context.ReadValueAsButton();
    }
    
    //called when crouch input is changed
    private void OnCrouch(InputAction.CallbackContext context){
        _isCrouchPressed = context.ReadValueAsButton();
    }

    private void OnEnable(){
        _playerInput.PlayerMovement.Enable();
    }

     private void OnDisable(){
        _playerInput.PlayerMovement.Disable();
    }
}
