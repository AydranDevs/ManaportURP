using System.Collections;
using System.Collections.Generic;
using Aarthificial.Reanimation;
using Manapotion.PartySystem;
using UnityEngine;
using Manapotion.Stats;

namespace Manapotion.Actions
{
    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/Actions/New Combo")]
    public class PartyMemberCombo : APartyMemberAction
    {
        [Tooltip("The action that is required to be active for this one to be performed.")]
        public APartyMemberAction requiredAction;
        
        public override string GetActionID()
        {
            return this.name;
        }
        
        public StatID modifierStatID;
        public override StatID GetModifierStatID()
        {
            return modifierStatID;
        }
        public List<PartyMemberMeleeAttack> attacksList;
        
        [System.NonSerialized]
        private int _currentlyPerformingAttackIndex = 0;
        [System.NonSerialized]
        private bool _willAdvanceToNextAttack = false;
        
        public override IEnumerator PerformAction(PartyMember member, DamageInstance damageInstance = null)
        {
            if (damageInstance == null)
            {
                yield break;
            }
            if (_willAdvanceToNextAttack)
            {
                yield break;
            }
            
            if (attacksList[_currentlyPerformingAttackIndex].isActive && !_willAdvanceToNextAttack)
            {
                Debug.Log($"Clicked during {attacksList[_currentlyPerformingAttackIndex].name}");
                attacksList[_currentlyPerformingAttackIndex].OnMeleeAttackEnded += OnMeleeAttackEnded;
                _willAdvanceToNextAttack = true;
                yield break;
            }

            member.StartCoroutine(attacksList[0].PerformAction(member, damageInstance));
            _currentlyPerformingAttackIndex = 0;

            yield break;
        }

        public void OnMeleeAttackEnded(object sender, PartyMemberMeleeAttack.OnMeleeAttackEndedArgs e)
        {
            attacksList[_currentlyPerformingAttackIndex].OnMeleeAttackEnded -= OnMeleeAttackEnded;
            
            Debug.Log("Attack ended");
            AdvanceToNextAttack(e.member);
        }

        public void AdvanceToNextAttack(PartyMember member)
        {
            _currentlyPerformingAttackIndex++;
            if (_currentlyPerformingAttackIndex > attacksList.Count - 1)
            {
                _currentlyPerformingAttackIndex = 0;
            }
            member.StartCoroutine(attacksList[_currentlyPerformingAttackIndex].PerformAction(member));
            _willAdvanceToNextAttack = false;
        }
    }
}
