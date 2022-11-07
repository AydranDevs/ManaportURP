using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Manapotion.Equipables;
using Manapotion.PartySystem;
using Manapotion.Items;
using Manapotion.Stats;

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

        foreach (var i in item.itemScriptableObject.charIDsThatCanEquip)
        {
            if (i == charID)
            {
                // if item in slot is equipped, swap the two.
                if (weapon.itemScriptableObject != null && item.itemScriptableObject.itemCategory == ItemCategory.Weapon)
                {
                    UnequipItem(weapon);
                }
                else if (armour.itemScriptableObject != null && item.itemScriptableObject.itemCategory == ItemCategory.Armour)
                {
                    UnequipItem(armour);
                }
                else if (vanity.itemScriptableObject != null && item.itemScriptableObject.itemCategory == ItemCategory.Vanity)
                {
                    UnequipItem(vanity);
                }
                
                if (item.itemScriptableObject.itemCategory == ItemCategory.Weapon)
                {
                    weapon = item;
                }
                else if (item.itemScriptableObject.itemCategory == ItemCategory.Armour)
                {
                    armour = item;
                }
                else
                {
                    vanity = item;
                }
                
                break;
            }
        }

        if (item.itemScriptableObject.stats != null)
        {
            foreach (var s in item.itemScriptableObject.stats.statArray)
            {
                var statID = s.statID;
                Party.GetMember(charID).statsManagerScriptableObject.GetStat(statID).value.AddModifier(
                    new Buff
                    {
                        stat = s,
                        value = s.value.baseValue
                    }
                );
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

        // if the item has stats, remove the buff(s) given to the character
        if (item.itemScriptableObject.stats != null)
        {
            foreach (var s in item.itemScriptableObject.stats.statArray)
            {
                var statID = s.statID;
                Debug.Log(Party.GetMember(charID));
                Party.GetMember(charID).statsManagerScriptableObject.GetStat(statID).value.RemoveModifier(
                    new Buff
                    {
                        stat = s,
                        value = s.value.baseValue
                    }
                );
            }
        }
        
        // find the correct item to unequip
        if (item.itemScriptableObject.itemCategory == ItemCategory.Weapon)
        {
            weapon = new Item { itemScriptableObject = null, amount = 0 };
        }
        else if (item.itemScriptableObject.itemCategory == ItemCategory.Armour)
        {
            armour = new Item { itemScriptableObject = null, amount = 0 };
        }
        else
        {
            vanity = new Item { itemScriptableObject = null, amount = 0 };
        }


        equipableUnequippedEvent.Invoke(item, charID);
        partyBagSciptableObject.AddItem(new Item { itemScriptableObject = item.itemScriptableObject, amount = 1});
    }
}