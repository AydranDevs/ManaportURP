using Aarthificial.Reanimation;
using Manapotion.PartySystem;
using UnityEngine;

namespace Manapotion.Actions
{
    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/Actions/New ComboAttackScriptableObject")]
    public class ComboAttackScriptableObject : ActionScriptableObject
    {
        [System.NonSerialized]
        private PartyMember _member;

        public int attacksInCombo = 3;
        
        private int currentAttackIndex = 0;

        private ReanimatorListener ComboAttackEnd;
        private ReanimatorListener CanDropCombo;

        [SerializeField]
        // [System.NonSerialized]
        private bool _checkingIfCanAdvanceToNextComboAttack = false;
        [SerializeField]
        // [System.NonSerialized]
        private bool _willAdvanceToNextComboAttack = false;

        void OnEnable() {
            ComboAttackEnd += WaitToAdvanceToNextAttack;
            CanDropCombo += CheckIfCanAdvanceToNextAttack;
        }

        protected override void HandlePerformAction(PartyMember member, DamageInstance damageInstance = null)
        {
            _member = member;
            
            if (currentAttackIndex == 0)
            {
                member.characterRenderer.GetReanimator().AddListener("comboAttackEnd", ComboAttackEnd);
                member.characterRenderer.GetReanimator().AddListener("canDropComboEvent", CanDropCombo);
            }

            HandleComboAttack(member, damageInstance);
        }

        private void HandleComboAttack(PartyMember member, DamageInstance damageInstance)
        { 
            if (_checkingIfCanAdvanceToNextComboAttack)
            {
                _willAdvanceToNextComboAttack = true;
            }
        }

        public void WaitToAdvanceToNextAttack()
        {
            _checkingIfCanAdvanceToNextComboAttack = true;
        }

        public void CheckIfCanAdvanceToNextAttack()
        {
            if (_willAdvanceToNextComboAttack)
            {
                return;
            }

            _member.characterRenderer.GetReanimator().Set("state", 0);
            _member.characterRenderer.GetReanimator().Set("abilityState", 1);
            _member.characterRenderer.GetReanimator().Set("piquretta", 0);
            _checkingIfCanAdvanceToNextComboAttack = false;
            Debug.Log("No!!");
        }

        void OnDestroy() {
            currentAttackIndex = 0;
            _checkingIfCanAdvanceToNextComboAttack = false;
            _willAdvanceToNextComboAttack = false;
        }
    }
}
