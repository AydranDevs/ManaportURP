using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PartyNamespace {
    namespace LaurieNamespace {
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

        public class Laurie : PartyMember {

            private static Laurie Instance;

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

            // Player mana point values
            [Header("Mana Points")]
            public const float  MANA_POINTS_MAX_DEFAULT = 5f;
            public float manaPointsMax = 5f;
            public float manaPoints;
            
            public float manaPointsRegen;
            public const float MANA_REGEN_DEFAULT = 0.5f;

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
            [Header("Abilities")]
            public float abilityCooldownLimit;

            public float spindashDist;
            public float blinkdashDist;
            public float pounceDist;

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
            public MovementState movementState = MovementState.Idle; // sets "movementType" to None / Initializes MovementState
            public DirectionState directionState = DirectionState.South; // sets "direction" to South / Initializes DirectionState
            public FacingState facingState = FacingState.South; // sets "facing" to South / Initializes FacingState
            public float sprintModifier = 1.75f; // Used in playerMovement, multiplies speed by value set here
            public float skidThreshold = 8f; // Used in playerMovement, how long the player must be sprinting for to make them skid to a stop and turn around upon stopping or pressing the opposite direction.

            // Ability
            public AbilityState abilityState = AbilityState.None; // sets "ability" to None / Initializes AbilityState
            public AuxilaryMovementType auxilaryMovementType = AuxilaryMovementType.Spindash; //sets "auxilaryType" to Spindash / Initializes AuxilaryMovementType

            // Spellcasting
            public PrimarySpellType primarySpellType = PrimarySpellType.Blasteur;
            public PrimarySpellElement primarySpellElement = PrimarySpellElement.Arcane;
            
            public SecondarySpellType secondarySpellType = SecondarySpellType.Blasteur;
            public SecondarySpellElement secondarySpellElement = SecondarySpellElement.Arcane;

            public State state = State.Movement;

            public HealthBarScript healthBar;
            public ManaBarScript manaBar;

            public Party party;
            public GameStateManager gameManager;

            public LaurieController controller;
            public LaurieCasting casting;
            public LaurieAbilities abilities;

            public float manaRegenCooldown;
            public const float MANA_REGEN_COOLDOWN_DEFUALT = 1f;
            public bool manaRegenCoolingDown = false;

            private float manaRegenTimer;
            public const float MANA_REGEN_TIMER_DEFAULT = 1f;

            private void Awake() {
                Instance = this;

                party = GetComponentInParent<Party>();

                controller = GetComponentInChildren<LaurieController>();
                casting = GetComponentInChildren<LaurieCasting>();
                abilities = GetComponentInChildren<LaurieAbilities>();
            }

            private void Start() {
                healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBarScript>();
                manaBar = GameObject.FindGameObjectWithTag("ManaBar").GetComponent<ManaBarScript>();

                hitPointsMax = HIT_POINTS_MAX_DEFAULT;
                manaPointsMax = MANA_POINTS_MAX_DEFAULT;
                
                MaxHP();
                MaxMP();

                abilityCooldownLimit = 2f;

                spindashDist = 5f;
                blinkdashDist = 3f;
        
                pushSp = defaultPushSp;
                walkSp = defaultwalkSp;
                sprintMod = defaultSprintMod;
                dashMod = defaultDashMod;

                sprintSp = defaultwalkSp * sprintMod;
                dashSp = defaultwalkSp * dashMod;

                manaRegenTimer = MANA_REGEN_TIMER_DEFAULT;
                manaPointsRegen = MANA_REGEN_DEFAULT;
                
                healthBar.SetMaxHealth(hitPointsMax);
                manaBar.SetMaxMana(manaPointsMax);

                PartyLeaderCheck();
            }

            public void MaxHP() {
                hitPoints = hitPointsMax;
            }

            public void MaxMP() {
                manaPoints = manaPointsMax;
            }

            public void RegenMP() {
                manaRegenTimer = manaRegenTimer - Time.deltaTime;

                if (manaRegenTimer <= 0f) {
                    manaPoints = manaPoints + manaPointsRegen;
                    manaRegenTimer = MANA_REGEN_TIMER_DEFAULT;
                }
            }

            private void CoolDownManaRegen() {
                manaRegenCooldown = manaRegenCooldown - Time.deltaTime;
                if (manaRegenCooldown <= 0f) {
                    manaRegenCoolingDown = false;
                }
            }

            public void AddXP(string type, float amount) {
                if (type == "points") {
                    xp += amount;
                }else if (type == "levels") {
                    xpLevel += amount;
                }else {
                    Debug.Log("Error! incorrect xp type given. (Laurie.cs)");
                    return;
                }
            }

            public void LevelUp() {
                xp = 0f;
                xpLevel++;
                xpMax = xpMax * 2;

                hitPointsMax += 2f;
                manaPointsMax += 2f;
            }

            private void Update() {
                if (hitPoints <= 0f) {
                    Die();
                    return;
                }

                PartyLeaderCheck();

                UpdateHealthBar();
                UpdateManaBar();

                if (manaRegenCoolingDown) {
                    CoolDownManaRegen();
                }

                if (!manaRegenCoolingDown && manaPoints < manaPointsMax) {
                    RegenMP();
                }
            }

            public void Damage(float damage) {
                hitPoints = hitPoints - damage;
            }

            private void UpdateHealthBar() {
                healthBar.SetHealth(hitPoints);
            }

            public void UseMana(float amount) {
                manaPoints = manaPoints - amount;

                manaRegenCoolingDown = true;
                manaRegenCooldown = MANA_REGEN_COOLDOWN_DEFUALT;
            }

            public float ManaPointsAfterUse(float amount) {
            return manaPoints - amount;
            }

            private void UpdateManaBar() {
                manaBar.SetMana(manaPoints);
            }

            public void Debug_SetMaxHP(float hp) {
                hitPointsMax = hp;
                healthBar.SetMaxHealth(hitPointsMax);
            }

            public void Debug_MaxHP() {
                hitPoints = hitPointsMax;
            }

            public void Debug_SetMaxMP(float mp) {
                manaPointsMax = mp;
                manaBar.SetMaxMana(manaPointsMax);
            }

            public void Debug_MaxMP() {
                manaPoints = manaPointsMax;
            }

            public void Debug_SetPrimarySpell(int spell) {
                if (spell == 0) {
                    primarySpellType = PrimarySpellType.Burston;
                }else if (spell == 1) {
                    primarySpellType = PrimarySpellType.Blasteur;
                }else if (spell == 2) {
                    primarySpellType = PrimarySpellType.Automa;
                }
            }

            public void Debug_SetSecondarySpell(int spell) {
                if (spell == 0) {
                    secondarySpellType = SecondarySpellType.Burston;
                }else if (spell == 1) {
                    secondarySpellType = SecondarySpellType.Blasteur;
                }else if (spell == 2) {
                    secondarySpellType = SecondarySpellType.Automa;
                }
            }

            public void Debug_SetPrimaryElement(int element) {
                if (element == 0) {
                    primarySpellElement = PrimarySpellElement.Arcane;
                }else if (element == 1) {
                    primarySpellElement = PrimarySpellElement.Pyro;
                }else if (element == 2) {
                    primarySpellElement = PrimarySpellElement.Cryo;
                }else if (element == 3) {
                    primarySpellElement = PrimarySpellElement.Toxi;
                }else if (element == 4) {
                    primarySpellElement = PrimarySpellElement.Volt;
                }
            }

            public void Debug_SetSecondaryElement(int element) {
                if (element == 0) {
                    secondarySpellElement = SecondarySpellElement.Arcane;
                }else if (element == 1) {
                    secondarySpellElement = SecondarySpellElement.Pyro;
                }else if (element == 2) {
                    secondarySpellElement = SecondarySpellElement.Cryo;
                }else if (element == 3) {
                    secondarySpellElement = SecondarySpellElement.Toxi;
                }else if (element == 4) {
                    secondarySpellElement = SecondarySpellElement.Volt;
                }
            }

            public void Die() {
                Destroy(gameObject);
            }

            private void PartyLeaderCheck() {
                if (party.partyLeader == PartyLeader.Laurie) {
                    gameObject.tag = "PlayerPartyLeader";
                    party.maxDistance = 5f;
                }else {
                    gameObject.tag = "PlayerPartyMember";
                }
            }
            
            public Vector3 GetPosition() {
                return transform.position;
            }

            public static string GetPrimarySpellInfo() {
                string spell = Instance.primarySpellElement.ToString() + " " + Instance.primarySpellType.ToString();
                return spell;
            }

            public static string GetSecondarySpellInfo() {
                string spell = Instance.secondarySpellElement.ToString() + " " + Instance.secondarySpellType.ToString();
                return spell;
            }
        }
    }
}
