using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableSlot : EquipmentSlot
{
    public ConsumableType consumableType;
    public override bool AddItem(Item item, ref int quantity, int durability = 1)
    {
        if (!(item is Consumable))
        {
            Debug.Log("Item is not a consumable");
            Warning.Display("Item is not a consumable");
            return false;
        }
        else if (((Consumable)item).consumableType != consumableType)
        {
            Debug.Log("Item is not a " + consumableType);
            Warning.Display("Item is not a " + consumableType);
            return false;
        }
        else
        {
            base.AddItem(item, ref quantity, durability);
            Equip((Equipment)item);
            return true;
        }
    }
}
