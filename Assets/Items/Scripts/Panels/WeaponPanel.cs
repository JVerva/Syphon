using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponPanel : MonoBehaviour
{
    public WeaponSlot[] slots;
    public WeaponSlot[] offHandSlots;
    public WeaponSlot[] mainHandSlots;
    private Inventory playerInventory;

    public event Action<InventorySlot> OnLeftClickEvent;
    public event Action<InventorySlot> DropItemEvent;

    private void OnValidate()
    {
        //find slots and set both weapon combos
        slots = GetComponentsInChildren<WeaponSlot>();
        int j = 0;
        int k = 0;
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].hand == Hand.OffHand)
            {
                offHandSlots[j++] = slots[i];
            }
            else if (slots[i].hand == Hand.MainHand)
            {
                mainHandSlots[k++] = slots[i];
            }
        }
    }

    private void Awake()
    {
        foreach (InventorySlot slot in slots)
        {
            slot.OnLeftClickEvent += OnLeftClick;
            ((WeaponSlot)slot).OnTwoHandedWeaponAdded += EquipTwoHanded;
            ((WeaponSlot)slot).OnOffHandedWeaponAdded += EquipOffHanded;
        }
        playerInventory = FindObjectOfType<Inventory>();
    }

    //check if there is an off hand weapon equipped, if so try to add it to the inventory, if that fails call the drop event and drop the item
    private void EquipTwoHanded(WeaponSlot slot)
    {
        int i;
        for (i = 0;i<mainHandSlots.Length && mainHandSlots[i] != slot; i++);
        int quantity = offHandSlots[i].Quantity;
        if (!playerInventory.AddItem(offHandSlots[i].Item,ref quantity, offHandSlots[i].Durability)){
            DropItemEvent?.Invoke(slot);
        }
        offHandSlots[i].Quantity = quantity;
    }
    
    private void EquipOffHanded(WeaponSlot slot)
    {
        int i;
        for (i = 0;i<offHandSlots.Length && offHandSlots[i] != slot; i++);
        int quantity = mainHandSlots[i].Quantity;
        if (!playerInventory.AddItem(mainHandSlots[i].Item,ref quantity, mainHandSlots[i].Durability)){
            DropItemEvent?.Invoke(slot);
        }
        mainHandSlots[i].Quantity = quantity;
    }


    //invoke a left click event if any inventory solt has invoked it
    private void OnLeftClick(InventorySlot obj)
    {
        OnLeftClickEvent?.Invoke(obj);
    }
}
