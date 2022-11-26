using System.Collections.Generic;
using UnityEngine;

namespace Manapotion.PartySystem.LaurieCharacter
{   
    public enum State { Movement, Attack, AuxMove }
        
    public class Laurie : PartyMember_Mage
    {
        private static Laurie Instance;
        
        #region Ability State
        // Ability
        public AbilityState abilityState = AbilityState.None; // sets "ability" to None / Initializes AbilityState
        public AuxilaryMovementType auxilaryMovementType = AuxilaryMovementType.Spindash; //sets "auxilaryType" to Spindash / Initializes AuxilaryMovementType
        #endregion

        public State state = State.Movement;

        [HideInInspector]
        public Party party;
        
        // public LaurieCasting laurieCasting { get; private set; }
        // public LaurieController laurieController { get; private set; }
        // public LaurieAbilities laurieAbilities { get; private set; }
        // public LaurieRenderer laurieRenderer { get; private set; }
        
        private void Awake()
        {
            Instance = this;
            party = GetComponentInParent<Party>();
        }

        protected override void InitMember()
        {
            // laurieCasting = new LaurieCasting(this);
            // laurieAbilities = new LaurieAbilities(this);
            // laurieController = new LaurieController(this);
            // laurieRenderer = new LaurieRenderer(this);
        }
        
        private void Update()
        {
            // laurieCasting.Update(); 
            // laurieAbilities.Update(); 
            // laurieRenderer.Update();
            // laurieController.Update();
        }
    }
}
    


