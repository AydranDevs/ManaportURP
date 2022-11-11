using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Manapotion.Equipables;
using Manapotion.PartySystem;
using Manapotion.Stats;

namespace Manapotion.Items
{    
    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/Items/New EquipmentManagerScriptableObject")]
    public class EquipmentManagerScriptableObject : ScriptableObject
    {
        /// <summary>
        /// Invoked when an item is equipped
        /// </summary>
        public event EventHandler<BagItemEquippedEventArgs> BagItemEquippedEvent;
        public class BagItemEquippedEventArgs : EventArgs
        {
            public EquipmentManagerScriptableObject equipmentManagerScriptableObject;
        }

        /// <summary>
        /// Invoked when an item is unequipped
        /// </summary>
        public event EventHandler<BagItemUnequippedEventArgs> BagItemUnequippedEvent;
        public class BagItemUnequippedEventArgs : EventArgs
        {
            public Item unequippedItem;
            public int charID;
        }

        [SerializeField]
        private int charID;

        // equipment Items
        public Item weapon;
        public Item armour;
        public Item vanity;

        public BagScriptableObject partyBagSciptableObject;

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
                    
                    // equip the item to the right slot according to it's category
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

            // if the item has a StatManagerScriptableObject attached, apply modifiers to this character's stats.
            if (item.itemScriptableObject.statsManagerScriptableObject != null)
            {
                foreach (var s in item.itemScriptableObject.statsManagerScriptableObject.statArray)
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
            
            // invoke the equipped event so that Equipment UI can be updated accordingly and remove the item from the bag
            // to prevent duplication
            BagItemEquippedEvent?.Invoke(this,
                new BagItemEquippedEventArgs
                {
                    equipmentManagerScriptableObject = this
                }
            );
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
            if (item.itemScriptableObject.statsManagerScriptableObject != null)
            {
                foreach (var s in item.itemScriptableObject.statsManagerScriptableObject.statArray)
                {
                    var statID = s.statID;
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

            // invoke the unequipped event and add the item back to the bag
            // to prevent the player from losing the item
            BagItemUnequippedEvent?.Invoke(
                this,
                new BagItemUnequippedEventArgs
                {
                    unequippedItem = item,
                    charID = this.charID
                }
            );
            partyBagSciptableObject.AddItem(new Item { itemScriptableObject = item.itemScriptableObject, amount = 1});
        }
    }
}
