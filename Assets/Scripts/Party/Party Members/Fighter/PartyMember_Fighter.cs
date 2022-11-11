using System;
using UnityEngine;
using Manapotion.Attacking;
using Manapotion.Stats;

namespace Manapotion.PartySystem
{
    public class PartyMember_Fighter : PartyMember
    {
        public static event EventHandler<OnUpdateStaminaBarEventArgs> OnUpdateStaminaBar;
        public class OnUpdateStaminaBarEventArgs : EventArgs
        {
            public float stamina;
            public float maxStamina;
        }

        public Attack attack;

        // cooldown of 1s before stamina starts regenerating
        private const float STAMINA_REGEN_COOLDOWN_DEFUALT = 1f;
        private float _staminaRegenCooldown;
        private bool _staminaRegenCoolingDown = false; // true when stamina regen is currently cooling down

        // internal regen timer (based on stamina regeration rate stat)
        private float _staminaRegenTimer;

        protected override void Init()
        {
            ManaBehaviour.OnUpdate += Update;

            _staminaRegenTimer = stats.manaport_stat_staminapoints_regen_rate.GetValue();
            MaxSP();

            attack = new Attack(this);

            InitMember();
        }

        protected virtual void InitMember()
        {

        }

        void Update()
        {
            if (_staminaRegenCoolingDown)
            {
                CoolDownStaminaRegen();
            }
            if (!_staminaRegenCoolingDown && stats.manaport_stat_staminapoints.GetValue() < stats.manaport_stat_max_staminapoints.GetValue())
            {
                RegenSP();
            }

            if (partyMemberState != PartyMemberState.CurrentLeader)
            {
                return;
            }

            // anything that updates UI should go below this.

            UpdateStaminaBar(stats.manaport_stat_staminapoints.GetValue(), stats.manaport_stat_max_staminapoints.GetValue());
        }

        #region Stamina Point Management
        private void UpdateStaminaBar(float value, float maxValue)
        {
            OnUpdateStaminaBar?.Invoke(this, new OnUpdateStaminaBarEventArgs
            {
                stamina = value,
                maxStamina = maxValue
            });
        }
        
        public void MaxSP()
        {
            stats.manaport_stat_staminapoints.Max();
        }

        private void RegenSP()
        {
            _staminaRegenTimer = _staminaRegenTimer - Time.deltaTime;
            if (_staminaRegenTimer <= 0f)
            {
                stats.manaport_stat_staminapoints.SetValue(stats.manaport_stat_staminapoints.GetValue() + stats.manaport_stat_staminapoints_regen_amount.GetValue());
                _staminaRegenTimer = stats.manaport_stat_staminapoints_regen_rate.GetValue();
            }
        }
        
        private void CoolDownStaminaRegen()
        {
            _staminaRegenCooldown = _staminaRegenCooldown - Time.deltaTime;
            if (_staminaRegenCooldown <= 0f)
            {
                _staminaRegenCoolingDown = false;
                _staminaRegenCooldown = STAMINA_REGEN_COOLDOWN_DEFUALT;
            }
        }

        public void UseStamina(int amount)
        {
            if (!pointsManagerScriptableObject.GetPointScriptableObject(PointID.Staminapoints).value.CanSubtract(amount))
            {
                // not enough stamina
                return;
            }

            pointsManagerScriptableObject.GetPointScriptableObject(PointID.Staminapoints).value.currentValue -= amount;
            _staminaRegenCoolingDown = true;
            _staminaRegenCooldown = STAMINA_REGEN_COOLDOWN_DEFUALT;
        }

        public float StaminaPointsAfterUse(int amount)
        {
            var pt = pointsManagerScriptableObject.GetPointScriptableObject(PointID.Staminapoints);
            return pt.value.currentValue - amount;
        }
        #endregion

        #region Attacking
        public override void PerformMainAction(int action)
        {
            if (action == 0)
            {
                actionsManagerScriptableObject.PerformAction(
                    equipmentManagerScriptableObject.weapon.itemScriptableObject.attacksManagerScriptableObject.attacksArray[0],
                    this,
                    statsManagerScriptableObject.GetStat(Stats.StatID.STR),
                    (Actions.DamageInstance.DamageInstanceType)damageType,
                    (Actions.DamageInstance.DamageInstanceElement)primaryActionElement
                );
            }
            else
            {
                actionsManagerScriptableObject.PerformAction(
                    equipmentManagerScriptableObject.weapon.itemScriptableObject.attacksManagerScriptableObject.attacksArray[1],
                    this,
                    statsManagerScriptableObject.GetStat(Stats.StatID.STR),
                    (Actions.DamageInstance.DamageInstanceType)damageType,
                    (Actions.DamageInstance.DamageInstanceElement)primaryActionElement
                );
            }
        }
        #endregion
    }
}
