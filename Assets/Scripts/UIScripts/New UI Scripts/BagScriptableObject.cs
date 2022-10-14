using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class BagScriptableObject : ScriptableObject
{
    [field: SerializeField]
    public List<Item> itemList { get; private set; }

    [NonSerialized]
    public UnityEvent bagItemListChangedEvent;
    [NonSerialized]
    public UnityEvent bagItemUsedEvent;
    [NonSerialized]
    public UnityEvent<Item, int> bagItemEquippedEvent;

    private void OnEnable()
    {
        itemList = new List<Item>();
        if (bagItemListChangedEvent == null)
        {
            bagItemListChangedEvent = new UnityEvent();
        }
        if (bagItemUsedEvent == null)
        {
            bagItemUsedEvent = new UnityEvent();
        }
        if (bagItemEquippedEvent == null)
        {
            bagItemEquippedEvent = new UnityEvent<Item, int>();
        }
    }

    public void AddItem(Item item)
    {
        Item itemToAdd = new Item { itemID = item.itemID, amount = item.amount };
        if (itemToAdd.itemID == ItemID.manaport_nothing)
        {
            return;
        }

        if (itemToAdd.GetMetadata().stackable)
        {
            bool itemAlreadyInBag = false;
            foreach (Item it in itemList)
            {
                if (it.itemID == itemToAdd.itemID)
                {
                    it.amount += itemToAdd.amount;
                    itemAlreadyInBag = true;
                }
            }
            if (!itemAlreadyInBag)
            {
                itemList.Add(itemToAdd);
            }
        }
        else
        {
            itemList.Add(itemToAdd);
        }
        bagItemListChangedEvent.Invoke();
    }

    public void RemoveItem(Item item)
    {
        if (item.itemID == ItemID.manaport_nothing)
        {
            return;
        }

        foreach (Item it in itemList)
        {
            if (it.itemID == item.itemID)
            {
                it.amount -= 1;
                if (it.amount == 0)
                {
                    itemList.Remove(it);
                }
                
                bagItemListChangedEvent.Invoke();
                return;
            }
        }
    }

    public void UseItem(Item item, int i)
    {
        if (item.itemID == ItemID.manaport_nothing)
        {
            return;
        }

        item.GetMetadata().UseEvent(i);
        RemoveItem(item);
        bagItemUsedEvent.Invoke();
    }

    public void EquipItem(Item item, int i)
    {
        if (item.itemID == ItemID.manaport_nothing)
        {
            return;
        }

        item.GetMetadata().EquipEvent(i);
        bagItemEquippedEvent.Invoke(item, i);
        RemoveItem(item);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }

    public void AddAllItems()
    {
        foreach (ItemID id in Enum.GetValues(typeof(ItemID)))
        {
            if (id == ItemID.manaport_nothing)
            {
                continue;
            }

            AddItem( new Item { itemID = id, amount = 1 });
        }
    }
}