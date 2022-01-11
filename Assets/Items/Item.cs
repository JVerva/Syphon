using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "newItem", menuName="Item")]
public class Item : ScriptableObject
{
    public new string name;
    public Sprite icon;
    public MeshRenderer mesh;
    public string itemInfo;
    public int stackSize;
}
