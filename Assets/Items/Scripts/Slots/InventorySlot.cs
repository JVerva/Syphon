using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image image;
    [SerializeField] protected Text quantityText;
    [SerializeField] protected Text durabilityText;

    private Item _item;
    private int _quantity;
    private int _durability;
    public bool isEmpty;

    public event Action<InventorySlot> OnLeftClickEvent;

    public InventorySlot()
    {
    }

    public InventorySlot(Item item, int quantity, int durability = 1)
    {
        Item = item;
        Quantity = quantity;
        Durability = durability;
    }

    private void Awake()
    {
        isEmpty = true;
    }

    //display item icon on slot
    public Item Item {
        get { return _item; }
        set { _item = value;
            if (_item == null)
            {
                RemoveItem();
            }
            else {
                image.enabled = true;
                image.sprite = _item.icon;
            } 
        }
    }

    //if reaches 0 destroy item, when it is greater than 1 display its amount on slot
    public int Quantity
    {
        get { return _quantity; }
        set { _quantity = value;
            if (_quantity == 0)
            {
                RemoveItem();
            }
            else
            {
                if (_quantity > _item.stackSize)
                {
                    _quantity = _item.stackSize;
                }
                if (_quantity > 1)
                {
                    quantityText.enabled = true;
                    quantityText.text = _quantity.ToString();
                }
                else
                {
                    quantityText.enabled = false;
                }
            }
        }
    }

    //when durability = 0 destroy item, if it is different than items max durability disply durability bar
    public int Durability
    {
        get { return _durability; }
        set
        {
            if (!(_item is Equipment))
            {
                durabilityText.enabled = false;
                return;
            }
            _durability = value;
            if (_durability <= 0)
            {
                RemoveItem();
            }else if(_durability == ((Equipment)_item).maxDurability)
            {
                durabilityText.enabled = false;
            }
            else
            {
                durabilityText.enabled = true;
                durabilityText.text = _durability.ToString() + "/" + ((Equipment)_item).maxDurability;
            }
        }
    }

    //adds item to the slot
    public virtual bool AddItem(Item item, ref int quantity, int durability = 1)
    {
        if (Item == item)
        {
            if (Quantity + quantity > _item.stackSize)
            {
                quantity -= _item.stackSize - Quantity;
                Quantity = _item.stackSize;
            }
            else {
                Quantity += quantity;
                quantity = 0;
            }
        }
        else
        {
            Item = item;
            Quantity = quantity;
            quantity -= Quantity;
        }
        Durability = durability;
        isEmpty = false;
        return true;
    }

    //delete item in the slot
    public virtual void RemoveItem()
    {
        _item = null;
        _quantity = 0;
        _durability = 0;
        image.enabled = false;
        quantityText.enabled = false;
        durabilityText.enabled = false;
        isEmpty = true;
    }

    //called when there is a mouse click over the slot
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData != null && eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClickEvent?.Invoke(this);
        }
    }

    //called when the cursor enters the slots transform
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {

    }

    //called when the cursor exits the slots transform
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        
    }
}
