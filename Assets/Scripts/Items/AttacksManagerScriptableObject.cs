using UnityEngine;
using Manapotion.Actions;

namespace Manapotion.Items
{
    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/Items/New AttacksManagerScriptableObject")]
    public class AttacksManagerScriptableObject : ScriptableObject
    {
        [Tooltip("the first element of this array is the weapon's primary attack, second is secondary, etc.")]
        public ActionScriptableObject[] attacksArray;
    }
}
