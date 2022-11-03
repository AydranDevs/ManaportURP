using UnityEngine;
using Manapotion.Stats;

namespace Manapotion.Items
{
    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/New ItemScriptableObject")]
    public class ItemScriptableObject : ScriptableObject
    {
        public ItemCategory itemCategory = ItemCategory.Consumable;
        public string itemName;
        public string itemDescription;

        public Sprite itemSprite;

        public bool stackable;
        public bool equipable;

        public int[] charIDsThatCanEquip;
        public StatsManagerScriptableObject stats;
    }
}
