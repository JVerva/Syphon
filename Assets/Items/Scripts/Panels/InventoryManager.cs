using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour, IPointerClickHandler
{
    private Inventory inventory;
    private EquipmentPanel equipmentPanel;
    private InventorySlot selectedSlot;

    private void Awake()
    {
        inventory = GetComponentInChildren<Inventory>();
        equipmentPanel = GetComponentInChildren<EquipmentPanel>();
        selectedSlot = null;
        inventory.OnLeftClickEvent += SelectItem;
        equipmentPanel.OnLeftClickEvent += SelectItem;
        inventory.OnLeftClickEvent += SelectItem;
        equipmentPanel.OnLeftClickEvent += SelectItem;
    }

    //select item if none is, else swap items
    private void SelectItem(InventorySlot slot)
    {
        if (selectedSlot == null)
        {
            if (!slot.isEmpty)
            {
                selectedSlot = slot;
                ToggleVisualizeSelection(true);
            }
        }
        else
        {
            SwapItems(slot);
        }
    }

    //Gray out the slot and make the slots item image follow the mouse
    private void ToggleVisualizeSelection(bool visualize)
    {
        if (visualize)
        {
            selectedSlot.image.color = new Color(selectedSlot.image.color.r, selectedSlot.image.color.g, selectedSlot.image.color.b, 0.5f);
        }
        else
        {
            selectedSlot.image.color = new Color(selectedSlot.image.color.r, selectedSlot.image.color.g, selectedSlot.image.color.b, 1);
        }
    }

    //if items are the same add the selected quantity to the new slot and keep the first item selected; if they are different swap them
    private void SwapItems(InventorySlot slot)
    {
        int quantity = selectedSlot.Quantity;
        //if the second slot is empty
        if (slot.isEmpty)
        {
            if (slot.AddItem(selectedSlot.Item, ref quantity, selectedSlot.Durability))
            {
                selectedSlot.RemoveItem();
            }
            UnselectSlot();
            return;
        }
        //if the second slot has an item in it
        else
        {
            //if the items are the same
            if (slot.Item == selectedSlot.Item)
            {
                slot.AddItem(selectedSlot.Item, ref quantity, selectedSlot.Durability);
                selectedSlot.Quantity = quantity;
                UnselectSlot();
                return;
            }
            else
            {
                Item tempItem = selectedSlot.Item;
                int tempQuantity = quantity;
                int tempDurability = selectedSlot.Durability;
                quantity = slot.Quantity;
                if(selectedSlot.AddItem(slot.Item, ref quantity, slot.Durability))
                    slot.AddItem(tempItem, ref tempQuantity, tempDurability);
                UnselectSlot();
                return;
            }
        }
    }

    //if there is a selected slot and right click is pressed unselect it
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData!=null && eventData.button == PointerEventData.InputButton.Right)
        {
            UnselectSlot();
        }
    }

    public void UnselectSlot()
    {
        if (selectedSlot != null)
        {
            ToggleVisualizeSelection(false);
            selectedSlot = null;
        }
    }
}
