using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourSlot : EquipmentSlot
{
    public ArmourType armourType;

    public override bool AddItem(Item item, ref int quantity, int durability = 1)
    {
        if (!(item is Armour))
        {
            Debug.Log("Item is not Armour");
            Warning.Display("Item is not Armour");
            return false;
        }
        else if (((Armour)item).armourType != armourType)
        {
            Debug.Log("Item is not " + armourType + "armour");
            return false;
        }
        else
        {
            base.AddItem(item, ref quantity, durability);
            Equip(item);
            return true;
        }
    }
}
