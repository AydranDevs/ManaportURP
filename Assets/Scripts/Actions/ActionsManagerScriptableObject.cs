using System.Collections.Generic;
using UnityEngine;

namespace Manapotion.Actions
{
    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/ActionsManagerScriptableObject")]
    public class ActionsManagerScriptableObject : ScriptableObject
    {
        // holds every possible action that the character can use
        public List<ActionScriptableObject> possibleActions;
    }
}