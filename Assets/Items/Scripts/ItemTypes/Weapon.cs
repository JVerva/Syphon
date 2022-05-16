using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newWeapon", menuName = "Item/Equipment/Weapon")]
public class Weapon : Equipment
{
    public WeaponType weaponType;
    public float equipedWeight;
}

public enum WeaponType {OneHanded, TwoHanded, OffHand };
