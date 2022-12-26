using System.Collections.Generic;
using UnityEngine;
using Manapotion.PartySystem;
using Manapotion.Stats;

namespace Manapotion.Actions
{
    /// <summary>
    /// Class for managing actions.
    /// </summary>
    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/Actions/New ActionsManager")]
    public class PartyMemberActionsManager : ScriptableObject
    {
        /// <summary>
        /// Holds every action a character could possibly use.
        /// </summary>
        public List<APartyMemberAction> possibleActions;

        /// <summary>
        /// Make the given member perform an action.
        /// </summary>
        /// <param name="action">action scriptable object (usually from an equipped weapon)</param>
        /// <param name="member">member to perform the action</param>
        /// <param name="type">damage type</param>
        /// <param name="element">action elemental type</param>
        /// <returns>the APartyMemberAction that was performed</returns>
        public APartyMemberAction PerformAction(APartyMemberAction action, PartyMember member, DamageInstance damageInstance = null)
        {
            if (action == null || member == null)
            {
                return null;
            }

            for (int i = 0; i < possibleActions.Count; i++)
            {
                if (possibleActions[i] == action)
                {
                    member.StartCoroutine(possibleActions[i].PerformAction(member, damageInstance));
                    return possibleActions[i];
                }
            }

            return null;
        }
    }
}