using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    PlayerInputManager playerInputManager;
    CameraController cameraController;
    GameObject inventoryTab;

    private void OnValidate()
    {
        playerInputManager = FindObjectOfType<PlayerInputManager>();
        cameraController = FindObjectOfType<CameraController>();
        inventoryTab = GetComponentInChildren<PlayerInventory>().gameObject;
    }

    private void Awake()
    {
        inventoryTab.SetActive(false);
        ToggleCursor(false);
        playerInputManager.inventoryToggle += ToggleInventory;
    }

    private void ToggleInventory()
    {
        if (inventoryTab.activeSelf)
        {
            inventoryTab.SetActive(false);
            ToggleCursor(false);
        }
        else
        {
            inventoryTab.SetActive(true);
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
