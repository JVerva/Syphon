using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tool", menuName = "Item/Equipment/Tool")]
public class Tool : Equipment
{
    public ToolType toolType;
}

public enum ToolType {BuildingHammer, Axe, StoneHammer, PickAxe, Scythe, SkinningKnife };