using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Manapotion.PartySystem.WinsleyCharacter
{
    public enum State { Movement, Attack, AuxMove }

    public class Winsley : PartyMember_Fighter
    {
        public State state = State.Movement;

        public Party party;

        public WinsleyController winsleyController { get; private set; }
        public WinsleyRenderer winsleyRenderer { get; private set; }

        private void Awake()
        {
            party = GetComponentInParent<Party>();   
        }

        protected override void InitMember()
        {   
            // winsleyController = new WinsleyController(this);
            winsleyRenderer = new WinsleyRenderer(this);
        }
    
        private void Update()
        {
            // winsleyController.Update();
            winsleyRenderer.Update();
        }
    }
}
