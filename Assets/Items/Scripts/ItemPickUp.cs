using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : Interactable
{
    [SerializeField] private Item item;
    [SerializeField] private int quantity;
    [SerializeField] private int durability;
    private Inventory inventory;

    private void OnValidate()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    private void Start()
    {
        DuplicateCollider();
        interactionText = "Pick Up " + item.name;
        if (quantity > 1)
        {
           interactionText += " x" + quantity.ToString();
        }
    }

    //Adds item to the inventory
    public override void Interact()
    {
        inventory.AddItem(item, ref quantity, durability);
        if (quantity == 0)
            Destroy();
        Debug.Log("Pick up Item");
    }

    //destroy the game object
    private void Destroy()
    {
        GameObject.Destroy(gameObject);
    }
}
