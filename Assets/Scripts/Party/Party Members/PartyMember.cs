using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Manapotion.Items;
using Manapotion.StatusEffects;
using Manapotion.Actions;
using Manapotion.Stats;
using Manapotion.Input;
using Manapotion.Rendering;

namespace Manapotion.PartySystem
{
    public enum MovementState { Idle, Push, Walk, Sprint, Dash, Skid, AuxilaryMovement }
    public enum DirectionState { North, NorthEast, East, SouthEast, South, SouthWest, West, NorthWest }
    public enum FacingState { North, East, South, West }

    public enum AbilityState { None, AuxilaryMovement, SpellcastPrimary, SpellcastSecondary }
    public enum AuxilaryMovementType { Spindash, BlinkDash, Pounce }

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
            public float health;
            public float maxHealth;
        }
        #endregion

        public Input.CharacterInput characterInput;
        public Input.CharacterController characterController;
        public CharacterRenderer characterRenderer;
        
        public PartyFormation formation;

        public PartyMemberState partyMemberState;
        public StatusEffectParticles statusEffectParticles;

        [Header("Manager ScriptableObjects")]
        public ActionsManagerScriptableObject actionsManagerScriptableObject;
        public StatsManagerScriptableObject statsManagerScriptableObject;
        public EquipmentManagerScriptableObject equipmentManagerScriptableObject;
        public PointsManagerScriptableObject pointsManagerScriptableObject;
        
        [SerializeField]
        private List<GameObject> _statusEffectParticles;

        public List<StatusEffects.Buff> statusEffects;

        public MovementState movementState = MovementState.Idle;
        public DirectionState directionState = DirectionState.South;
        public FacingState facingState = FacingState.South;
        public float dashThreshold = 8f; 

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

            // subscribe to every stat value's modified event
            for (int i = 0; i < statsManagerScriptableObject.statArray.Length; i++)
            {
                statsManagerScriptableObject.statArray[i].OnStatModifiedEvent += OnStatModifiedEvent_RefreshPoints;
            }

            statusEffects = new List<StatusEffects.Buff>();
            _statusEffectParticles = new List<GameObject>();
            
            MaxHP();
            PartyLeaderCheck();
            
            Init();
        }

        protected virtual void Init()
        {

        }

        #region Update Ability Icons
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
        private void UpdateHealthBar(float health, float maxHealth)
        {
            OnUpdateHealthBarEvent?.Invoke(this, new OnUpdateHealthBarEventArgs
            {
                health = health,
                maxHealth = maxHealth
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

        public void MaxHP()
        {
            pointsManagerScriptableObject.GetPointScriptableObject(PointID.Hitpoints).value.currentValue = pointsManagerScriptableObject.GetPointScriptableObject(PointID.Hitpoints).value.maxValue;
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

        public void AddStatusEffect(StatusEffect effect, int power, float duration)
        {
            var stEf = new StatusEffects.Buff(effect, power, duration);

            if (StatusEffectsContains(stEf))
            {
                for (int i = 0; i < statusEffects.Count; i++)
                {
                    if (statusEffects[i].effect.buffType == stEf.effect.buffType)
                    {
                        statusEffects[i].ResetTime();
                        stEf = null;
                        return;
                    }
                }
            }
            else
            {
                stEf.active = true;
                stEf.Init(this);
                statusEffects.Add(stEf);
            }
        }

        private bool StatusEffectsContains(StatusEffects.Buff effect)
        {
            bool b = false;

            for (int i = 0; i < statusEffects.Count; i++)
            {
                if (statusEffects[i].effect.buffType == effect.effect.buffType)
                {
                    b = true;
                    return b;
                }
            }

            return b;
        }

        /// <summary>
        /// called when a stat is modified
        /// </summary>
        public void OnStatModifiedEvent_RefreshPoints(object sender, Stat.OnStatModifiedEventArgs e)
        {
            pointsManagerScriptableObject.RefreshPoints();
        }
        #endregion

        #region Actions
        /// <summary>
        /// Make the member perform their Primary or Secondary action.
        /// </summary>
        /// <param name="action">Primary (0) or Secondary (1)</param>
        public virtual void PerformMainAction(int action)
        {

        }
        #endregion

        void Update()
        {
            if (pointsManagerScriptableObject.GetPointScriptableObject(PointID.Hitpoints).value.currentValue <= 0)
            {
                // Die();
                // return;
            }
            
            // remove all statuses that arent active
            if (statusEffects.Count >= 0 && statusEffects != null)
            {
                statusEffects.RemoveAll(status => !status.active);
            }

            PartyLeaderCheck();
            if (partyMemberState != PartyMemberState.CurrentLeader)
            {
                return;
            }

            UpdateHealthBar(
                pointsManagerScriptableObject.GetPointScriptableObject(PointID.Hitpoints).value.currentValue,
                pointsManagerScriptableObject.GetPointScriptableObject(PointID.Hitpoints).value.maxValue
            );
        }

        #region Party
        private void PartyLeaderCheck()
        {
            if (partyMemberState == PartyMemberState.CurrentLeader) 
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
        public void SummonParticles(GameObject g)
        {
            var go = GameObject.Instantiate(g, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), Quaternion.identity, transform);
            _statusEffectParticles.Add(go);
        }

        public void StopParticles(GameObject g)
        {
            if (_statusEffectParticles.Contains(g)) {
                int i = _statusEffectParticles.IndexOf(g);
                Destroy(_statusEffectParticles[i]);
            }
        }
        #endregion
        
        public Vector3 GetPosition()
        {
            return transform.position;
        }
    }
}
