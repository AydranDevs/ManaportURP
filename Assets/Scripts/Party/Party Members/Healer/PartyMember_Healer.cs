using System;
using UnityEngine;

namespace Manapotion.PartySystem
{
    public class PartyMember_Healer : PartyMember
    {
        public static event EventHandler<OnUpdateRemedyBarEventArgs> OnUpdateRemedyBar;
        public class OnUpdateRemedyBarEventArgs : EventArgs
        {
            public float remedy;
            public float maxRemedy;
        }

        protected override void Init()
        {
            ManaBehaviour.OnUpdate += Update;

            InitMember();
        }

        protected virtual void InitMember()
        {

        }

        void Update()
        {
            if (partyMemberState != PartyMemberState.CurrentLeader)
            {
                return;
            }

            // anything that updates UI should go below this.

            UpdateRemedyBar(pointsManagerScriptableObject.GetPointScriptableObject(Stats.PointID.Remedypoints).value.currentValue, pointsManagerScriptableObject.GetPointScriptableObject(Stats.PointID.Remedypoints).value.maxValue);
        }

        private void UpdateRemedyBar(float value, float maxValue)
        {
            OnUpdateRemedyBar?.Invoke(this, new OnUpdateRemedyBarEventArgs
            {
                remedy = value,
                maxRemedy = maxValue
            });
        }
        
        public void MaxRP()
        {
            pointsManagerScriptableObject.GetPointScriptableObject(Stats.PointID.Remedypoints).value.currentValue = pointsManagerScriptableObject.GetPointScriptableObject(Stats.PointID.Remedypoints).value.maxValue;
        }
    }
}
