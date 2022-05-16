using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armour : Equipment
{
    public ArmourType armourType;
    public float equipedWeight;
    public SkinnedMeshRenderer skinnedMeshRenderer;
}

public enum ArmourType {Head, Chest, Hands, Legs, Feet, BackPack};
