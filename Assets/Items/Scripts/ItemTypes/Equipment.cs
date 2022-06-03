using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Equipment", menuName = "Item/Equipment")]
public class Equipment : Item
{
    public float maxDurability;
    public SkinnedMeshRenderer skinnedMeshRenderer;
}
