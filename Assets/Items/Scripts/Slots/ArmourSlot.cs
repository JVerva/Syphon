using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourSlot : EquipmentSlot
{
    public ArmourType armourType;
    public Transform body;

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
            Debug.Log("Item is not " + armourType + " armour");
            Warning.Display("Item is not " + armourType + " armour");
            return false;
        }
        else
        {
            base.AddItem(item, ref quantity, durability = 1);
            Unequip();
            Equip((Equipment)item);
            return true;
        }
    }
}
