using System.Collections.Generic;
using UnityEngine;
using Manapotion.PartySystem;

[CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/Actions/Targeting/New MultiTargetDefinititon")]
public class MultiTargetDefinition : TargetDefinition
{
    public override IEnumerable<Vector2> SelectTarget(PartyMember member)
    {
        yield break;
    }
}