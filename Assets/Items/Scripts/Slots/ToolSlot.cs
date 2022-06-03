using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolSlot : EquipmentSlot
{
    public ToolType toolType;

    public override bool AddItem(Item item, ref int quantity, int durability = 1)
    {
        if (!(item is Tool))
        {
            Debug.Log("Item is not a tool");
            Warning.Display("Item is not a tool");
            return false;
        }
        else if (((Tool)item).toolType != toolType)
        {
            Debug.Log("Item is not a " + toolType);
            Warning.Display("Item is not a " + toolType);
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
