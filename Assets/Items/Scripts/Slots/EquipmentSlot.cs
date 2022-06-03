using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class EquipmentSlot : InventorySlot
{

    public SkinnedMeshRenderer targetMesh;
    public SkinnedMeshRenderer mesh = null;

    private void Awake()
    {
        targetMesh = GameObject.Find("Body").GetComponent<SkinnedMeshRenderer>();
        isEmpty = true;
    }

    public override bool AddItem(Item item, ref int quantity, int durability = 1)
    {
        if (!(item is Equipment))
        {
            Debug.Log("Item is not Equipment");
            Warning.Display("Item is not Equuipment");
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

    public override void RemoveItem()
    {
        Unequip();
        base.RemoveItem();
    }

    public void Equip(Equipment item)
    {
        mesh = Instantiate(item.skinnedMeshRenderer, targetMesh.transform);
        mesh.bones = targetMesh.bones;
        mesh.rootBone = targetMesh.rootBone;
    }

    public void Unequip()
    {
        if (mesh != null)
        {
            GameObject.Destroy(mesh.gameObject);
            mesh = null;
        }
    }
}
