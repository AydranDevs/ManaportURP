using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory {
    public class BagSlot {
        public ItemID itemId;
        public Sprite itemIcon;
        public int quantity = 0;

        public BagSlot(ItemID itemId, int quantity) {
            this.itemId = itemId;
            this.quantity = quantity;
        }
    }
}
