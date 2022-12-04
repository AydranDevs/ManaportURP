using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manapotion.PartySystem;

namespace Manapotion.Actions.Targets
{
    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/Actions/Targets/New TargetDefinitionScriptableObject")]
    public class TargetDefinitionScriptableObject : ScriptableObject
    {
        public bool currentlyTargeting
        {
            get; 
            private set;
        }

        public float targetingRangeUnits;
        public List<Targetable> targetablesList
        {
            get;
            private set;
        }

        public virtual IEnumerator BeginTargeting(PartyMember member)
        {
            if (currentlyTargeting)
            {
                Debug.LogWarning($"{name} is already currently targeting. Starting again...");
            }
            targetablesList = new List<Targetable>();
            currentlyTargeting = true;
            yield break;
        }

        public virtual IEnumerable<Vector3> SelectTarget(PartyMembers member)
        {
            yield break;
        }
    }
}
