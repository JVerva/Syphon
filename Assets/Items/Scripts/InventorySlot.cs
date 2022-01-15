using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    private ItemInstance _itemInstance;
    public ItemInstance itemInstance
    {
        get { return _itemInstance; }
        set
        {
            _itemInstance = value;
            if (_itemInstance == null)
            {
                image.enabled = false;
                quantity.enabled = false;
                durability.enabled = false;
            }
            else
            {
                image.enabled = true;
                image.sprite = _itemInstance.item.icon;
                if (_itemInstance.quantity > 1)
                {
                    quantity.enabled = true;
                    quantity.text = _itemInstance.quantity.ToString();
                }
                else
                {
                    quantity.enabled = false;
                }
                durability.enabled = false;
            }
        }
    }
    [SerializeField] protected Image image;
    [SerializeField] protected Text quantity;
    [SerializeField] protected Text durability;

    private void OnValidate()
    {
        itemInstance = null;
    }

    public bool AddItem(ItemInstance newItemInstance)
    {
        itemInstance = newItemInstance;
        return true;
    }

    public void RemoveItem(ItemInstance newItemInstance)
    {
        itemInstance = null;
        return;
    }
}
