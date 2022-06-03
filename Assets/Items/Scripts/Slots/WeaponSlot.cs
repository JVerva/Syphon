using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponSlot : EquipmentSlot
{
    public Hand hand;
    public event Action<WeaponSlot> OnTwoHandedWeaponAdded;
    public event Action<WeaponSlot> OnOffHandedWeaponAdded;
    public override bool AddItem(Item item, ref int quantity, int durability = 1)
    {
        if (!(item is Weapon))
        {
            Debug.Log("Item is not a weapon");
            Warning.Display("Item is not a weapon");
            return false;
        }else if(((Weapon)item).weaponType == WeaponType.OffHand && hand == Hand.MainHand)
        {
            Debug.Log("Item is not a main hand weapon");
            Warning.Display("Item is not a main hand weapon");
            return false;
        }else if((((Weapon)item).weaponType == WeaponType.TwoHanded || ((Weapon)item).weaponType == WeaponType.OneHanded )&& hand == Hand.OffHand )
        {
            Debug.Log("Item is not an off hand weapon");
            Warning.Display("Item is not an off hand weapon");
            return false;
        }
        else
        {
            if (((Weapon)item).weaponType == WeaponType.TwoHanded)
                OnTwoHandedWeaponAdded?.Invoke(this);
            else if (((Weapon)item).weaponType == WeaponType.OffHand)
                OnOffHandedWeaponAdded?.Invoke(this);
            base.AddItem(item, ref quantity, durability = 1);
            Equip((Equipment)item);
            return true;
        }
    }
}

public enum Hand {MainHand, OffHand}
