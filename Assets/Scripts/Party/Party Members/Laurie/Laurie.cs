using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Manapotion.PartySystem.LaurieCharacter
{   
    public enum AbilityState { None, AuxilaryMovement, SpellcastPrimary, SpellcastSecondary }
    public enum AuxilaryMovementType { Spindash, BlinkDash, Pounce }
    
    public enum PrimarySpellType { Automa, Blasteur, Burston }
    public enum PrimarySpellElement { Arcane, Pyro, Cryo, Toxi, Volt }
    public enum SecondarySpellType { Automa, Blasteur, Burston }
    public enum SecondarySpellElement { Arcane, Pyro, Cryo, Toxi, Volt }
    
    public enum State { Movement, Attack, AuxMove }
        
    public class Laurie : PartyMember_Mage
    {
        private static Laurie Instance;
        
        #region Ability State
        // Ability
        public AbilityState abilityState = AbilityState.None; // sets "ability" to None / Initializes AbilityState
        public AuxilaryMovementType auxilaryMovementType = AuxilaryMovementType.Spindash; //sets "auxilaryType" to Spindash / Initializes AuxilaryMovementType
        #endregion
        
        #region Spellcasting
        // Spellcasting
        public PrimarySpellType primarySpellType = PrimarySpellType.Blasteur;
        public PrimarySpellElement primarySpellElement = PrimarySpellElement.Arcane;
        public SecondarySpellType secondarySpellType = SecondarySpellType.Blasteur;
        public SecondarySpellElement secondarySpellElement = SecondarySpellElement.Arcane;
        #endregion

        public State state = State.Movement;

        [HideInInspector]
        public Party party;
        
        public LaurieController laurieController { get; private set; }
        public LaurieCasting laurieCasting { get; private set; }
        public LaurieAbilities laurieAbilities { get; private set; }
        public LaurieRenderer laurieRenderer { get; private set; }

        public InputProvider inputProvider;
        public InputActionAsset controls;
        
        private void Awake()
        {
            Instance = this;
            party = GetComponentInParent<Party>();
        }

        private void Start()
        {
            base.Init();

            laurieCasting = new LaurieCasting(this);
            laurieAbilities = new LaurieAbilities(this);
            laurieController = new LaurieController(this);
            laurieRenderer = new LaurieRenderer(this);

            // MaxMP();

            // formation = new PartyFormation(5);
            // formation.formationPositions.Add(Party.Instance.previousLeader.GetComponent<PartyMember>(), new Vector2Int(-1, 1));
            // formation.formationPositions.Add(Party.Instance.oldestLeader.GetComponent<PartyMember>(), new Vector2Int(1, 1));
        }

        // public void MaxMP()
        // {
        //     stats.manaPoints.Max();
        // }
        
        // public void RegenMP()
        // {
        //     manaRegenTimer = manaRegenTimer - Time.deltaTime;
        //     if (manaRegenTimer <= 0f)
        //     {
        //         stats.manaPoints.value += stats.manaPointsRegenAmount.value;
        //         manaRegenTimer = MANA_REGEN_TIMER_DEFAULT;
        //     }
        // }
        
        private void CoolDownManaRegen()
        {
            manaRegenCooldown = manaRegenCooldown - Time.deltaTime;
            if (manaRegenCooldown <= 0f)
            {
                manaRegenCoolingDown = false;
            }
        }
        
        private void Update()
        {
            // if (stats.hitPoints.Empty())
            // {
            //     Die();
            //     return;
            // }

            laurieCasting.Update(); 
            laurieAbilities.Update(); 
            laurieRenderer.Update();
            laurieController.Update();
            
            // if (manaRegenCoolingDown)
            // {
            //     CoolDownManaRegen();
            // }

            // if (!manaRegenCoolingDown && stats.manaPoints.value < stats.manaPoints.maxValue)
            // {
            //     RegenMP();
            // }
        }

        public override void SetPartyMaxDistance()
        {
            party.maxDistance = 4f;
        }

        public static string GetPrimarySpellInfo()
        {
            string spell = Instance.primarySpellElement.ToString() + " " + Instance.primarySpellType.ToString();
            return spell;
        }

        public static string GetSecondarySpellInfo()
        {
            string spell = Instance.secondarySpellElement.ToString() + " " + Instance.secondarySpellType.ToString();
            return spell;
        }
    }
}
    


