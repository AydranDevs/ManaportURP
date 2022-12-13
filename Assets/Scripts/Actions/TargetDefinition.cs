using System.Collections.Generic;
using UnityEngine;
using Manapotion.PartySystem;

[CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/Actions/Targeting/New TargetDefinititon")]
public class TargetDefinition : ScriptableObject
{
	public virtual IEnumerable<Vector2> SelectTarget(PartyMember member)
    {
        yield return new Vector2(69f, 420f);
    }
}