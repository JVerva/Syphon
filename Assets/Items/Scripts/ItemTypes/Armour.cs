using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Armour", menuName = "Item/Equipment/Armour")]
public class Armour : Equipment
{
    public ArmourType armourType;
    public float equipedWeight;
}

public enum ArmourType {Head, Chest, Hands, Legs, Feet};
