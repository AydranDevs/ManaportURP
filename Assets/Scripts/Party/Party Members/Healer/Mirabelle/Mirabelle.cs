using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Manapotion.PartySystem.MirabelleCharacter
{
    public enum HealingType { Shower, Spray }
    public enum UmbrellaState {  OpeningUmbrella, UmbrellaOpened, ClosingUmbrella, UmbrellaClosed }

    public enum State { Movement, Umbrella }

    public class Mirabelle : PartyMember_Healer
    {   
        public HealingType healingType = HealingType.Shower;
        public PartyBuffs healingEffect = PartyBuffs.Rejuvenated;
        public UmbrellaState umbrellaState = UmbrellaState.UmbrellaClosed;

        public State state = State.Movement;

        [HideInInspector]
        public Party party;

        // public MirabelleController mirabelleController { get; private set; }
        // public MirabelleRenderer mirabelleRenderer { get; private set; }
        // public MirabelleHealing mirabelleHealing { get; private set; }
        
        private void Awake()
        {
            party = GetComponentInParent<Party>();
        }

        protected override void InitMember()
        {         
            // mirabelleController = new MirabelleController(this);
            // mirabelleRenderer = new MirabelleRenderer(this);
            // mirabelleHealing = new MirabelleHealing(this);
        }
    
        private void Update()
        {
            // mirabelleController.Update();
            // mirabelleRenderer.Update();
            // mirabelleHealing.Update();
        }
    }
}
