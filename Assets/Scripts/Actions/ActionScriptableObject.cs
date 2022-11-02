using System.Collections;
using UnityEngine;
using Manapotion.PartySystem;

namespace Manapotion.Actions
{
    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/ActionScriptableObject")]
    public class ActionScriptableObject : ScriptableObject
    {
        public string action_name;
        public string action_animationName;

        public virtual IEnumerator PerformAction(PartyMember member)
        {
            Debug.Log(member.gameObject.name + " performed action: " + action_name);
            return null;
        }
    }
}