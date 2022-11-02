using System.Collections.Generic;
using UnityEngine;
using Manapotion.PartySystem;

namespace Manapotion.Actions
{
    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/ActionsManagerScriptableObject")]
    public class ActionsManagerScriptableObject : ScriptableObject
    {
        // holds every possible action that the character can use
        public List<ActionScriptableObject> possibleActions;

        /// <summary>
        /// Make the given member perform an action.
        /// </summary>
        /// <param name="actionName">name of the action</param>
        /// <param name="member">member to perform the action</param>
        /// <returns>the ActionScriptableObject that was performed</returns>
        public ActionScriptableObject PerformAction(string actionName, PartyMember member)
        {
            for (int i = 0; i < possibleActions.Count; i++)
            {
                if (possibleActions[i].action_name == actionName)
                {
                    ManaBehaviour.instance.StartCoroutine(possibleActions[i].PerformAction(member));
                    return possibleActions[i];
                }
            }
            
            return null;
        }

        /// <summary>
        /// Make the given member perform an action.
        /// </summary>
        /// <param name="id">id of the action</param>
        /// <param name="member">member to perform the action</param>
        /// <returns>the ActionScriptableObject that was performed</returns>
        public ActionScriptableObject PerformAction(int id, PartyMember member)
        {
            ManaBehaviour.instance.StartCoroutine(possibleActions[id].PerformAction(member));
            return possibleActions[id];
        }
    }
}