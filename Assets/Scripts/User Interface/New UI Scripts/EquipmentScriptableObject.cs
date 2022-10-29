using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Manapotion.Equipables;
using Manapotion.PartySystem;
using Manapotion.Items;

[CreateAssetMenu]
public class EquipmentScriptableObject : ScriptableObject
{
    [NonSerialized]
    public UnityEvent<EquipmentScriptableObject> bagItemEquippedEvent;
    [NonSerialized]
    public UnityEvent<Item, int> equipableUnequippedEvent;

    [field: SerializeField]
    public int charID { get; private set; }

    public Item weapon;
    public Item armour;
    public Item vanity;

    public BagScriptableObject partyBagSciptableObject;

    private void OnEnable()
    {
        if (bagItemEquippedEvent == null)
        {
            bagItemEquippedEvent = new UnityEvent<EquipmentScriptableObject>();
        }
        if (equipableUnequippedEvent == null)
        {
            equipableUnequippedEvent = new UnityEvent<Item, int>();
        }
    }

    /// <summary>
    /// Equip an item
    /// </summary>
    /// <param name="item">item to equip</param>
    public void EquipItem(Item item)
    {
        if (item.itemScriptableObject == null || item.itemScriptableObject.equipable == false)
        {
            return;
        }

        var iC = item.itemScriptableObject.itemCategory;

        foreach (var i in item.itemScriptableObject.charIDsThatCanEquip)
        {
            if (i == charID)
            {
                // if item in slot is equipped, swap the two.
                if (weapon.itemScriptableObject != null && iC == ItemCategory.Weapon)
                {
                    UnequipItem(weapon);
                }
                else if (armour.itemScriptableObject != null && iC == ItemCategory.Armour)
                {
                    UnequipItem(armour);
                }
                else if (vanity.itemScriptableObject != null && iC == ItemCategory.Vanity)
                {
                    UnequipItem(vanity);
                }
                
                if (iC == ItemCategory.Weapon)
                {
                    // weapon = item.itemScriptableObject;
                }
                else if (iC == ItemCategory.Armour)
                {
                    // armour = item.itemScriptableObject;
                }
                else
                {
                    // vanity = item.item.itemScriptableObject;
                }
                Debug.Log("equipped: " + item.ToString());
                
                break;
            }
        }
        
        bagItemEquippedEvent.Invoke(this);
        partyBagSciptableObject.RemoveItem(item);   
    }


    /// <summary>
    /// Unequip an item
    /// </summary>
    /// <param name="item"></param>
    public void UnequipItem(Item item)
    {
        if (item.itemScriptableObject == null)
        {
            return;
        }

        Debug.Log("Unequipping " + item.ToString() + " from charID: " + charID);

        var iC = item.itemScriptableObject.itemCategory;
        if (iC == ItemCategory.Weapon)
        {
            weapon = new Item { itemScriptableObject = item.itemScriptableObject };
        }
        else if (iC == ItemCategory.Armour)
        {
            armour = new Item { itemScriptableObject = item.itemScriptableObject };
        }
        else
        {
            vanity = new Item { itemScriptableObject = item.itemScriptableObject };
        }

        equipableUnequippedEvent.Invoke(item, charID);
        partyBagSciptableObject.AddItem(new Item { itemScriptableObject = item.itemScriptableObject, amount = 1});
    }
}