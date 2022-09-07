using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manapotion.PartySystem;

namespace Manapotion.PartySystem.Inventory
{
    public class PartyInventory
    {
        private Party _party;
        
        public Bag bag { get; private set; }
        public Equipment equipment { get; private set; }
        public Beastiary Beastiary { get; private set; }

        public PartyInventory(Party party)
        {
            bag = new Bag(_party);
        }
    }
}
