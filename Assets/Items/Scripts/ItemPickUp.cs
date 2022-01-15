using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : Interactable
{
    public ItemInstance itemInstance;
    private Inventory playerInventory;

    private void OnValidate()
    {
        playerInventory = FindObjectOfType<Inventory>();
    }


    public override void Interact()
    {
        Debug.Log("Try to pick up Item");
        playerInventory.AddItem(itemInstance);
    }
}
