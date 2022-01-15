using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventorySlot[] Slots;
    public bool isFull;

    private void OnValidate()
    {
        Slots = GetComponentsInChildren<InventorySlot>();
    }

    public void AddItem(ItemInstance itemInstance)
    {
        List<int> foundIndex = new List<int>();
        //get the isert slot order
        foundIndex =FindSlotInsertOrder(itemInstance.item);
        for(int i = 0; i < foundIndex.Count && itemInstance.quantity>0; i++)
        {
            if (foundIndex[i] >= 0)
            {
                if (Slots[foundIndex[i]].itemInstance != null)
                {
                    int sumQuantity = itemInstance.quantity + Slots[foundIndex[i]].itemInstance.quantity;
                    if (sumQuantity >= itemInstance.item.stackSize)
                    {
                        itemInstance.quantity = itemInstance.item.stackSize;
                        Slots[foundIndex[i]].AddItem(itemInstance);
                        Debug.Log(Slots[foundIndex[i]].itemInstance.quantity);
                        itemInstance.quantity = sumQuantity - itemInstance.item.stackSize;
                        Debug.Log(Slots[foundIndex[i]].itemInstance.quantity);
                    }
                    else
                    {
                        itemInstance.quantity = sumQuantity;
                        Slots[foundIndex[i]].AddItem(itemInstance);
                        itemInstance.quantity = 0;
                    }
                }
                else if(Slots[foundIndex[i]].itemInstance == null)
                {
                    Slots[foundIndex[i]].AddItem(itemInstance);
                }
            }
            else
            {
                Debug.Log("Inventory full");
            }
        }
    }

    //look through inventory to find the same item, store all of those indexes and the last index saved shall be an empty one
    private List<int> FindSlotInsertOrder(Item item)
    {
        List<int> found = new List<int>();
        int cnt=0;
        for(int i = 0; i < Slots.Length; i++)
        {
            if (Slots[i].itemInstance != null)
            {
                if (Slots[i].itemInstance.item == item)
                {
                    found.Add(i);
                    cnt++;
                }
            }
        }
        found.Add(FindEmptySlotIndex());
        return found;
    }

    //find the first empty slot, if the inventory is full return -1
    private int FindEmptySlotIndex()
    {
        int i;
        for ( i = 0; Slots[i].itemInstance != null && i < Slots.Length; i++);
        if (i == (Slots.Length - 1) && Slots[i] != null)
        {
            isFull = true;
            i = -1;
        }
        else
        {
            isFull = false;
        }
        return i;
    }
}
