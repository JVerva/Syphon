using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPanel : MonoBehaviour
{
    WeaponPanel weaponPanel;
    ArmourPanel armourPanel;
    ToolPanel toolPanel;
    ConsumablePanel consumablePanel;

    public event Action<InventorySlot> OnLeftClickEvent;

    private void Awake()
    {
        weaponPanel = GetComponentInChildren<WeaponPanel>();
        armourPanel = GetComponentInChildren<ArmourPanel>();
        toolPanel = GetComponentInChildren<ToolPanel>();
        consumablePanel = GetComponentInChildren<ConsumablePanel>();

        foreach(WeaponSlot slot in weaponPanel.slots)
        {
            slot.OnLeftClickEvent += OnLeftClick;
        }
        foreach (ArmourSlot slot in armourPanel.Slots)
        {
            slot.OnLeftClickEvent += OnLeftClick;
        }
        foreach (ToolSlot slot in toolPanel.Slots)
        {
            slot.OnLeftClickEvent += OnLeftClick;
        }
        foreach (ConsumableSlot slot in consumablePanel.Slots)
        {
            slot.OnLeftClickEvent += OnLeftClick;
        }

    }

    private void OnLeftClick(InventorySlot obj)
    {
        OnLeftClickEvent?.Invoke(obj);
    }
}
