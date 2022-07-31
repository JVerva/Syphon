using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private Vector2 _cameraOffset;
    [SerializeField] private float _cameraDist;
    private PlayerInput _playerInput;
    private CameraController _cameraController;
    private InventoryManager _inventoryManager;
    private Vector2 _lastCameraOffset;
    private float _lastCameraDist;

    private void OnValidate()
    {
        _cameraController = FindObjectOfType<CameraController>();
        _inventoryManager = FindObjectOfType<InventoryManager>();
    }

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _lastCameraOffset = _cameraController.offset;
        _lastCameraDist = _cameraController.dist;
        _inventoryManager.gameObject.SetActive(false);
        ToggleCursor(false);
        _playerInput.UI.ToggleInventory.canceled += ToggleInventory;
    }

    private void ToggleInventory(InputAction.CallbackContext context) 
    {

        if (_inventoryManager.gameObject.activeSelf)
        {
            _inventoryManager.UnselectSlot();
            _inventoryManager.gameObject.SetActive(false);
            _cameraController.offset = _lastCameraOffset;
            _cameraController.dist = _lastCameraDist;
            ToggleCursor(false);
        }
        else
        {
            _inventoryManager.gameObject.SetActive(true);
            _lastCameraOffset = _cameraController.offset;
            _lastCameraDist = _cameraController.dist;
            _cameraController.offset = _cameraOffset;
            _cameraController.dist = -_cameraDist;
            ToggleCursor(true);
        }

    }

    private void ToggleCursor(bool enable)
    {
        if (enable)
        {
            //lock cursor to the middle of the screen and make it invisible
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            _cameraController.getInput = false;
        }
        else
        {
            //lock cursor to the middle of the screen and make it invisible
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            _cameraController.getInput = true;
        }
    }

    private void OnEnable(){
        _playerInput.Enable();
    }

    private void OnDisable(){
        _playerInput.Disable();
    }
}
