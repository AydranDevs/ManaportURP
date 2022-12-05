using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Manapotion.PartySystem;
using Manapotion.Utilities;

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

        public UI_Equipment uI_Equipment;
        public UI_Bag uI_Bag;
        public UI_Beastiary uI_Beastiary;
    
        public CharacterUIHandle[] handles { get; private set; }
        // private Dictionary<int, Action> _handleDict;

        public PartyMember currentLeader { get; private set; }

        [Header("Status")]
        public AbilityIconSprites abilityIconSprites;

        public GameObject statusParent;
        public GameObject abilityIconParent;
        public GameObject statusBarParent;

        public GameObject abilityIconPrefab;
        public GameObject healthBarPrefab;
        public GameObject manaBarPrefab;
        public GameObject staminaBarPrefab;
        public GameObject remedyBarPrefab;

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

            UtilitiesClass.CreateInputAction(_inputActionMap, OnToggleInv, _toggleBag, "ToggleBag");
            UtilitiesClass.CreateInputAction(_inputActionMap, OnToggleInv, _toggleEquip, "ToggleEquip");
            UtilitiesClass.CreateInputAction(_inputActionMap, OnToggleInv, _toggleBeastiary, "ToggleBeastiary");
        }

        private void Start()
        {
            GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged_GameStateChanged;
            _party = Party.Instance;

            UtilitiesClass.CreateInputAction(_inputActionMap, inventoryUIManager.OnToggleBag, _toggleBag, "ToggleBag");
            UtilitiesClass.CreateInputAction(_inputActionMap, inventoryUIManager.OnToggleEquip, _toggleEquip, "ToggleEquip");
            UtilitiesClass.CreateInputAction(_inputActionMap, inventoryUIManager.OnToggleBeastiary, _toggleBeastiary, "ToggleBeastiary");

            handles = new CharacterUIHandle[_party.members.Count()];
            for (int i = 0; i < _party.members.Count(); i++)
            {
                handles[i] = new CharacterUIHandle(i);
            }
            // _handleDict = new Dictionary<int, Action>();

            // _handleDict.Add(0, HandleLaurieUI);
            // _handleDict.Add(1, HandleMirabelleUI);
            // _handleDict.Add(2, HandleWinsleyUI);

            currentLeader = Party.GetCurrentLeader();
        }

        private void Update()
        {
            // Action action = _handleDict[Party.GetPartyMemberIndex(Party.GetCurrentLeader())];
            // if (action != null)
            // {
            //     action();
            // }
        }

        #region Inv
        public void OnToggleInv(InputAction.CallbackContext context)
        {   
            if (!context.started)
            {
                return;
            }

            invOpen = true;

            GameStateManager.Instance.ChangeGameState(GameState.Inv);
        }

        public void OnGameStateChanged_GameStateChanged(object sender, GameStateManager.OnGameStateChangedArgs e)
        {
            if (e.newState == GameState.Inv)
            {
                statusUIManager.Hide();
                // inventoryUIManager.bagUI.Refresh();
            }
            else
            {
                statusUIManager.Show();
            }
        }
        #endregion

        // #region Handle Char UI
        // private void HandleLaurieUI()
        // {
        //     if (Party.GetCurrentLeader().gameObject != currentLeader.gameObject)
        //     {
        //         currentLeader = Party.GetCurrentLeader();
        //         statusUIManager.SetManaBar();
        //     }
        // }

        // private void HandleMirabelleUI()
        // {
        //     if (Party.GetCurrentLeader().gameObject != currentLeader.gameObject)
        //     {
        //         currentLeader = Party.GetCurrentLeader();
        //         statusUIManager.SetRemedyBar();
        //     }
        // }

        // private void HandleWinsleyUI()
        // {
        //     if (Party.GetCurrentLeader().gameObject != currentLeader.gameObject)
        //     {
        //         currentLeader = Party.GetCurrentLeader();
        //         statusUIManager.SetStaminaBar();
        //     }
        // }
        // #endregion
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
    }
}

