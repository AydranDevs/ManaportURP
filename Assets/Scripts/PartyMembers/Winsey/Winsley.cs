using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Manapotion.PartySystem.WinsleyCharacter
{
    public enum State { Movement, Attack, AuxMove }

    public class Winsley : PartyMember
    {
        public State state = State.Movement;

        public HealthBarScript healthBar;
        public ManaBarScript manaBar;

        public Party party;

        public WinsleyController winsleyController { get; private set; }
        public WinsleyRenderer winsleyRenderer { get; private set; }

        public InputProvider inputProvider;
        public InputActionAsset controls;

        private void Awake()
        {
            party = GetComponentInParent<Party>();   
        }

        private void Start()
        {
            base.Start();
            
            winsleyController = new WinsleyController(this);
            winsleyRenderer = new WinsleyRenderer(this);

            healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBarScript>();
            
            healthBar.SetMaxHealth(stats.hitPoints.maxValue);
        }
    
        private void Update()
        {
            if (stats.hitPoints.Empty())
            {
                Die();
                return;
            }

            winsleyController.Update();
            winsleyRenderer.Update();
            
            UpdateHealthBar();
        }

        private void UpdateHealthBar()
        {
            healthBar.SetHealth(stats.hitPoints.value);
        }

        public override void SetPartyMaxDistance()
        {
            party.maxDistance = 5f;
        }
    }
}
