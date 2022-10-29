using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Manapotion.PartySystem;
using Manapotion.Items;

[CreateAssetMenu]
public class BagScriptableObject : ScriptableObject
{
    [field: SerializeField]
    public List<Item> itemList { get; private set; }

    [NonSerialized]
    public UnityEvent bagItemListChangedEvent;
    [NonSerialized]
    public UnityEvent bagItemUsedEvent;

    public ItemScriptableObject testItemScriptableObject;

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
    }

    public void AddItem(Item item)
    {
        Item itemToAdd = new Item { itemScriptableObject = item.itemScriptableObject, amount = item.amount };
        if (itemToAdd.itemScriptableObject == null)
        {
            return;
        }

        if (itemToAdd.itemScriptableObject.stackable)
        {
            bool itemAlreadyInBag = false;
            foreach (Item it in itemList)
            {
                if (it.itemScriptableObject == itemToAdd.itemScriptableObject)
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
            Debug.Log("unstackable object(s) added to inventory (" + item.amount + " " + item.ToString() + "s.)");
            itemList.Add(itemToAdd);
        }
        bagItemListChangedEvent.Invoke();
    }

    public void RemoveItem(Item item)
    {
        if (item.itemScriptableObject == null)
        {
            return;
        }

        foreach (Item it in itemList)
        {
            if (it.itemScriptableObject == item.itemScriptableObject)
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
        if (item.itemScriptableObject == null)
        {
            return;
        }

        // item.itemScriptableObject.UseEvent(i);
        RemoveItem(item);
        bagItemUsedEvent.Invoke();
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}