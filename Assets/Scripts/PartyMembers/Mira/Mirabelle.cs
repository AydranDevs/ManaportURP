using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Manapotion.PartySystem.MirabelleCharacter
{
    public enum HealingType { Showera, Spray }
    public enum UmbrellaState {  OpeningUmbrella, UmbrellaOpened, ClosingUmbrella, UmbrellaClosed }

    public enum State { Movement, Umbrella }

    public class Mirabelle : PartyMember
    {   
        public HealingType healingType = HealingType.Showera;
        public PartyBuffs HealingEffect = PartyBuffs.Rejuvenated;
        public UmbrellaState umbrellaState = UmbrellaState.UmbrellaClosed;

        public State state = State.Movement;

        [HideInInspector]
        public HealthBarScript healthBar;

        [HideInInspector]
        public Party party;

        public MirabelleController mirabelleController { get; private set; }
        public MirabelleRenderer mirabelleRenderer { get; private set; }
        public MirabelleHealing mirabelleHealing { get; private set; }

        public InputProvider inputProvider;
        public InputActionAsset controls;

        private void Awake()
        {
            party = GetComponentInParent<Party>();
        }

        private void Start()
        {
            base.Start();
            
            mirabelleController = new MirabelleController(this);
            mirabelleRenderer = new MirabelleRenderer(this);
            mirabelleHealing = new MirabelleHealing(this);

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
            mirabelleController.Update();
            mirabelleRenderer.Update();
            
            UpdateHealthBar();
        }

        private void UpdateHealthBar()
        {
            healthBar.SetHealth(stats.hitPoints.value);
        }

        public override void SetPartyMaxDistance()
        {
            party.maxDistance = 3f;
        }
    }
}
