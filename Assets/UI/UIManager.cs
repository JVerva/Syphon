using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    PlayerInputManager playerInputManager;
    CameraController cameraController;
    [SerializeField]GameObject inventoryTab;
    [Space]
    [SerializeField]private Vector2 cameraOffset;
    [SerializeField]private float cameraDist;
    private Vector2 lastCameraOffset;
    private float lastCameraDist;

    private void OnValidate()
    {
        playerInputManager = FindObjectOfType<PlayerInputManager>();
        cameraController = FindObjectOfType<CameraController>();
    }

    private void Awake()
    {
        lastCameraOffset = cameraController.offset;
        lastCameraDist = cameraController.dist;
        inventoryTab.SetActive(false);
        ToggleCursor(false);
        playerInputManager.inventoryToggle += ToggleInventory;
    }

    private void ToggleInventory()
    {
        if (inventoryTab.activeSelf)
        {
            inventoryTab.SetActive(false);
            cameraController.offset = lastCameraOffset;
            cameraController.dist = lastCameraDist;
            ToggleCursor(false);
        }
        else
        {
            inventoryTab.SetActive(true);
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
