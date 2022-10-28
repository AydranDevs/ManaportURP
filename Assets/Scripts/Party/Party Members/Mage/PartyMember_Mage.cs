using System;
using UnityEngine;

namespace Manapotion.PartySystem
{
    public class PartyMember_Mage : PartyMember
    {
        public static event EventHandler<OnUpdateManaBarEventArgs> OnUpdateManaBar;
        public class OnUpdateManaBarEventArgs : EventArgs
        {
            public float mana;
            public float maxMana;
        }

        // cooldown of 1s before mana starts regenerating
        public const float MANA_REGEN_COOLDOWN_DEFUALT = 1f;
        public float manaRegenCooldown;
        public bool manaRegenCoolingDown = false;

        // internal regen timer (based on mana regeration rate stat)
        private float _manaRegenTimer;
        
        protected override void Init()
        {
            ManaBehaviour.OnUpdate += Update;

            _manaRegenTimer = stats.manaport_stat_manapoints_regen_rate.GetValue();
            MaxMP();

            InitMember();
        }

        protected virtual void InitMember()
        {

        }

        void Update()
        {
            if (manaRegenCoolingDown)
            {
                CoolDownManaRegen();
            }
            if (!manaRegenCoolingDown && stats.manaport_stat_manapoints.GetValue() < stats.manaport_stat_max_manapoints.GetValue())
            {
                RegenMP();
            }

            if (partyMemberState != PartyMemberState.CurrentLeader)
            {
                return;
            }

            // anything that updates UI should go below this.

            UpdateManaBar(stats.manaport_stat_manapoints.GetValue(), stats.manaport_stat_max_manapoints.GetValue());
        }

        private void UpdateManaBar(float value, float maxValue)
        {
            OnUpdateManaBar?.Invoke(this, new OnUpdateManaBarEventArgs
            {
                mana = value,
                maxMana = maxValue
            });
        }

        public void MaxMP()
        {
            stats.manaport_stat_manapoints.Max();
        }
        
        public void RegenMP()
        {
            _manaRegenTimer = _manaRegenTimer - Time.deltaTime;
            if (_manaRegenTimer <= 0f)
            {
                stats.manaport_stat_manapoints.SetValue(stats.manaport_stat_manapoints.GetValue() + stats.manaport_stat_manapoints_regen_amount.GetValue());
                _manaRegenTimer = stats.manaport_stat_manapoints_regen_rate.GetValue();
            }
        }

        private void CoolDownManaRegen()
        {
            manaRegenCooldown = manaRegenCooldown - Time.deltaTime;
            if (manaRegenCooldown <= 0f)
            {
                manaRegenCoolingDown = false;
                manaRegenCooldown = MANA_REGEN_COOLDOWN_DEFUALT;
            }
        }

        public void UseMana(float amount)
        {
            stats.manaport_stat_manapoints.SetValue(stats.manaport_stat_manapoints.GetValue() - amount);
            manaRegenCoolingDown = true;
            manaRegenCooldown = MANA_REGEN_COOLDOWN_DEFUALT;
        }
        
        public float ManaPointsAfterUse(float amount)
        {
            return stats.manaport_stat_manapoints.GetValue() - amount;
        }
    }
}
