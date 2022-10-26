using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manapotion;
using Manapotion.StatusEffects;
using Manapotion.Status;
using Manapotion.Equipables;

namespace Manapotion.PartySystem
{
    public enum MovementState { Idle, Push, Walk, Sprint, Dash, Skid, AuxilaryMovement }
    public enum DirectionState { North, NorthEast, East, SouthEast, South, SouthWest, West, NorthWest }
    public enum FacingState { North, East, South, West }

    public enum AbilityState { None, AuxilaryMovement, SpellcastPrimary, SpellcastSecondary }
    public enum AuxilaryMovementType { Spindash, BlinkDash, Pounce }
    
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
        public static event EventHandler<OnAbilityChangedEventArgs> OnAbilityChanged;
        public class OnAbilityChangedEventArgs : EventArgs
        {
            public int index;
            public Sprite sprite;
        }
        public static event EventHandler<OnCoolingDownEventArgs> OnCoolingDown;
        public class OnCoolingDownEventArgs : EventArgs
        {
            public int index;
            public float cooldownTime;
            public float cooldown;
        }
        public static event EventHandler<OnAbilityLockChangedEventArgs> OnAbilityLockChanged;
        public class OnAbilityLockChangedEventArgs : EventArgs
        {
            public int index;
            public bool isLocked;
        }

        public static event EventHandler<OnUpdateHealthBarEventArgs> OnUpdateHealthBar;
        public class OnUpdateHealthBarEventArgs : EventArgs
        {
            public float health;
            public float maxHealth;
        }
        #endregion

        public PartyFormation formation;

        public PartyMemberState partyMemberState;
        public StatusEffectParticles statusEffectParticles;
        
        [SerializeField]
        private List<GameObject> _statusEffectParticles;

        public List<Buff> statusEffects;

        public MovementState movementState = MovementState.Idle;
        public DirectionState directionState = DirectionState.South;
        public FacingState facingState = FacingState.South;
        public float dashThreshold = 8f; 

        [field: SerializeField]
        public EquipmentScriptableObject equipmentScriptableObject { get; private set; }

        [field: SerializeField]
        public PartyMemberStats stats { get; protected set; }
        
        public void Start()
        {
            ManaBehaviour.OnUpdate += Update;

            stats.manaport_stat_hitpoints.SetMaxValue(stats.manaport_stat_max_hitpoints.GetValue());
            stats.manaport_stat_manapoints.SetMaxValue(stats.manaport_stat_max_manapoints.GetValue());
            stats.manaport_stat_experience_points.SetMaxValue(stats.manaport_stat_max_experience_points.GetValue());

            statusEffects = new List<Buff>();
            _statusEffectParticles = new List<GameObject>();

            MaxHP();

            PartyLeaderCheck();
            Initialize();
        }

        /// <summary>
        /// Runs after Start() runs in grandparent class PartyMember 
        /// </summary>
        protected virtual void Initialize()
        {

        }

        #region Update Ability Icons
        public void UpdateAbilityIcons(int i, Sprite sp)
        {
            OnAbilityChanged?.Invoke(this, new OnAbilityChangedEventArgs
            {
                index = i,
                sprite = sp
            });
        }

        public void UpdateAbilityIconCooldown(int i, float cooldownTime, float cooldown)
        {
            OnCoolingDown?.Invoke(this, new OnCoolingDownEventArgs
            {
                index = i,
                
                cooldownTime = cooldownTime,
                cooldown = cooldown
            });
        }

        public void UpdateAbilityIconLock(int i, bool locked)
        {
            OnAbilityLockChanged?.Invoke(this, new OnAbilityLockChangedEventArgs
            {
                index = i,
                isLocked = locked
            });
        }
        #endregion
        
        #region Update Status Bars
        private void UpdateHealthBar(float health, float maxHealth)
        {
            OnUpdateHealthBar?.Invoke(this, new OnUpdateHealthBarEventArgs
            {
                health = health,
                maxHealth = maxHealth
            });
        }
        #endregion

        #region Status
        public void Damage(float damage)
        {
            stats.manaport_stat_hitpoints.SetValue(stats.manaport_stat_hitpoints.GetValue() - damage);
        }

        public void Die()
        {
            Destroy(gameObject);
        }

        public void MaxHP()
        {
            stats.manaport_stat_hitpoints.Max();
        }

        public void AddXP(string type, float amount)
        {
            if (type == "points")
            {
                stats.manaport_stat_experience_points.SetValue(stats.manaport_stat_experience_points.GetValue() + amount);
            }
            else if (type == "levels")
            {
                stats.manaport_stat_experience_level.SetValue(stats.manaport_stat_experience_level.GetValue() + (int)amount);
            }
            else 
            {
                Debug.Log("Error! incorrect xp type given.");
                return;
            }
        }

        public void LevelUp()
        {
            stats.manaport_stat_experience_points.SetValue(0f);
            stats.manaport_stat_experience_level.SetValue(stats.manaport_stat_experience_level.GetValue() + 1f);
            stats.manaport_stat_experience_points.SetMaxValue(stats.manaport_stat_experience_level.GetMaxValue() * 2);

            stats.manaport_stat_max_hitpoints.SetValue(stats.manaport_stat_max_hitpoints.GetValue() + 2f);
            stats.manaport_stat_hitpoints.SetMaxValue(stats.manaport_stat_max_hitpoints.GetValue());
        }

        public void AddStatusEffect(StatusEffect effect, int power, float duration)
        {
            var stEf = new Buff(effect, power, duration);

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

        private bool StatusEffectsContains(Buff effect)
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
        #endregion

        void Update()
        {
            if (stats.manaport_stat_hitpoints.Empty())
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

            Debug.Log("updating health basr lool letsscogo " + gameObject.name);
            UpdateHealthBar(stats.manaport_stat_hitpoints.GetValue(), stats.manaport_stat_max_hitpoints.GetValue());
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
