using System.Collections.Generic;
using UnityEngine;

namespace PartyNamespace {
    namespace MirabelleNamespace {
        
        public enum MovementState { Idle, Push, Walk, Sprint, Dash, Skid, AuxilaryMovement }
        public enum DirectionState { North, NorthEast, East, SouthEast, South, SouthWest, West, NorthWest }
        public enum FacingState { North, East, South, West }
        public enum AbilityState { None, AuxilaryMovement, SpellcastPrimary, SpellcastSecondary }
        public enum AuxilaryMovementType { Spindash, BlinkDash, Pounce }

        public enum PrimarySpellType { Automa, Blasteur, Burston }
        public enum PrimarySpellElement { Arcane, Pyro, Cryo, Toxi, Volt }
        public enum SecondarySpellType { Automa, Blasteur, Burston }
        public enum SecondarySpellElement { Arcane, Pyro, Cryo, Toxi, Volt }

        public enum State { Movement, Attack, AuxMove }

        public class Mirabelle : PartyMember {

            #region Attributes
                
            // Player experience point values
            [Header("Experience Points")]
            public float xpLevel;
            public float xpMax;
            public float xp;
            
            // Player health point values
            [Header("Hit Points")]
            public const float HIT_POINTS_MAX_DEFAULT = 20f;
            public float hitPointsMax = 20f;
            public float hitPoints;

            // Player stability point values
            [Header("Stability Points")]
            public float stabilityMax = 1f;
            public float stability = 1f; // (100%)
            public float stressMax = 1f;
            public float stress = 0f;

            // Movement
            [Header("Movement")]
            public float pushSp;
            private float defaultPushSp = 1.5f;

            public float dashSp;
            public float sprintSp;
            public float walkSp;
            private float defaultwalkSp = 2f;
            
            public float sprintMod;
            private float defaultSprintMod = 1.35f;

            public float dashMod;
            private float defaultDashMod = 1.5f;

            // Abilities
            // [Header("Abilities")]
            // public float abilityCooldownLimit;

            // public float spindashDist;
            // public float blinkdashDist;
            // public float pounceDist;

            // Defenses
            [Header("Defenses")]
            // percents
            public float stressRes = 0.50f;
            public float pyroRes = 0.00f;
            public float cryoRes = 0.00f;
            public float boltRes = 0.00f;
            public float toxiRes = 0.00f;
            public float arcaneRes = 0.00f;

            // Debuffs
            [Header("Debuffs")]
            public bool isOnFire = false;
            public bool isFreezing = false;
            public bool isZapped = false;
            public bool isPoisoned = false;

            // Buffs
            [Header("Buffs")]
            public bool isPyroBoosted = false; // Provides a bit of defense against pyro - increases damage dealt by pyro spells
            public bool isCryoBoosted = false; // Provides a bit of defense against cryo - increases damage dealt by cryo spells
            public bool isBoltBoosted = false; // Provides a bit of defense against bolt - increases damage dealt by bolt spells
            public bool isToxiBoosted = false; // Provides a bit of defense against toxi - increases damage dealt by toxi spells

            [Tooltip("increases the player's movement speed and sprint modifier by a set amount")]
            public bool isSpeedBoosted = false;
            [Tooltip("Increases crit chance and crit damage of all spells by a set amount")]
            public bool isCritBoosted = false;
            [Tooltip("Regenerates a fixed amount of health over a fixed amount of time")]
            public bool isRegenerating = false;
            [Tooltip("Increases overall damage and makes the player nearly 'invinicible', all damage taken is added together and instead taken over time. Similar to Payday 2's Stoic. also causes some visual things you'll see later.")]
            public bool isLashingOut = false;
            [Tooltip("Makes the player invulnerable to ALL types of damage for a short amount of time")]
            public bool isInvincible = false;
            [Tooltip("Converts all damage taken to shield for a short amount of time")]
            public bool isAbsorbing = false;
            
            #endregion

            // Movement
            public MovementState movementState = MovementState.Idle; 
            public DirectionState directionState = DirectionState.South; 
            public FacingState facingState = FacingState.South; 
            public float sprintModifier = 1.75f; 
            public float skidThreshold = 8f; 

            public State state = State.Movement;

            public HealthBarScript healthBar;
            public ManaBarScript manaBar;

            public Party party;

            public MirabelleController controller;
            //public LaurieCasting casting;
            //public LaurieAbilities abilities;

            private void Awake() {
                party = GetComponentInParent<Party>();

                controller = GetComponentInChildren<MirabelleController>();
                //casting = GetComponentInChildren<LaurieCasting>();
                //abilities = GetComponentInChildren<LaurieAbilities>();
            }

            private void Start() {
                healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBarScript>();

                hitPointsMax = HIT_POINTS_MAX_DEFAULT;
                
                MaxHP();
        
                pushSp = defaultPushSp;
                walkSp = defaultwalkSp;
                sprintMod = defaultSprintMod;
                dashMod = defaultDashMod;

                sprintSp = defaultwalkSp * sprintMod;
                dashSp = defaultwalkSp * dashMod;
                
                healthBar.SetMaxHealth(hitPointsMax);
            }

            public void MaxHP() {
                hitPoints = hitPointsMax;
            }

            public void AddXP(string type, float amount) {
                if (type == "points") {
                    xp += amount;
                }else if (type == "levels") {
                    xpLevel += amount;
                }else {
                    Debug.Log("Error! incorrect xp type given. (Mirabelle.cs)");
                    return;
                }
            }

            public void LevelUp() {
                xp = 0f;
                xpLevel++;
                xpMax = xpMax * 2;

                hitPointsMax += 2f;
            }
        
            private void Update() {
                if (hitPoints <= 0f) {
                    Die();
                    return;
                }

                UpdateHealthBar();
            }

            public void Damage(float damage) {
                hitPoints = hitPoints - damage;
            }

            private void UpdateHealthBar() {
                healthBar.SetHealth(hitPoints);
            }

            public void Die() {
                Destroy(gameObject);
            }
            
            public Vector3 GetPosition() {
                return transform.position;
            }

            public void Debug_SetMaxHP(float hp) {
                hitPointsMax = hp;
                healthBar.SetMaxHealth(hitPointsMax);
            }

            public void Debug_MaxHP() {
                hitPoints = hitPointsMax;
            }

        }
    }
}
