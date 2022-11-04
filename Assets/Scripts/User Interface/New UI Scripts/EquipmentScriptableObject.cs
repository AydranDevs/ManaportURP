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

    private Dictionary<int, Buff> _appliedBuffs;

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
        if (_appliedBuffs == null)
        {
            _appliedBuffs = new Dictionary<int, Buff>();
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
                    weapon = item;
                }
                else if (iC == ItemCategory.Armour)
                {
                    armour = item;
                }
                else
                {
                    vanity = item;
                }
                Debug.Log("equipped: " + item.ToString());
                
                break;
            }
        }

        if (item.itemScriptableObject.stats != null)
        {
            foreach (var s in item.itemScriptableObject.stats.stats)
            {
                var statID = s.statID;
                var newBuff = new Buff
                {
                    stat = s,
                    value = s.value.baseValue
                };
                Party.GetMember(charID).statsManagerScriptableObject.GetStat(statID).value.AddModifier(newBuff);
                try
                {
                    _appliedBuffs[Party.GetMember(charID).statsManagerScriptableObject.GetStat(statID).value.modifiers.IndexOf(newBuff)] = newBuff;
                }
                catch (KeyNotFoundException)
                {
                    var o_value = newBuff.value;
                    var n_newBuff = new Buff
                    {
                        stat = s,
                        value = o_value + _appliedBuffs[Party.GetMember(charID).statsManagerScriptableObject.GetStat(statID).value.modifiers.IndexOf(newBuff)].value
                    };
                    _appliedBuffs[Party.GetMember(charID).statsManagerScriptableObject.GetStat(statID).value.modifiers.IndexOf(newBuff)] = n_newBuff;
                }
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

        if (item.itemScriptableObject.stats != null)
        {
            foreach (var s in item.itemScriptableObject.stats.stats)
            {
                var statID = s.statID;
                for(
                    int i = 0;
                    i < Party.GetMember(charID).statsManagerScriptableObject.GetStat(statID).value.modifiers.Count;
                    i++
                )
                {
                    if (Party.GetMember(charID).statsManagerScriptableObject.GetStat(statID).value.modifiers[i] == _appliedBuffs[i])
                    {
                        Party.GetMember(charID).statsManagerScriptableObject.GetStat(statID).value.RemoveModifier(_appliedBuffs[i]);
                        
                        foreach (var buff in item.itemBuffs)
                        {
                            if (buff.stat.statID == _appliedBuffs[i].stat.statID)
                            {
                                if (_appliedBuffs[i].value - buff.value > 0)
                                {
                                    _appliedBuffs[i].value -= buff.value;
                                    break;
                                }
                                else
                                {
                                    _appliedBuffs.Remove(i);
                                    break;
                                }
                            } 
                        }
                    }
                }
            }
        }

        var iC = item.itemScriptableObject.itemCategory;
        if (iC == ItemCategory.Weapon)
        {
            weapon = new Item { itemScriptableObject = null, amount = 0 };
        }
        else if (iC == ItemCategory.Armour)
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