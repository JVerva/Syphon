using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "newItem", menuName="Item/Item")]
public class Item : ScriptableObject
{
    public new string name;
    public Sprite icon;
    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;
    public string itemInfo;
    public int stackSize;
    public float weight;
}
