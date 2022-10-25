using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Manapotion.PartySystem.WinsleyCharacter
{
    public enum State { Movement, Attack, AuxMove }

    public class Winsley : PartyMember
    {
        public State state = State.Movement;

        public Party party;

        public WinsleyController winsleyController { get; private set; }
        public WinsleyRenderer winsleyRenderer { get; private set; }

        public InputProvider inputProvider;
        public InputActionAsset controls;

        private void Awake()
        {
            party = GetComponentInParent<Party>();   
        }

        protected override void Initialize()
        {   
            winsleyController = new WinsleyController(this);
            winsleyRenderer = new WinsleyRenderer(this);
        }
    
        private void Update()
        {
            winsleyController.Update();
            winsleyRenderer.Update();
        }

        public override void SetPartyMaxDistance()
        {
            party.maxDistance = 5f;
        }
    }
}
