using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private PlayerInputManager playerInputManager;
    private CameraController cameraController;
    private InventoryManager inventoryManager;
    [Space]
    [SerializeField] private Vector2 cameraOffset;
    [SerializeField] private float cameraDist;
    private Vector2 lastCameraOffset;
    private float lastCameraDist;

    private void OnValidate()
    {
        playerInputManager = FindObjectOfType<PlayerInputManager>();
        cameraController = FindObjectOfType<CameraController>();
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    private void Awake()
    {
        lastCameraOffset = cameraController.offset;
        lastCameraDist = cameraController.dist;
        inventoryManager.gameObject.SetActive(false);
        ToggleCursor(false);
        playerInputManager.inventoryToggle += ToggleInventory;
    }

    private void ToggleInventory()
    {
        if (inventoryManager.gameObject.activeSelf)
        {
            inventoryManager.UnselectSlot();
            inventoryManager.gameObject.SetActive(false);
            cameraController.offset = lastCameraOffset;
            cameraController.dist = lastCameraDist;
            ToggleCursor(false);
        }
        else
        {
            inventoryManager.gameObject.SetActive(true);
            lastCameraOffset = cameraController.offset;
            lastCameraDist = cameraController.dist;
            cameraController.offset = cameraOffset;
            cameraController.dist = -cameraDist;
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
            cameraController.getInput = false;
        }
        else
        {
            //lock cursor to the middle of the screen and make it invisible
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            cameraController.getInput = true;
        }
    }
}
