using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    public InventorySlot[] Slots;
    public bool isFull;
    public event Action<InventorySlot> OnLeftClickEvent;
    private void OnValidate()
    {
        Slots = GetComponentsInChildren<InventorySlot>();
        foreach(InventorySlot slot in Slots)
        {
            slot.OnLeftClickEvent += OnLeftClick;
        }
    }

    //invoke a left click event if any inventory solt has invoked it
    private void OnLeftClick(InventorySlot slot)
    {
        OnLeftClickEvent?.Invoke(slot);
    }

    //Add item to the inventory, finds the slots wich have the same item, filling them up before occupying a new one
    public bool AddItem(Item item,ref int quantity,int durability = 1)
    {
        List<int> indexOrder = GetSlotInsertOrder(item);
        for(int i =0; i< indexOrder.Count && quantity>0; i++)
        {
            if (indexOrder[i] == -1)
            {
                Warning.Display("Inventory is Full");
                Debug.Log("Inventory is Full");
                return false;
            }
            Slots[indexOrder[i]].AddItem(item,ref quantity, durability);
        }
        Debug.Log("Item Added");
        return true;
    }

    //look through inventory to find the same item, store all of those indexes and the last index saved shall be an empty one
    private List<int> GetSlotInsertOrder(Item item)
    {
        List<int>indexOrder = new List<int>();
        for (int i = 0; i < Slots.Length; i++)
        {
            if(Slots[i].Item == item)
            {
                indexOrder.Add(i);
            }
        }
        indexOrder.Add(GetEmptySlotIndex());
        return indexOrder;
    }

    //find the first empty slot, if the inventory is full return -1
    private int GetEmptySlotIndex()
    {
        int i = 0;
        for (i = 0; i < Slots.Length; i++)
        {
            if (Slots[i].Item == null)
            {
                isFull = false;
                break;
            }
        }
        if (i == Slots.Length && Slots[i-1].Item != null)
        {
            isFull = true;
            i = -1;
        }
        return i;
    }
}
