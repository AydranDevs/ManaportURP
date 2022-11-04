using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manapotion.Stats;

namespace Manapotion.Items
{
    [Serializable]
    public class Item
    {
        public ItemScriptableObject itemScriptableObject;
        public List<Buff> itemBuffs;
        public int amount;

        public Sprite GetSprite()
        {
            return itemScriptableObject.itemSprite;
        }

        public override string ToString()
        {
            return itemScriptableObject.itemName;
        }
    }
}
