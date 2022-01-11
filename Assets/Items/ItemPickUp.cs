using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : Interactable
{
    public ItemInstance itemInstance;
    PlayerInventory inventory;
    public override void Interact()
    {
        inventory.AddItem(itemInstance);
        if (itemInstance.quantity <= 0)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
