using System.Collections.Generic;
using UnityEngine;
using Manapotion.PartySystem;
using Manapotion.Stats;

namespace Manapotion.Actions
{
    /// <summary>
    /// Class for managing actions.
    /// </summary>
    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/Actions/New ActionsManagerScriptableObject")]
    public class ActionsManagerScriptableObject : ScriptableObject
    {
        /// <summary>
        /// Holds every action a character could possibly use.
        /// </summary>
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

        /// <summary>
        /// Make the given member perform an action.
        /// </summary>
        /// <param name="a">action scriptable object (usually from an equipped weapon)</param>
        /// <param name="member">member to perform the action</param>
        /// <param name="type">damage type</param>
        /// <param name="element">action elemental type</param>
        /// <returns>the ActionScriptableObject that was performed</returns>
        public ActionScriptableObject PerformAction(ActionScriptableObject a, PartyMember member, DamageInstance.DamageInstanceType type, DamageInstance.DamageInstanceElement element)
        {
            if (a == null || member == null)
            {
                return null;
            }

            for (int i = 0; i < possibleActions.Count; i++)
            {
                if (possibleActions[i] == a)
                {
                    ManaBehaviour.instance.StartCoroutine(possibleActions[i].PerformAction(member, type, element));
                    return possibleActions[i];
                }
            }

            return null;
        }
        
        /// <summary>
        /// Make the given member perform an action.
        /// </summary>
        /// <param name="a">action scriptable object (usually from an equipped weapon)</param>
        /// <param name="member">member to perform the action</param>
        /// <param name="stat">stat that the action should use</param>
        /// <param name="type">damage type</param>
        /// <param name="element">action elemental type</param>
        /// <returns>the ActionScriptableObject that was performed</returns>
        public ActionScriptableObject PerformAction(ActionScriptableObject a, PartyMember member, Stat stat, DamageInstance.DamageInstanceType type, DamageInstance.DamageInstanceElement element)
        {
            if (a == null || member == null)
            {
                return null;
            }

            for (int i = 0; i < possibleActions.Count; i++)
            {
                if (possibleActions[i] == a)
                {
                    ManaBehaviour.instance.StartCoroutine(possibleActions[i].PerformAction(member, stat, type, element));
                    return possibleActions[i];
                }
            }

            return null;
        }
    }
}