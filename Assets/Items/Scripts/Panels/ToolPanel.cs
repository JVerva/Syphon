using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ToolPanel : MonoBehaviour
{

    public ToolSlot[] Slots;

    public event Action<InventorySlot> OnLeftClickEvent;

    private void OnValidate()
    {
        Slots = GetComponentsInChildren<ToolSlot>();
        foreach (InventorySlot slot in Slots)
        {
            slot.OnLeftClickEvent += OnLeftClick;
        }
    }

    //invoke a left click event if any inventory solt has invoked it
    private void OnLeftClick(InventorySlot obj)
    {
        OnLeftClickEvent?.Invoke(obj);
    }
}
