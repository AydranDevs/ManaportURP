using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory {
    

    public class PartyInventory : MonoBehaviour {
        public static PartyInventory Instance;
    

        public Bag bag;
        public Equipment equipment;
        public Beastiary Beastiary;

        private void Awake() {
            Instance = this;
        }

        private void Start() {
            bag = new Bag();
        }
    }
}
