using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{


    [SerializeField] private float _airResistance;
    [SerializeField] public float g;
    [SerializeField] private Transform _body;
    [Space]
    [SerializeField] private bool _debug;

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
    private ControllerColliderHit _hit;
    private float _targetMoveSpeed;

    private Camera _mainCamera;
    private Vector3 _groundNormal;
    private Vector3 _moveDirection;
    private Vector2 _moveInput;
    private Vector3 _pushDown;
    private Vector3 _lastVelocity;
    private Vector3 _accelerationVector;
    private Vector3 _gravityDirection;
    private Vector3 _targetVelocity;
    private Vector3 _targetGravityVelocity;
    private Vector2 _aerialMoveVelocity;
    private float _accelaration;
    private float _groundAngle;
    private float gravityAcceleration;
    private float moveSpeed;
    private float targetMoveSpeed;
    private float targetGravitySpeed;
    private float jumpVelocity;

    //getters and setters
    public Vector2 MoveInputDir { get { return _moveInputDir; } }
    public Vector3 MoveDirection { get { return _moveDirection; } set { _moveDirection = value; } }
    public bool IsMovePressed { get { return _isMovePressed; } }
    public bool IsRunPressed { get { return _isRunPressed; } }
    public bool IsCrouchPressed { get { return _isCrouchPressed; } }
    public bool IsGrounded { get { return _characterController.isGrounded; } }
    public PlayerState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public Vector3 Velocity { get { return _velocity; } set { _velocity = value; } }
    public float AirResistance { get { return _airResistance; } }
    public Vector3 TargetVelocity { get { return _targetVelocity; } set { _targetVelocity = value; } }
    public float Accelaration { get { return _accelaration; } set { _accelaration = value; } }
    public CharacterController CharacterController { get { return _characterController; } }
    public ControllerColliderHit Hit { get { return _hit; } }
    public Vector3 PushDown { get { return _pushDown; } set { _pushDown = value; } }
    public float TargetMoveSpeed { get { return _targetMoveSpeed; } set { _targetMoveSpeed = value; } }
    public Vector3 GroundNormal { get { return _groundNormal; } set { _groundNormal = value; } }
    public float GroundAngle { get { return _groundAngle; } set { _groundAngle = value; } }

    private void Awake(){
        //set default value
        _mainCamera = FindObjectOfType<Camera>();
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

        //set listeners for player input
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
        GetMovementDirection();
        _currentState.UpdateStates();
        _characterController.Move(_velocity + _pushDown * Time.deltaTime);
        //RotateCharacter();
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

    //gets the movement direction based on input and camera direction
    private void GetMovementDirection()
    {
        Vector3 forwardDirection = _mainCamera.transform.forward;
        Vector3 rightDirection = _mainCamera.transform.right;
        _moveDirection = _moveInputDir.y * forwardDirection + _moveInputDir.x * rightDirection;
        _moveDirection = new Vector3(_moveDirection.x, 0, _moveDirection.z).normalized;
    }

    //rotate character to movement direction
    private void RotateCharacter()
    {
        transform.forward = Vector3.Slerp(transform.forward, new Vector3(_moveDirection.x, 0, _moveDirection.z).normalized, PlayerStats.turnSpeed * Time.deltaTime);

        _accelerationVector = _body.transform.InverseTransformDirection(_accelerationVector);
        _body.transform.rotation = Quaternion.Euler(_body.transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, _body.transform.rotation.eulerAngles.z);
        _body.transform.rotation = Quaternion.Lerp(_body.transform.rotation, Quaternion.Euler(new Vector3(_accelerationVector.z * PlayerStats.tiltFactor, _body.transform.rotation.eulerAngles.y, -_accelerationVector.x * PlayerStats.tiltFactor)), PlayerStats.tiltSpeed * Time.deltaTime);
    }


    private void OnEnable(){
        _playerInput.PlayerMovement.Enable();
    }

     private void OnDisable(){
        _playerInput.PlayerMovement.Disable();
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        _hit = hit;
    }
}
