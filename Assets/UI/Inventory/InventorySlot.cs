using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using UnityEngine;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image icon;
    [SerializeField] private Text count;
    [SerializeField] private Text durability;
    public ItemInstance itemInstance;
    public event Action<InventorySlot> OnLeftClick;
    public event Action<InventorySlot> OnRightClick;


    private void OnValidate()
    {
        //clear slot in the editor
        RemoveItem();
    }

    //Add item to this slot, setting its sprite and quantity 
    public void AddItem(ItemInstance newitemInstance)
    {
        itemInstance = newitemInstance;
        icon.sprite = itemInstance.item.icon;
        icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, 255);
        count.text = itemInstance.quantity.ToString();
    }

    //Remove item from slot clear its sprite and other text formats
    public void RemoveItem()
    {
        itemInstance = null;
        icon.sprite = null;
        icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, 0);
        count.text = null;
        durability.text = null;
    }

    //Detect if there was a click on the slot
    public void OnPointerClick(PointerEventData eventData)
    {
        //call left click event
        if (eventData != null && eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick?.Invoke(this);
        }
        //call right click event
        if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if(itemInstance!=null)
                OnRightClick?.Invoke(this);
        }
    }
}
