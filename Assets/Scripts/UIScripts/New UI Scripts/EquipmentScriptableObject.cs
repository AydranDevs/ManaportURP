using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Manapotion.Equipables;
using Manapotion.PartySystem;

[CreateAssetMenu]
public class EquipmentScriptableObject : ScriptableObject
{
    [NonSerialized]
    public UnityEvent<EquipmentScriptableObject> bagItemEquippedEvent;
    [NonSerialized]
    public UnityEvent<EquipableData, int> equipableUnequippedEvent;

    [field: SerializeField]
    public int charID { get; private set; }

    public EquipableData weapon;
    public EquipableData armour;
    public EquipableData vanity;

    public BagScriptableObject partyBagSciptableObject;

    private void OnEnable()
    {
        if (bagItemEquippedEvent == null)
        {
            bagItemEquippedEvent = new UnityEvent<EquipmentScriptableObject>();
        }
        if (equipableUnequippedEvent == null)
        {
            equipableUnequippedEvent = new UnityEvent<EquipableData, int>();
        }
    }

    /// <summary>
    /// Equip an item
    /// </summary>
    /// <param name="item">item to equip</param>
    public void EquipItem(Item item)
    {
        if (item.itemID == ItemID.manaport_nothing || item.GetMetadata().equipableData == null)
        {
            return;
        }

        var iC = item.GetMetadata().category;

        foreach (var i in item.GetMetadata().equipableData.charIDsThatCanEquip)
        {
            if (i == charID)
            {
                Debug.Log("char can equip");
                // if item in slot is equipped, swap the two.
                if (weapon.equipableID != ItemID.manaport_nothing && iC == ItemCategories.Weapon)
                {
                    UnequipEquipable(weapon);
                }
                else if (armour.equipableID != ItemID.manaport_nothing && iC == ItemCategories.Armour)
                {
                    UnequipEquipable(armour);
                }
                else if (vanity.equipableID != ItemID.manaport_nothing && iC == ItemCategories.Vanity)
                {
                    UnequipEquipable(vanity);
                }
                
                if (iC == ItemCategories.Weapon)
                {
                    weapon = item.GetMetadata().equipableData;
                }
                else if (iC == ItemCategories.Armour)
                {
                    armour = item.GetMetadata().equipableData;
                }
                else
                {
                    vanity = item.GetMetadata().equipableData;
                }
                
                break;
            }
        }
        
        bagItemEquippedEvent.Invoke(this);
        partyBagSciptableObject.RemoveItem(item);   
    }


    /// <summary>
    /// Unequip an equipable
    /// </summary>
    /// <param name="equipableData"></param>
    public void UnequipEquipable(EquipableData equipableData)
    {
        if (equipableData.equipableID == ItemID.manaport_nothing)
        {
            return;
        }

        Debug.Log("Unequipping " + equipableData.equipableID + " from charID: " + charID);

        var iC = equipableData.GetItemIDMetaData().category;
        if (iC == ItemCategories.Weapon)
        {
            weapon = new EquipableData { equipableID = ItemID.manaport_nothing };
        }
        else if (iC == ItemCategories.Armour)
        {
            armour = new EquipableData { equipableID = ItemID.manaport_nothing };
        }
        else
        {
            vanity = new EquipableData { equipableID = ItemID.manaport_nothing };
        }

        equipableUnequippedEvent.Invoke(equipableData, charID);
        partyBagSciptableObject.AddItem(new Item { itemID = equipableData.equipableID, amount = 1});
    }
}