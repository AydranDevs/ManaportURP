using System;

namespace Manapotion.Stats
{
    [System.Serializable]
    public class Stat
    {
        public event EventHandler<OnStatModifiedEventArgs> OnStatModifiedEvent;
        public class OnStatModifiedEventArgs : EventArgs
        {
            public StatID statID;
            public int baseValue;
            public int modifiedValue;
        }


        public StatsManagerScriptableObject parent;
        public StatID statID;
        public ModifiableInt value;

        public void Init()
        {
            value.Init();
            value.OnValueChangedEvent += StatModified;
        }

        public void StatModified(object sender, EventArgs e)
        {
            OnStatModifiedEvent?.Invoke(
                this,
                new OnStatModifiedEventArgs{
                    statID = this.statID,
                    baseValue = this.value.baseValue,
                    modifiedValue = this.value.modifiedValue
                }
            );
        }
    }
}