using System;
using UnityEngine;
using Manapotion.UI;

namespace Manapotion.PartySystem.Inventory
{    
    public enum ItemID 
    { 
        CONSUMABLE_RESTORE_HEALTH_I_POTION,
        CONSUMABLE_RESTORE_MANA_I_POTION,
        CONSUMABLE_REGEN_STAMINA_I_POTION,
        CONSUMABLE_REGEN_MANA_I_POTION,
        CONSUMABLE_REGEN_HEALTH_I_POTION,
        CONSUMABLE_BOOST_PYRO_I_POTION,
        CONSUMABLE_BOOST_CRYO_I_POTION,
        CONSUMABLE_BOOST_TOXI_I_POTION,
        CONSUMABLE_BOOST_VOLT_I_POTION,
        CONSUMABLE_EFFECT_INVINCIBILITY_I_POTION,
        CONSUMABLE_EFFECT_ABSORPTION_I_POTION,
        CONSUMABLE_EFFECT_RAGE_I_POTION,
        CONSUMABLE_RESTORE_HEALTH_II_POTION,
        CONSUMABLE_RESTORE_MANA_II_POTION,
        CONSUMABLE_REGEN_STAMINA_II_POTION,
        CONSUMABLE_REGEN_MANA_II_POTION,
        CONSUMABLE_REGEN_HEALTH_II_POTION,
        CONSUMABLE_BOOST_PYRO_II_POTION,
        CONSUMABLE_BOOST_CRYO_II_POTION,
        CONSUMABLE_BOOST_TOXI_II_POTION,
        CONSUMABLE_BOOST_VOLT_II_POTION,
        CONSUMABLE_EFFECT_INVINCIBILITY_II_POTION,
        CONSUMABLE_EFFECT_ABSORPTION_II_POTION,
        CONSUMABLE_EFFECT_RAGE_II_POTION,
        CONSUMABLE_RESTORE_HEALTH_III_POTION,
        CONSUMABLE_RESTORE_MANA_III_POTION,
        CONSUMABLE_REGEN_STAMINA_III_POTION,
        CONSUMABLE_REGEN_MANA_III_POTION,
        CONSUMABLE_REGEN_HEALTH_III_POTION,
        CONSUMABLE_BOOST_PYRO_III_POTION,
        CONSUMABLE_BOOST_CRYO_III_POTION,
        CONSUMABLE_BOOST_TOXI_III_POTION,
        CONSUMABLE_BOOST_VOLT_III_POTION,
        CONSUMABLE_EFFECT_INVINCIBILITY_III_POTION,
        CONSUMABLE_EFFECT_ABSORPTION_III_POTION,
        CONSUMABLE_EFFECT_RAGE_III_POTION,

        WEAPON_TRAINING_TOME,
        WEAPON_AZURE_PARASOL,
        WEAPON_GARDEN_SHEAR,
        ARMOUR_VIOLET_ROBES,
        ARMOUR_TIMID_RAINCOAT,
        ARMOUR_SUNBEATEN_OVERALLS,
        VANITY_GOLD_RING_HAT,
        VANITY_CUTESY_BLUE_RAINBOOTS,
        VANITY_SCARLET_RINGS,

        MAXCOUNT
    }

    public class Bag
    {
        public BagSlot[] slots = new BagSlot[(int)ItemID.MAXCOUNT];

        private Party _party;
        private MainUIManager _main;

        public Bag(Party party)
        {
            _party = party;
            _main = MainUIManager.Instance;

            // initialize bag slots
            for (int i = 0; i < (int)ItemID.MAXCOUNT; i++)
            {
                slots[i] = new BagSlot((ItemID)i, 0);
                
            }

            // retrieve saved bags contents here
            AddItem(ItemID.WEAPON_TRAINING_TOME, 1);
            AddItem(ItemID.WEAPON_AZURE_PARASOL, 1);
            AddItem(ItemID.WEAPON_GARDEN_SHEAR, 1);
            AddItem(ItemID.ARMOUR_VIOLET_ROBES, 1);
            AddItem(ItemID.ARMOUR_TIMID_RAINCOAT, 1);
            AddItem(ItemID.ARMOUR_SUNBEATEN_OVERALLS, 1);
            AddItem(ItemID.VANITY_GOLD_RING_HAT, 1);
            AddItem(ItemID.VANITY_CUTESY_BLUE_RAINBOOTS, 1);
            AddItem(ItemID.VANITY_SCARLET_RINGS, 1);
            // ...
            
            // Update bag ui with filled bag slots 
            // this will ALWAYS HAPPEN AFTER RETRIEVING SAVED INVENTORY!!!
            _main.inventoryUIManager.bagUI.filledBagSlots = GetFilledBagSlots();
        }

        public void AddItem(ItemID itemId, int quantity)
        {
            foreach (var slot in slots)
            {
                if (slot.itemId == itemId)
                {
                    slot.quantity += quantity;
                    _main.inventoryUIManager.bagUI.filledBagSlots = GetFilledBagSlots();
                    break;
                }
            }
        }

        public void RemoveItem(ItemID itemId, int quantity)
        {
            foreach (var slot in slots)
            {
                if (slot.itemId == itemId)
                {
                    slot.quantity -= quantity;
                    _main.inventoryUIManager.bagUI.filledBagSlots = GetFilledBagSlots();
                    break;
                }
            }
        }

        public int GetFilledBagSlots()
        {
            int num = 0;
            foreach (var slot in slots)
            {
                if (slot.quantity > 0)
                {
                    num++;
                }
            }
            return num;
        }
    }
}
