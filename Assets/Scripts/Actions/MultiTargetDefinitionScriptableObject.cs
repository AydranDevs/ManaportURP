using System.Collections.Generic;
using UnityEngine;

namespace Manapotion.Actions.Targets
{
    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/Actions/Targets/New MultiTargetDefinitionScriptableObject")]
    public class MultiTargetDefinitionScriptableObject : TargetDefinitionScriptableObject
    {
        public override IEnumerable<Vector3> SelectTarget(PartyMembers member)
        {
            yield break;
        }
    }
}
