using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _maxDist;
    [SerializeField] private float _minDist;
    [SerializeField] private Transform _lookTarget;
    [SerializeField] private Transform _pivot;
    [SerializeField] private float _scrollStepDist;
    [SerializeField] private float _sensitivity;
    [SerializeField] private float _moveAccelaration;
    [SerializeField] private bool _debug;
    private PlayerInput _playerInput;
    private int layerMask;
    
    public Vector2 offset;
    public bool getInput;
    public float dist;
    private Vector2 _deltaMouse;

    private void OnValidate()
    {
        _lookTarget.transform.localPosition = new Vector3(offset.x, offset.y, 0);
    }

    private void Awake(){
        //set default values
        _playerInput = new PlayerInput();
        layerMask = 1<<3;
        layerMask = ~layerMask;

        //set listeners for player input
        _playerInput.PlayerCamera.Look.performed += OnLook;
        _playerInput.PlayerCamera.Scroll.performed += OnScroll;
    }

    private void Update()
    {
        //take care of lerping the carmera and look target positions
        _lookTarget.transform.localPosition = Vector3.Lerp(_lookTarget.transform.localPosition, new Vector3(offset.x, offset.y, 0),_moveAccelaration*Time.deltaTime);
        transform.localPosition = Vector3.Lerp(transform.localPosition,new Vector3(0, 0, dist),_moveAccelaration*Time.deltaTime);
        //check for collisions
        CheckCollision();
        if (_debug)
            Debug.DrawLine(_lookTarget.transform.position, -transform.forward*-dist+_lookTarget.transform.position,Color.blue);
    }

    //change camera distance depending on scroll input
    private void OnScroll(InputAction.CallbackContext context)
    {
        float scroll = Mathf.Sign(context.ReadValue<Vector2>().y);
        dist += _scrollStepDist * scroll;
        dist = Mathf.Clamp(dist, -_maxDist, -_minDist);
    }

    //check if there are collisions between player and camera, puting camera at collision point 
    private void CheckCollision()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(_lookTarget.transform.position, -transform.forward, out hitInfo, -dist, layerMask))
        {
            transform.localPosition = new Vector3(0, 0, -hitInfo.distance);
        }
    }

    //called when mouse position changes
    private void OnLook(InputAction.CallbackContext context){
        //get mouse input and rotate camera appropriately
        if (getInput)
        {
            _deltaMouse += context.ReadValue<Vector2>()*_sensitivity;
        }
        _pivot.transform.rotation = Quaternion.Euler(0, _deltaMouse.x, 0);
        _deltaMouse.y = Mathf.Clamp(_deltaMouse.y, -80, 80);
        _lookTarget.transform.rotation = Quaternion.Euler(-_deltaMouse.y,_deltaMouse.x , 0);
    }

    private void OnEnable(){
        _playerInput.Enable();
    }

    private void OnDisable(){
        _playerInput.Disable();
    }
}
