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

    public enum PartyStats 
    {
        Health,
        Mana,
        XP,
        AttackDamage,
        AttackSpeed,
        PyroResistence,
        CryoResistence,
        ToxiResistence,
        VoltResistence,
        ArcaneResistence,
        StressResistence,
        WalkSpeed,
        SprintMod,
        DashMod,
        PushSpeed,
        Stability,
        ManaRegen,
        SpindashDistance,
        BlinkdashDistance,
        PounceDistance,
        AbilityCooldown
    }
    
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

    public abstract class PartyMember : MonoBehaviour
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
        public static event EventHandler<OnUpdateManaBarEventArgs> OnUpdateManaBar;
        public class OnUpdateManaBarEventArgs : EventArgs
        {
            public float mana;
            public float maxMana;
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

        public EquipableData weapon;
        public EquipableData armour;
        public EquipableData vanity;

        #region Stats
        [System.Serializable]
        public class Stats
        {
            public Stat[] statArray;

            public int xpLevel;
            public const float EXP_MAX_DEFAULT = 100f;
            public Stat experiencePoints;

            public const float HIT_POINTS_MAX_DEFAULT = 20f;
            public Stat hitPoints;

            public const float ATTACK_DAMAGE_DEFAULT = 2f;
            public Stat attackDamage;

            public const float ATTACK_SPEED_DEFAULT = 1f;
            public Stat attackSpeed;

            public const float STABILITY_POINTS_MAX = 1.00f; // 100%
            public Stat stabilityPoints;
            
            public const float MANA_POINTS_MAX_DEFAULT = 5f;
            public Stat manaPoints;

            public const float MANA_REGEN_DEFAULT = 0.5f;
            public Stat manaPointsRegen;
            
            private const float PUSH_SP_DEFAULT = 1.5f;
            public Stat pushSp;

            private const float DASH_MOD_DEFAULT = 1.5f;
            public Stat dashMod;

            private const float SPRINT_MOD_DEFAULT = 1.35f;
            public Stat sprintMod;

            private const float WALK_SP_DEFAULT = 2f;
            public Stat walkSp;

            private const float ABILITY_COOLDOWN_DEFAULT = 2f;
            public Stat abilityCooldownLimit;

            private static readonly float[] ABILITY_DISTANCE_DEFAULT = new float[3] { 5f, 3f, 4f };
            public Stat[] abilityDistances = new Stat[]
            {
                new Stat(0f, ABILITY_DISTANCE_DEFAULT[0], PartyStats.SpindashDistance),
                new Stat(0f, ABILITY_DISTANCE_DEFAULT[1], PartyStats.BlinkdashDistance),
                new Stat(0f, ABILITY_DISTANCE_DEFAULT[2], PartyStats.PounceDistance)
            };
            
            public Stat[] resistances = new Stat[6]
            {
                new Stat(0.00f, 1.00f, PartyStats.StressResistence),
                new Stat(0.00f, 1.00f, PartyStats.PyroResistence),
                new Stat(0.00f, 1.00f, PartyStats.CryoResistence),
                new Stat(0.00f, 1.00f, PartyStats.ToxiResistence),
                new Stat(0.00f, 1.00f, PartyStats.VoltResistence),
                new Stat(0.00f, 1.00f, PartyStats.ArcaneResistence)
            };
            
            public Stats()
            {
                experiencePoints = new Stat(0f, EXP_MAX_DEFAULT, PartyStats.XP);
                
                hitPoints = new Stat(HIT_POINTS_MAX_DEFAULT, HIT_POINTS_MAX_DEFAULT, PartyStats.Health);

                attackDamage = new Stat(ATTACK_DAMAGE_DEFAULT, 999f, PartyStats.AttackDamage);
                attackSpeed = new Stat(ATTACK_SPEED_DEFAULT, 999f, PartyStats.AttackSpeed);
                
                stabilityPoints = new Stat(0.00f, STABILITY_POINTS_MAX, PartyStats.Stability);

                manaPoints = new Stat(MANA_POINTS_MAX_DEFAULT, MANA_POINTS_MAX_DEFAULT, PartyStats.Mana);
                manaPointsRegen = new Stat(MANA_REGEN_DEFAULT, 999f, PartyStats.ManaRegen);

                pushSp = new Stat(PUSH_SP_DEFAULT, 999f, PartyStats.PushSpeed);
                dashMod = new Stat(DASH_MOD_DEFAULT, 999f, PartyStats.DashMod);
                sprintMod = new Stat(SPRINT_MOD_DEFAULT, 999f, PartyStats.SprintMod);
                walkSp = new Stat(WALK_SP_DEFAULT, 999f, PartyStats.WalkSpeed);

                abilityCooldownLimit = new Stat(ABILITY_COOLDOWN_DEFAULT, 999f, PartyStats.AbilityCooldown);

                statArray = new Stat[]
                {
                    experiencePoints,
                    hitPoints,
                    attackDamage,
                    attackSpeed,
                    stabilityPoints,
                    manaPoints,
                    manaPointsRegen,
                    pushSp,
                    dashMod,
                    sprintMod,
                    walkSp,
                    abilityCooldownLimit,
                    abilityDistances[0],
                    abilityDistances[1],
                    abilityDistances[2],
                    resistances[0],
                    resistances[1],
                    resistances[2],
                    resistances[3],
                    resistances[4],
                    resistances[5]
                };
            }

            public Stat FindStat(PartyStats stat)
            {
                foreach (var s in statArray)
                {
                    if (s.id == stat)
                    {
                        return s;
                    }
                }
                
                return null;
            }
        }
        public Stats stats = new Stats();
        #endregion

        public void Start()
        {
            ManaBehaviour.OnUpdate += Update;
            statusEffects = new List<Buff>();
            _statusEffectParticles = new List<GameObject>();

            MaxHP();

            PartyLeaderCheck();
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

        private void UpdateManaBar(float mana, float maxMana)
        {
            OnUpdateManaBar?.Invoke(this, new OnUpdateManaBarEventArgs
            {
                mana = mana,
                maxMana = maxMana
            });
        }
        #endregion

        #region Status
        public void Damage(float damage)
        {
            stats.hitPoints.value -= damage;
        }

        public void Die()
        {
            Destroy(gameObject);
        }

        public void MaxHP()
        {
            stats.hitPoints.Max();
        }

        public void AddXP(string type, float amount)
        {
            if (type == "points")
            {
                stats.experiencePoints.value += amount;
            }
            else if (type == "levels")
            {
                stats.xpLevel += (int)amount;
            }
            else 
            {
                Debug.Log("Error! incorrect xp type given.");
                return;
            }
        }

        public void LevelUp()
        {
            stats.experiencePoints.value = 0f;
            stats.xpLevel++;
            stats.experiencePoints.SetMaxValue(stats.experiencePoints.maxValue * 2);

            stats.hitPoints.SetMaxValue(stats.hitPoints.maxValue + 2f);
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

        #region Equip
        public void Equip(int i, EquipableData equipable)
        {
            if (i == 0)
            {
                weapon = equipable;
                weapon.OnEquip();
            }
            else if (i == 1)
            {
                armour = equipable;
                armour.OnEquip();
            }
            else if (i == 2)
            {
                vanity = equipable;
                vanity.OnEquip();
            }
            else
            {
                throw new Exception("No Equipable found with id " + i);
            }
        }

        public void Unequip(int i)
        {
            if (i == 0)
            {
                weapon.OnUnequip();
                weapon = null;
            }
            else if (i == 1)
            {
                armour.OnUnequip();
                armour = null;
            }
            else if (i == 2)
            {
                vanity.OnUnequip();
                vanity = null;
            }
            else
            {
                throw new Exception("No Equipable found with id " + i);
            }
        }
        #endregion

        void Update()
        {
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

            UpdateHealthBar(stats.hitPoints.value, stats.hitPoints.maxValue);
            UpdateManaBar(stats.manaPoints.value, stats.manaPoints.maxValue);
        }

        #region Party
        public abstract void SetPartyMaxDistance();

        private void PartyLeaderCheck()
        {
            if (partyMemberState == PartyMemberState.CurrentLeader) 
            {
                gameObject.tag = "PlayerPartyLeader";
                SetPartyMaxDistance();
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
