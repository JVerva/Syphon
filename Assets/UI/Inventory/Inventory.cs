using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
   [SerializeField] private int inventorySize;
    private InventorySlot[] inventorySlots;
    private InventorySlot selectedSlot;
    public bool isFull;
    public int emptySlotIndex;

    public event Action OnChange;
    private void OnValidate()
    {
        //set default values
        inventorySlots = GetComponentsInChildren<InventorySlot>();
        inventorySize = inventorySlots.Length;
    }

    void Awake()
    {
        //check for clicks on slots
        foreach (InventorySlot inventorySlot in inventorySlots)
        {
            inventorySlot.OnLeftClick += SelectSlot;
            inventorySlot.OnRightClick += ShowItemMenu;
            this.OnChange += Update;
        }
    }

    private void Update()
    {
        emptySlotIndex = GetClearSlot();
        if (emptySlotIndex > inventorySize)
        {
            isFull = true;
        }
        else
        {
            isFull = false;
        }
    }

    private void ShowItemMenu(InventorySlot arg2)
    {

    }

    //Select slot
    private void SelectSlot(InventorySlot inventorySlot)
    {
        //check if there was a slot selected before
        if(selectedSlot == null)
        {
            //if the slot is empty, dont select it
            if (inventorySlot.itemInstance == null)
                return;
            // select the slot
            else
                selectedSlot = inventorySlot;
                return;
        }
        //if not take care of the interaction
        else
        {
            SwapItems(selectedSlot, inventorySlot);
        }
    }

    //interaction between two selected slots
    private void SwapItems(InventorySlot selectedSlot, InventorySlot inventorySlot)
    {
        //if the second slot doesnt have item just move the selected one
        if (inventorySlot.itemInstance == null)
        {
            inventorySlot.AddItem(selectedSlot.itemInstance);
            selectedSlot.RemoveItem();
            selectedSlot = null;
        }
        //if the items are the same
        else if (inventorySlot.itemInstance.item == selectedSlot.itemInstance.item)
        {
            //check if their summed quantities are over the items limit
            int sumQuantity = inventorySlot.itemInstance.quantity + selectedSlot.itemInstance.quantity;
            //if not add everything to the second slot
            if(sumQuantity <= inventorySlot.itemInstance.item.stackSize)
            {
                inventorySlot.itemInstance.quantity = sumQuantity;
                selectedSlot.RemoveItem();
                selectedSlot = null;
            }
            //if so fill the second slot to the brim
            else
            {
                inventorySlot.itemInstance.quantity = inventorySlot.itemInstance.item.stackSize;
                selectedSlot.itemInstance.quantity = sumQuantity - inventorySlot.itemInstance.item.stackSize;
            }
        }
        //if the items are different swap them
        else
        {
            ItemInstance temp = inventorySlot.itemInstance;
            inventorySlot.AddItem(selectedSlot.itemInstance);
            selectedSlot.AddItem(temp);
            selectedSlot = null;
        }
        OnChange?.Invoke();
    }

    //return the slot indexes wich have  the desired item
    public int[] SearchForItem(Item item) 
    {
        int count = 0;
        int[] indexes = null;
        for(int i = 0; i < inventorySize; i++)
        {
            if (inventorySlots[i].itemInstance != null)
            {
                if (inventorySlots[i].itemInstance.item == item)
                {
                    indexes[count] = i;
                    count++;
                }
            }
        }
        return indexes;
    }

    //get the first clear slot index, if inventory is full return size+1
    public int GetClearSlot()
    {
        for(int i = 0; i < inventorySize; i++)
        {
            if (inventorySlots[i].itemInstance == null)
                return i;
        }
        return inventorySize + 1;
    }


    //Adds an item to the inventory
    public void AddItem(ItemInstance itemInstance)
    {
        int[] indexesFound = SearchForItem(itemInstance.item);
        //if no same item was found check if its full, if not add it to the first empty slot
        if (indexesFound == null)
        {
            if (isFull)
            {
                Debug.Log("Inventory is Full");
            }
            else
            {
                inventorySlots[emptySlotIndex].AddItem(itemInstance);
            }
        }
        //if the same item was found
        else
        {
            InventorySlot inventorySlot;
            //go through the same item indexes 
            for (int i = 0; i < indexesFound.Length; i++)
            {
                inventorySlot = inventorySlots[indexesFound[i]];
                //check if their summed quantities are over the items limit
                int sumQuantity = inventorySlot.itemInstance.quantity + itemInstance.quantity;
                //if not add everything to the second slot
                if (sumQuantity <= itemInstance.item.stackSize)
                {
                    inventorySlot.itemInstance.quantity = sumQuantity;
                    break;
                }
                //if so fill the second slot to the brim
                else
                {
                    inventorySlot.itemInstance.quantity = itemInstance.item.stackSize;
                    itemInstance.quantity -= sumQuantity - itemInstance.item.stackSize;
                }
            }
            //if after adding the item there is still some left try to put it in an empty slot
            if (itemInstance.quantity > 0)
            {
                if (isFull)
                {
                    Debug.Log("inventory is full");
                }
                else
                {
                    inventorySlots[emptySlotIndex].AddItem(itemInstance);
                }
            }
        }
        OnChange?.Invoke();
        return;
    }
}

