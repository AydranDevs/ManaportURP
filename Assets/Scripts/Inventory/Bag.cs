using System;
using UnityEngine;

namespace Inventory {
    
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

        MAXCOUNT
    }

    public class Bag {
        public BagSlot[] slots = new BagSlot[(int)ItemID.MAXCOUNT];

        public Bag() {
            // initialize bag slots
            for (int i = 0; i < (int)ItemID.MAXCOUNT; i++) {
                slots[i] = new BagSlot((ItemID)i, 0);
                
            }

            // retrieve saved inventory contents here
            // ...
            
            // Update inventory ui with filled bag slots 
            // this will ALWAYS HAPPEN AFTER RETRIEVING SAVED INVENTORY!!!
            InventoryUIHandler.Instance.filledBagSlots = GetFilledBagSlots();
        }

        public void AddItem(ItemID itemId, int quantity) {
            foreach (var slot in slots) {
                if (slot.itemId == itemId) {
                    slot.quantity += quantity;
                    InventoryUIHandler.Instance.filledBagSlots = GetFilledBagSlots();
                    break;
                }
            }
        }

        public void RemoveItem(ItemID itemId, int quantity) {
            foreach (var slot in slots) {
                if (slot.itemId == itemId) {
                    slot.quantity -= quantity;
                    InventoryUIHandler.Instance.filledBagSlots = GetFilledBagSlots();
                    break;
                }
            }
        }

        public int GetFilledBagSlots() {
            int num = 0;
            foreach (var slot in slots) {
                if (slot.quantity > 0) {
                    num++;
                }
            }
            return num;
        }
    }
}
