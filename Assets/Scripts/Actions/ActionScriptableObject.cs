using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manapotion.PartySystem;
using Manapotion.Items;

namespace Manapotion.Actions
{
    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/ActionScriptableObject")]
    public class ActionScriptableObject : ScriptableObject
    {
        public ActionID action_id;
        public string action_animationName;

        public List<ItemScriptableObject> action_requiredItems;

        public virtual IEnumerator PerformAction(PartyMember member)
        {
            Debug.Log("Action: " + action_id + " Started. Waiting...");
            yield return new WaitForSeconds(5f);
            Debug.Log("Action: " + action_id + "Completed." );
            yield break;
        }

        public virtual IEnumerator PerformAction(PartyMember member, DamageInstance.DamageInstanceType type, DamageInstance.DamageInstanceElement element)
        {
            yield break;
        }
    }
}