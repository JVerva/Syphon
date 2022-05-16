using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ArmourPanel : MonoBehaviour
{
    public ArmourSlot[] Slots;

    public event Action<InventorySlot> OnLeftClickEvent;

    private void OnValidate()
    {
        Slots = GetComponentsInChildren<ArmourSlot>();
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
