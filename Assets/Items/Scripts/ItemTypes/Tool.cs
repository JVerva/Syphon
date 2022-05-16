using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : Equipment
{
    public ToolType toolType;
}

public enum ToolType {BuildingHammer, Axe, StoneHammer, PickAxe, Scythe, SkinningKnife };