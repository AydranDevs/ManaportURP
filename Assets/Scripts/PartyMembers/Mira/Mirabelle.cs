using System.Collections.Generic;
using UnityEngine;

namespace PartyNamespace {
    namespace MirabelleNamespace {
        
        public enum MovementState { Idle, Push, Walk, Sprint, Dash, Skid, AuxilaryMovement }
        public enum DirectionState { North, NorthEast, East, SouthEast, South, SouthWest, West, NorthWest }
        public enum FacingState { North, East, South, West }
        public enum AbilityState { None, AuxilaryMovement, Healing, Buffing }
        public enum AuxilaryMovementType { Spindash, BlinkDash, Pounce }

        public enum HealingEffect { Showera, Spray }
        public enum HealingType { Rejuvenating, Warming, Comforting, Caring, Loving }
        public enum BuffEffect { Showera, Spray }
        public enum BuffType { Strengthening, Healing, Swiftening, Defending, PyroWarming, CryoChilling, ToxiSickening, VoltAmplifying }

        public enum State { Movement, Umbrella }
        public enum UmbrellaState {  OpeningUmbrella, UmbrellaOpened, ClosingUmbrella, UmbrellaClosed }

        public class Mirabelle : PartyMember {

            #region Attributes

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
            
            #endregion

            // Movement
            public MovementState movementState = MovementState.Idle; 
            public DirectionState directionState = DirectionState.South; 
            public FacingState facingState = FacingState.South; 
            public float sprintModifier = 1.75f; 
            public float skidThreshold = 8f; 

            // Abilities
            public AbilityState abilityState = AbilityState.None;
            
            public HealingEffect healingEffect = HealingEffect.Showera;
            public HealingType healingType = HealingType.Rejuvenating;

            public BuffEffect buffEffect = BuffEffect.Showera;
            public BuffType buffType = BuffType.Strengthening; 

            public State state = State.Movement;
            public UmbrellaState umbrellaState = UmbrellaState.UmbrellaClosed;

            public HealthBarScript healthBar;
            public ManaBarScript manaBar;

            public Party party;

            public MirabelleController controller;
            public MirabelleRenderer miraRenderer;
            public MirabelleHealing healing;
            //public LaurieAbilities abilities;

            private void Awake() {
                base.Start();
                party = GetComponentInParent<Party>();

                controller = GetComponentInChildren<MirabelleController>();
                miraRenderer = GetComponentInChildren<MirabelleRenderer>();
                healing = GetComponentInChildren<MirabelleHealing>();
                //abilities = GetComponentInChildren<LaurieAbilities>();
            }

            private void Start() {
                healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBarScript>();
        
                pushSp = defaultPushSp;
                walkSp = defaultwalkSp;
                sprintMod = defaultSprintMod;
                dashMod = defaultDashMod;

                sprintSp = defaultwalkSp * sprintMod;
                dashSp = defaultwalkSp * dashMod;
                
                healthBar.SetMaxHealth(hitPointsMax);

                PartyLeaderCheck();
            }

            // public void MaxHP() {
            //     hitPoints.Max();
            // }

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
                if (hitPoints.value <= 0f) {
                    Die();
                    return;
                }

                PartyLeaderCheck();
                
                UpdateHealthBar();
            }

            public void Damage(float damage) {
                hitPoints.value = hitPoints.value - damage;
            }

            private void UpdateHealthBar() {
                healthBar.SetHealth(hitPoints.value);
            }

            public void Die() {
                Destroy(gameObject);
            }

            private void PartyLeaderCheck() {
                if (party.partyLeader == PartyLeader.Mirabelle) {
                    gameObject.tag = "PlayerPartyLeader";
                    party.maxDistance = 3f;
                }else {
                    gameObject.tag = "PlayerPartyMember";
                }
            }
            
            public Vector3 GetPosition() {
                return transform.position;
            }

            public void Debug_SetMaxHP(float hp) {
                hitPointsMax = hp;
                healthBar.SetMaxHealth(hitPointsMax);
            }

            public void Debug_MaxHP() {
                hitPoints.value = hitPointsMax;
            }

        }
    }
}
