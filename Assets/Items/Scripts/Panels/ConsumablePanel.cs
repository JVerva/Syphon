using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ConsumablePanel : MonoBehaviour
{
    public ConsumableSlot[] Slots;

    public event Action<InventorySlot> OnLeftClickEvent;

    private void OnValidate()
    {
        Slots = GetComponentsInChildren<ConsumableSlot>();
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
