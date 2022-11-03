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
        /// <param name="actionID">ID of the action</param>
        /// <param name="member">member to perform the action</param>
        /// <returns>the ActionScriptableObject that was performed</returns>
        public ActionScriptableObject PerformAction(ActionID actionID, PartyMember member)
        {
            for (int i = 0; i < possibleActions.Count; i++)
            {
                if (possibleActions[i].action_id == actionID)
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
        /// <param name="actionID">ID of the action</param>
        /// <param name="member">member to perform the action</param>
        /// <param name="type">damage type</param>
        /// <param name="element">action elemental type</param>
        /// <returns>the ActionScriptableObject that was performed</returns>
        public ActionScriptableObject PerformAction(ActionID actionID, PartyMember member, DamageInstance.DamageInstanceType type, DamageInstance.DamageInstanceElement element)
        {
            for (int i = 0; i < possibleActions.Count; i++)
            {
                if (possibleActions[i].action_id == actionID)
                {
                    ManaBehaviour.instance.StartCoroutine(possibleActions[i].PerformAction(member, type, element));
                    return possibleActions[i];
                }
            }
            
            return null;
        }
    }
}