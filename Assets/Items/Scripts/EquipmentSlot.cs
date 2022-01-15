using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : InventorySlot
{
    private EquipmentInstance _equipmentInstance; 
    public new EquipmentInstance itemInstance
    {
        get { return _equipmentInstance; }
        set
        {
            _equipmentInstance = value;
            if (_equipmentInstance == null)
            {
                image.enabled = false;
                quantity.enabled = false;
                durability.enabled = false;
            }
            else
            {
                image.enabled = true;
                image.sprite = _equipmentInstance.item.icon;
                if (_equipmentInstance.quantity > 1)
                {
                    quantity.enabled = true;
                    quantity.text = _equipmentInstance.quantity.ToString();
                }
                else
                {
                    quantity.enabled = false;
                }
                if (itemInstance.durability < itemInstance.item.maxDurability)
                {
                    durability.enabled = true;
                    durability.text = itemInstance.durability.ToString() + "/" + itemInstance.item.maxDurability.ToString();
                }
                else
                {
                    durability.enabled = false;
                }
            }
        }
    } 

    public new bool AddItem(ItemInstance newItemInstance)
    {
        if (newItemInstance is EquipmentInstance)
        {
            itemInstance = (EquipmentInstance)newItemInstance;
            return true;
        }
        else
            return false;

    }
}
