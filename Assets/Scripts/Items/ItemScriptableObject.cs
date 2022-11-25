using System.Collections.Generic;
using UnityEngine;
using Manapotion.Stats;

namespace Manapotion.Items
{
    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/Items/New ItemScriptableObject")]
    public class ItemScriptableObject : ScriptableObject
    {
        public ItemCategory itemCategory = ItemCategory.Consumable;
        public string itemName;
        [TextArea(15,20)]
        public string itemDescription;

        public Sprite itemSprite;

        public bool stackable;
        public bool equipable;

        public int[] charIDsThatCanEquip;

        // managers
        public StatsManagerScriptableObject statsManagerScriptableObject;
        public AttacksManagerScriptableObject attacksManagerScriptableObject;
    }
}
