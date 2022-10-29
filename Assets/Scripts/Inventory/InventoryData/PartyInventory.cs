using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manapotion.PartySystem;
using Manapotion.Equipables;

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
            _party = party;
            bag = new Bag(_party);
        }
    }
}
