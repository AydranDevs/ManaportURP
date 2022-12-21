using System;
using UnityEngine;
using Manapotion.Items;
using Manapotion.Actions;
using Manapotion.Actions.Targets;
using Manapotion.Stats;
using Manapotion.Input;
using Manapotion.Rendering;
using System.Collections.Generic;

namespace Manapotion.PartySystem
{
    public enum PrimaryActionElement { Arcane, Pyro, Cryo, Toxi, Volt }
    public enum SecondaryActionElement { Arcane, Pyro, Cryo, Toxi, Volt }
    public enum DamageType { Physical, Magical }
    
    public enum PartyBuffs
    {
        Rejuvenated, // stat boosts
        Comfortable,
        Cared_For,
        Loved,
        
        Empowered, // increases max values of related stats
        Sociable,
        Nimble,
        Toughened,
        
        Pyro_UP, // boosts elemental damage and resistance
        Cryo_UP,
        Toxi_UP,
        Volt_UP
    }
    
    [System.Serializable]
    public struct StatusEffectParticles
    {
        public GameObject rejuvenatedBuffParticles;
    }

    public class PartyMember : MonoBehaviour
    {
        #region Events
        public static event EventHandler<OnAbilityChangedEventArgs> OnAbilityChangedEvent;
        public class OnAbilityChangedEventArgs : EventArgs
        {
            public int index;
            public Sprite sprite;
        }
        public static event EventHandler<OnCoolingDownEventArgs> OnCoolingDownEvent;
        public class OnCoolingDownEventArgs : EventArgs
        {
            public int index;
            public float cooldownTime;
            public float cooldown;
        }
        public static event EventHandler<OnAbilityLockChangedEventArgs> OnAbilityLockChangedEvent;
        public class OnAbilityLockChangedEventArgs : EventArgs
        {
            public int index;
            public bool isLocked;
        }

        public static event EventHandler<OnUpdateHealthBarEventArgs> OnUpdateHealthBarEvent;
        public class OnUpdateHealthBarEventArgs : EventArgs
        {
            public float currentValue;
            public float maxValue;
        }
        public static event EventHandler<OnUpdateActionBarEventArgs> OnUpdateActionBarEvent;
        public class OnUpdateActionBarEventArgs : EventArgs
        {
            public PointID actionPoints;

            public float currentValue;
            public float maxValue;
        }
        #endregion

        public CharacterInput characterInput;
        public Input.CharacterController characterController;
        public CharacterRenderer characterRenderer;
        public CharacterTargeting characterTargeting;

        // public StatusEffectParticles statusEffectParticles;

        [Header("Manager ScriptableObjects")]
        public ActionsManagerScriptableObject actionsManagerScriptableObject;
        public StatsManagerScriptableObject statsManagerScriptableObject;
        public EquipmentManagerScriptableObject equipmentManagerScriptableObject;
        public PointsManagerScriptableObject pointsManagerScriptableObject;

        [Header("Action")]
        [Tooltip("The type of points that the character uses when they perform an attack or action.")]
        public PointID actionPoints;

        // cooldown before mana starts regenerating
        [SerializeField]
        private float _actionPointsRegenCooldown;
        // true if currently waiting to regen
        public bool actionPointsRegenCoolingDown = false;
        public bool actionPointsRegenerating = false;

        // internal regen cooldown timer
        private float _actionPointsRegenCooldownTimer;
        // internal regen timer
        private float _actionPointsRegenTimer;

        public PrimaryActionElement primaryActionElement = PrimaryActionElement.Arcane;
        public SecondaryActionElement secondaryActionElement = SecondaryActionElement.Arcane;
        public DamageType damageType;

        // [field: SerializeField]
        // public PartyMemberStats stats { get; protected set; }
        
        private void Start()
        {
            // Update isnt called here so we use ManaBehaviour
            ManaBehaviour.OnUpdate += Update;

            characterInput.Init(this);
            characterController.Init(this);
            characterRenderer.Init(this);
            characterTargeting.Init(this);

            // subscribe to every stat value's modified event
            for (int i = 0; i < statsManagerScriptableObject.statArray.Length; i++)
            {
                statsManagerScriptableObject.statArray[i].OnStatModifiedEvent += OnStatModifiedEvent_RefreshPoints;
            }
            for (int i = 0; i < actionsManagerScriptableObject.possibleActions.Count; i++)
            {
                actionsManagerScriptableObject.possibleActions[i].OnActionPerformedEvent += OnActionPerformedEvent_UseActionPoints;
            }
            
            // max out all status points
            pointsManagerScriptableObject.GetPointScriptableObject(PointID.Hitpoints).value.currentValue = int.MaxValue;
            pointsManagerScriptableObject.GetPointScriptableObject(actionPoints).value.currentValue = int.MaxValue;
            PartyLeaderCheck();
        }

        #region Update Action Icons
        public void UpdateAbilityIcons(int i, Sprite sp)
        {
            OnAbilityChangedEvent?.Invoke(this, new OnAbilityChangedEventArgs
            {
                index = i,
                sprite = sp
            });
        }

        public void UpdateAbilityIconCooldown(int i, float cooldownTime, float cooldown)
        {
            OnCoolingDownEvent?.Invoke(this, new OnCoolingDownEventArgs
            {
                index = i,
                
                cooldownTime = cooldownTime,
                cooldown = cooldown
            });
        }

        public void UpdateAbilityIconLock(int i, bool locked)
        {
            OnAbilityLockChangedEvent?.Invoke(this, new OnAbilityLockChangedEventArgs
            {
                index = i,
                isLocked = locked
            });
        }
        #endregion
        
        #region Update Status Bars
        private void UpdateHealthBar(float currentValue, float maxValue)
        {
            OnUpdateHealthBarEvent?.Invoke(this, new OnUpdateHealthBarEventArgs
            {
                currentValue = currentValue,
                maxValue = maxValue
            });
        }

        private void UpdateActionBar(float currentValue, float maxValue)
        {
            OnUpdateActionBarEvent?.Invoke(this, new OnUpdateActionBarEventArgs
            {
                actionPoints = actionPoints,
                currentValue = currentValue,
                maxValue = maxValue
            });
        }
        #endregion

        #region Status
        public void Damage(float damage)
        {
            // stats.manaport_stat_hitpoints.SetValue(stats.manaport_stat_hitpoints.GetValue() - damage);
        }

        public void Die()
        {
            Destroy(gameObject);
        }

        public void AddXP(string type, int amount)
        {
            if (type == "points")
            {
                pointsManagerScriptableObject.GetPointScriptableObject(PointID.Experiencepoints).value.currentValue += amount;
                // stats.manaport_stat_experience_points.SetValue(stats.manaport_stat_experience_points.GetValue() + amount);
            }
            else if (type == "levels")
            {
                // uhhh put code here later
                // stats.manaport_stat_experience_level.SetValue(stats.manaport_stat_experience_level.GetValue() + (int)amount);
            }
            else 
            {
                Debug.Log("Error! incorrect xp type given.");
                return;
            }
        }

        public void LevelUp()
        {
            // stats.manaport_stat_experience_points.SetValue(0f);
            // stats.manaport_stat_experience_level.SetValue(stats.manaport_stat_experience_level.GetValue() + 1f);
            // stats.manaport_stat_experience_points.SetMaxValue(stats.manaport_stat_experience_level.GetMaxValue() * 2);

            // stats.manaport_stat_max_hitpoints.SetValue(stats.manaport_stat_max_hitpoints.GetValue() + 2f);
            // stats.manaport_stat_hitpoints.SetMaxValue(stats.manaport_stat_max_hitpoints.GetValue());
        }

        /// <summary>
        /// called when a stat is modified
        /// </summary>
        public void OnStatModifiedEvent_RefreshPoints(object sender, Stat.OnStatModifiedEventArgs e)
        {
            pointsManagerScriptableObject.RefreshPoints();
        }
        
        public void UseActionPoints(int amount)
        {
            if (!pointsManagerScriptableObject.GetPointScriptableObject(actionPoints).value.CanSubtract(amount))
            {
                // not enough action points... Maybe play a sound later
                return;
            }

            pointsManagerScriptableObject.GetPointScriptableObject(actionPoints).value.currentValue -= amount;
            actionPointsRegenCoolingDown = true;
            _actionPointsRegenCooldownTimer = _actionPointsRegenCooldown;
        }

        public float ActionPointsAfterUse(int amount)
        {
            var pt = pointsManagerScriptableObject.GetPointScriptableObject(actionPoints);
            return pt.value.currentValue - amount;
        }
        #endregion

        #region Actions
        /// <summary>
        /// Make the member perform their Primary or Secondary action.
        /// </summary>
        /// <param name="action">Primary (0) or Secondary (1)</param>
        public virtual void PerformMainAction(int action)
        {
            if (action == 0)
            {
                actionsManagerScriptableObject.PerformAction(
                    equipmentManagerScriptableObject.weapon.itemScriptableObject.attacksManagerScriptableObject.attacksArray[0].action_id,
                    this,
                    new DamageInstance
                    {
                    damageInstanceType = (DamageInstance.DamageInstanceType)damageType,
                    damageInstanceElement = (DamageInstance.DamageInstanceElement)primaryActionElement,
                    damageInstanceAmount = (float)this.statsManagerScriptableObject.GetStat(
                        equipmentManagerScriptableObject.weapon.itemScriptableObject.attacksManagerScriptableObject.attacksArray[0].modifierStatID
                        ).value.modifiedValue
                    });
            }
            else
            {
                actionsManagerScriptableObject.PerformAction(
                    equipmentManagerScriptableObject.weapon.itemScriptableObject.attacksManagerScriptableObject.attacksArray[1].action_id,
                    this,
                    new DamageInstance
                    {
                    damageInstanceType = (DamageInstance.DamageInstanceType)damageType,
                    damageInstanceElement = (DamageInstance.DamageInstanceElement)secondaryActionElement,
                    damageInstanceAmount = (float)this.statsManagerScriptableObject.GetStat(
                        equipmentManagerScriptableObject.weapon.itemScriptableObject.attacksManagerScriptableObject.attacksArray[1].modifierStatID
                        ).value.modifiedValue
                    });
            }
        }

        public void OnActionPerformedEvent_UseActionPoints(object sender, ActionScriptableObject.OnActionPerformedEventArgs e)
        {
            if (e.costPointID == actionPoints && e.cost > 0)
            {
                UseActionPoints(e.cost);
            }
        }
        #endregion

        void Update()
        {
            if (pointsManagerScriptableObject.GetPointScriptableObject(PointID.Hitpoints).value.currentValue <= 0)
            {
                // Die();
                // return;
            }

            // if currently waiting to regenerate, count down from _actionPointsRegenCooldown
            if (actionPointsRegenCoolingDown)
            {
                Debug.Log("cooling down regen ");
                _actionPointsRegenCooldownTimer -= Time.deltaTime;
                if (_actionPointsRegenCooldownTimer <= 0f)
                {
                    actionPointsRegenCoolingDown = false;
                    _actionPointsRegenCooldownTimer = _actionPointsRegenCooldown;
                }
            }
            // else, if actionPoints isn't at it's max value, regenerate
            else if (pointsManagerScriptableObject.GetPointScriptableObject(actionPoints).value.currentValue < pointsManagerScriptableObject.GetPointScriptableObject(actionPoints).value.maxValue)
            {
                _actionPointsRegenTimer -= Time.deltaTime;
                if (_actionPointsRegenTimer <= 0f)
                {
                    var pt = pointsManagerScriptableObject.GetPointScriptableObject(actionPoints);
                    pt.SetValue(pt.value.currentValue + 1);
                    _actionPointsRegenTimer = 2f;
                }
            }
            
            // // remove all statuses that arent active
            // if (statusEffects.Count >= 0 && statusEffects != null)
            // {
            //     statusEffects.RemoveAll(status => !status.active);
            // }

            PartyLeaderCheck();
            if (Party.Instance.partyLeader != this)
            {
                return;
            }

            UpdateHealthBar(
                pointsManagerScriptableObject.GetPointScriptableObject(PointID.Hitpoints).value.currentValue,
                pointsManagerScriptableObject.GetPointScriptableObject(PointID.Hitpoints).value.maxValue
            );

            UpdateActionBar(
                pointsManagerScriptableObject.GetPointScriptableObject(actionPoints).value.currentValue,
                pointsManagerScriptableObject.GetPointScriptableObject(actionPoints).value.maxValue
            );
        }

        #region Party
        private void PartyLeaderCheck()
        {
            if (Party.Instance.partyLeader == this) 
            {
                gameObject.tag = "PlayerPartyLeader";
            }
            else
            {
                gameObject.tag = "PlayerPartyMember";
            }
        }
        #endregion

        #region Particles
        // public void SummonParticles(GameObject g)
        // {
        //     var go = GameObject.Instantiate(g, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), Quaternion.identity, transform);
        //     _statusEffectParticles.Add(go);
        // }

        // public void StopParticles(GameObject g)
        // {
        //     if (_statusEffectParticles.Contains(g)) {
        //         int i = _statusEffectParticles.IndexOf(g);
        //         Destroy(_statusEffectParticles[i]);
        //     }
        // }
        #endregion
        
        public Vector3 GetPosition()
        {
            return transform.position;
        }

        #if UNITY_EDITOR
        private void OnDrawGizmos() {
            if (gameObject.tag != "PlayerPartyLeader")
            {
                return;
            }

            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, characterTargeting.GetCurrentTargetPosition());
        }
        #endif
    }
}
