using System;
using System.Collections.Generic;
using UnityEngine;
using Manapotion.UI;
using Manapotion.PartySystem;

namespace Manapotion.PartySystem.Inventory
{    
    public class Bag
    {
        private Party _party;

        private BagScriptableObject _bagScriptableObject;

        public Bag(Party party)
        {
            _party = party;

            _bagScriptableObject = _party.bagScriptableObject;

            _bagScriptableObject.AddAllItems();
            //_bagScriptableObject.AddItem(new Item { itemID = ItemID.manaport_consumable_restore_mana_potion_I, amount = 999});

            _bagScriptableObject.bagItemListChangedEvent.Invoke();
        }

        public void AddItem(Item item)
        {
            _bagScriptableObject.AddItem(item);
        }
    }
}
