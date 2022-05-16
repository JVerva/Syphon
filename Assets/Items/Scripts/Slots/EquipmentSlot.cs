using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipmentSlot : InventorySlot
{
    public override bool AddItem(Item item, ref int quantity, int durability = 1)
    {
        if (!(item is Equipment))
        {
            Debug.Log("Item is not Equipment");
            return false;
        }
        else
        {
            base.AddItem(item, ref quantity, durability = 1);
            Equip(item);
            return true;
        }
    }

    public override void RemoveItem()
    {
        Unequip();
        base.RemoveItem();
    }

    public virtual void Equip(Item item)
    {
    }

    public virtual void Unequip()
    {
    }
}
