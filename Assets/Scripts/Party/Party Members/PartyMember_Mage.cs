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

        void Update()
        {
            UpdateManaBar(stats.manaPoints.value, stats.manaPoints.maxValue);
        }

        private void UpdateManaBar(float mana, float maxMana)
        {
            OnUpdateManaBar?.Invoke(this, new OnUpdateManaBarEventArgs
            {
                mana = mana,
                maxMana = maxMana
            });
        }
    }
}
