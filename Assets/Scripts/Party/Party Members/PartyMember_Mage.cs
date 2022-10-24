using System;

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

        public const float MANA_REGEN_COOLDOWN_DEFUALT = 1f;
        public float manaRegenCooldown;
        public bool manaRegenCoolingDown = false;
        
        public const float MANA_REGEN_TIMER_DEFAULT = 1f;
        private float manaRegenTimer;

        void Start()
        {
            manaRegenTimer = MANA_REGEN_TIMER_DEFAULT;
        }

        void Update()
        {
            UpdateManaBar(stats.manaport_stat_manapoints.value, stats.manaport_stat_max_manapoints.value);
        }

        private void UpdateManaBar(float mana, float maxMana)
        {
            OnUpdateManaBar?.Invoke(this, new OnUpdateManaBarEventArgs
            {
                mana = mana,
                maxMana = maxMana
            });
        }

        public void UseMana(float amount)
        {
            stats.manaport_stat_manapoints.value -= amount;
            manaRegenCoolingDown = true;
            manaRegenCooldown = MANA_REGEN_COOLDOWN_DEFUALT;
        }
        
        public float ManaPointsAfterUse(float amount)
        {
            return stats.manaport_stat_manapoints.value - amount;
        }
    }
}
