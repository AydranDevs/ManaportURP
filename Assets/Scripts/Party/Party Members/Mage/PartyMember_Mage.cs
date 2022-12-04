using System;
using UnityEngine;
using Manapotion.Stats;
using Manapotion.Actions;

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
            for (int i = 0; i < actionsManagerScriptableObject.possibleActions.Count; i++)
            {
                actionsManagerScriptableObject.possibleActions[i].OnActionPerformedEvent += OnActionPerformedEvent_ActionPerformed;
            }

            _manaRegenTimer = 2f;
            MaxMP();

            InitMember();
        }

        protected virtual void InitMember()
        {

        }

        public void OnActionPerformedEvent_ActionPerformed(object sender, ActionScriptableObject.OnActionPerformedEventArgs e)
        {
            if (e.costPointID == PointID.Manapoints && e.cost > 0)
            {
                UseMana(e.cost);
            }
        }

        void Update()
        {
            if (manaRegenCoolingDown)
            {
                CoolDownManaRegen();
            }
            if (!manaRegenCoolingDown && pointsManagerScriptableObject.GetPointScriptableObject(PointID.Manapoints).value.currentValue < pointsManagerScriptableObject.GetPointScriptableObject(PointID.Manapoints).value.maxValue)
            {
                RegenMP();
            }

            if (Party.Instance.partyLeader != this)
            {
                return;
            }

            // anything that updates UI should go below this.

            UpdateManaBar(pointsManagerScriptableObject.GetPointScriptableObject(PointID.Manapoints).value.currentValue, pointsManagerScriptableObject.GetPointScriptableObject(PointID.Manapoints).value.maxValue);
        }

        #region Mana Point Management
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
            var pt = pointsManagerScriptableObject.GetPointScriptableObject(PointID.Manapoints);
            pt.value.currentValue = pt.value.maxValue;
        }
        
        public void RegenMP()
        {
            _manaRegenTimer = _manaRegenTimer - Time.deltaTime;
            if (_manaRegenTimer <= 0f)
            {
                var pt = pointsManagerScriptableObject.GetPointScriptableObject(PointID.Manapoints);
                pt.SetValue(pt.value.currentValue + 1);
                _manaRegenTimer = 2f;
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

        public void UseMana(int amount)
        {
            if (!pointsManagerScriptableObject.GetPointScriptableObject(PointID.Manapoints).value.CanSubtract(amount))
            {
                // not enough mana
                return;
            }

            pointsManagerScriptableObject.GetPointScriptableObject(PointID.Manapoints).value.currentValue -= amount;
            manaRegenCoolingDown = true;
            manaRegenCooldown = MANA_REGEN_COOLDOWN_DEFUALT;
        }
        
        public float ManaPointsAfterUse(int amount)
        {
            var pt = pointsManagerScriptableObject.GetPointScriptableObject(PointID.Manapoints);
            return pt.value.currentValue - amount;
        }
        #endregion

        #region Spellcasting
        public override void PerformMainAction(int action)
        {
            if (action == 0)
            {
                actionsManagerScriptableObject.PerformAction(
                    equipmentManagerScriptableObject.weapon.itemScriptableObject.attacksManagerScriptableObject.attacksArray[0].action_id,
                    this,
                    (Actions.DamageInstance.DamageInstanceType)damageType,
                    (Actions.DamageInstance.DamageInstanceElement)primaryActionElement
                );
            }
            else
            {
                actionsManagerScriptableObject.PerformAction(
                    equipmentManagerScriptableObject.weapon.itemScriptableObject.attacksManagerScriptableObject.attacksArray[1].action_id ,
                    this,
                    (Actions.DamageInstance.DamageInstanceType)damageType,
                    (Actions.DamageInstance.DamageInstanceElement)secondaryActionElement
                );
            }
        }
        #endregion
    }
}
