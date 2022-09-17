using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Manapotion.PartySystem;
using Manapotion.Status;

namespace Manapotion.UI
{
    [Serializable]
    public class AbilityIconSprites
    {
        // spells
        public Sprite ArcaneBurston;
        public Sprite ArcaneBlasteur;
        public Sprite ArcaneAutoma;
        public Sprite PyroBurston;
        public Sprite PyroBlasteur;
        public Sprite PyroAutoma;
        public Sprite CryoBurston;
        public Sprite CryoBlasteur;
        public Sprite CryoAutoma;
        public Sprite ToxiBurston;
        public Sprite ToxiBlasteur;
        public Sprite ToxiAutoma;
        public Sprite VoltBurston;
        public Sprite VoltBlasteur;
        public Sprite VoltAutoma;

        // heals
        public Sprite RejuvenatingShower;
        public Sprite ComfortingShower;
        public Sprite CaringShower;
        public Sprite LovingShower;

        // buffs
        public Sprite EmpoweringShower;
        public Sprite SociableShower;
        public Sprite SwifteningShower;
        public Sprite TougheningShower;
        public Sprite PyroUP;
        public Sprite CryoUP;
        public Sprite ToxiUP;
        public Sprite VoltUP;
    }

    public class MainUIManager : MonoBehaviour
    {
        public static MainUIManager Instance { get; private set; }
        public bool invOpen = false;

        private Party _party;

        public StatusUIManager statusUIManager { get; private set; }
        public InventoryUIManager inventoryUIManager { get; private set; }
    
        public CharacterUIHandle[] handles { get; private set; }
        private Dictionary<int, Action> _handleDict;

        public PartyMember currentLeader { get; private set; }

        [Header("Status")]
        public AbilityIconSprites abilityIconSprites;

        public GameObject statusParent;
        public GameObject abilityIconParent;
        public GameObject statusBarParent;

        public GameObject abilityIconPrefab;
        public GameObject healthBarPrefab;
        public GameObject manaBarPrefab;

        [Header("Input")]
        [SerializeField]
        private InputActionAsset _controls;
        private InputActionMap _inputActionMap;

        private InputAction _toggleBag;
        private InputAction _toggleEquip;
        private InputAction _toggleBeastiary;

        public DimmerHandler dimmer;

        [Header("Inventory")]
        public GameObject bagUIObject;
        public GameObject leftEquipUIObject;
        public GameObject rightEquipUIObject;
        public GameObject beastiaryUIObject;

        public ItemIcon[] itemIcons; //= new ItemIcon[(int)ItemID.MAXCOUNT];

        public Image emptyImage;

        public GameObject consumableSlot;
        public GameObject ingredientSlot;
        public GameObject materialSlot;

        public GameObject weaponSlot;
        public GameObject armourSlot;
        public GameObject vanitySlot;

        public GameObject bagSlotParent;
        public GameObject equipmentSlotParent;

        private void Awake()
        {
            Instance = this;

            statusUIManager = new StatusUIManager(this);
            inventoryUIManager = new InventoryUIManager(this);

            _inputActionMap = _controls.FindActionMap("Player");

            CreateInputAction(OnToggleInv, _toggleBag, "ToggleBag");
            CreateInputAction(OnToggleInv, _toggleEquip, "ToggleEquip");
            CreateInputAction(OnToggleInv, _toggleBeastiary, "ToggleBeastiary");
        }

        private void CreateInputAction(Action<InputAction.CallbackContext> subscriber, InputAction action, string actionName)
        {
            action = _inputActionMap.FindAction(actionName);
            action.Enable();
            action.started += subscriber;
            action.performed += subscriber;
            action.canceled += subscriber;
        }

        private void Start()
        {
            GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged_GameStateChanged;
            _party = Party.Instance;

            CreateInputAction(inventoryUIManager.OnToggleBag, _toggleBag, "ToggleBag");
            CreateInputAction(inventoryUIManager.OnToggleEquip, _toggleEquip, "ToggleEquip");
            CreateInputAction(inventoryUIManager.OnToggleBeastiary, _toggleBeastiary, "ToggleBeastiary");

            handles = new CharacterUIHandle[_party.members.Count()];
            for (int i = 0; i < _party.members.Count(); i++)
            {
                handles[i] = new CharacterUIHandle(i);
            }
            _handleDict = new Dictionary<int, Action>();

            _handleDict.Add(0, HandleLaurieUI);
            _handleDict.Add(1, HandleMirabelleUI);
            _handleDict.Add(2, HandleWinsleyUI);

            currentLeader = Party.GetCurrentLeader();
        }

        private void Update()
        {
            Action action = _handleDict[Party.GetPartyMemberIndex(Party.GetCurrentLeader())];
            if (action != null)
            {
                action();
            }
        }

        #region Inv
        public void OnToggleInv(InputAction.CallbackContext context)
        {   
            if (!context.started)
            {
                return;
            }

            invOpen = true;
            
            inventoryUIManager.equipUI.Refresh();
            inventoryUIManager.bagUI.Refresh();
            inventoryUIManager.beastiaryUI.Refresh();

            GameStateManager.Instance.ChangeGameState(GameState.Inv);
        }

        public void OnGameStateChanged_GameStateChanged(object sender, GameStateManager.OnGameStateChangedArgs e)
        {
            if (e.newState == GameState.Inv)
            {
                dimmer.FadeIn();
                statusUIManager.Hide();
                inventoryUIManager.bagUI.Refresh();
            }
            else
            {
                dimmer.FadeOut();
                statusUIManager.Show();
            }
        }
        #endregion

        #region Handle Char UI
        private void HandleLaurieUI()
        {
            if (Party.GetCurrentLeader().gameObject != currentLeader.gameObject)
            {
                currentLeader = Party.GetCurrentLeader();
            }
        }

        private void HandleMirabelleUI()
        {
            if (Party.GetCurrentLeader().gameObject != currentLeader.gameObject)
            {
                currentLeader = Party.GetCurrentLeader();
            }
        }

        private void HandleWinsleyUI()
        {
            if (Party.GetCurrentLeader().gameObject != currentLeader.gameObject)
            {
                currentLeader = Party.GetCurrentLeader();
            }
        }
        #endregion
    }
    
    public class CharacterUIHandle
    {
        public PartyMember member { get; private set; }
        public int memberId { get; private set; }

        public CharacterUIHandle(int id)
        {
            memberId = id;
            member = Party.Instance.members[id].GetComponent<PartyMember>();
        }

        public Stat GetStat(PartyStats stat)
        {
            return member.stats.FindStat(stat);
        }
    }
}

