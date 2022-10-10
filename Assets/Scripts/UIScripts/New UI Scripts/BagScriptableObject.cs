using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class BagScriptableObject : ScriptableObject
{
    [field: SerializeField]
    public List<Item> itemList { get; private set; }

    [System.NonSerialized]
    public UnityEvent bagItemListChangedEvent;

    private void OnEnable()
    {
        Debug.Log("On Enable");
        itemList = new List<Item>();
        if (bagItemListChangedEvent == null)
        {
            bagItemListChangedEvent = new UnityEvent();
        }
    }

    public void AddItem(Item item)
    {
        if (item.IsStackable())
        {
            bool itemAlreadyInBag = false;
            foreach (Item it in itemList)
            {
                if (it.itemID == item.itemID)
                {
                    it.amount += item.amount;
                    itemAlreadyInBag = true;
                }
            }
            if (!itemAlreadyInBag)
            {
                itemList.Add(item);
            }
        }
        else
        {
            itemList.Add(item);
        }
        bagItemListChangedEvent.Invoke();
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }

    public void AddAllItems()
    {
        foreach (ItemID id in Enum.GetValues(typeof(ItemID)))
        {
            AddItem( new Item { itemID = id, amount = 1 });
        }
    }
}