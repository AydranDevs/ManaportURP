using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Manapotion.PartySystem.Inventory;
using Manapotion.UI;

namespace Manapotion.PartySystem
{
    public enum PartyMemberState { CurrentLeader, PreviousLeader, OldestLeader }
    public enum PartyLeader { Laurie, Mirabelle, Winsley }

    public class Party : MonoBehaviour
    {
        public static Party Instance;

        public static Action OnPartyLeaderChanged;
        [NonSerialized]
        public static UnityEvent<PartyMember> OnPartyLeaderChangedEvent;

        public bool overrideCanSwitchLeaders { get; private set; } = false;

        public BagScriptableObject bagScriptableObject;

        // public EquipmentManagerScriptableObject laurieEquipmentScriptableObject;
        // public EquipmentManagerScriptableObject mirabelleEquipmentScriptableObject;
        // public EquipmentManagerScriptableObject winsleyEquipmentScriptableObject;

        [SerializeField] private InputActionAsset _controls;
        private InputActionMap _inputActionMap;

        private InputAction _nextLeader;
        private InputAction _previousLeader;
        
        public PartyLeader partyLeader;
        public GameObject previousLeader;
        public GameObject oldestLeader;

        public GameObject[] members;

        private PartyCam cam;

        public float maxDistance;

        public PartyInventory partyInventory { get; private set; }

        private void Awake()
        {
            Instance = this;
            if (OnPartyLeaderChangedEvent == null)
            {
                OnPartyLeaderChangedEvent = new UnityEvent<PartyMember>();
            }
        }

        private void Start()
        {
            partyInventory = new PartyInventory(this);
            // bagScriptableObject.bagItemEquippedEvent.AddListener(partyInventory.EquipItemToMember);
            
            cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PartyCam>();
            cam.target = members[0].transform;
            partyLeader = PartyLeader.Laurie;
            previousLeader = members[1];
            oldestLeader = members[2];

            _inputActionMap = _controls.FindActionMap("Player");
            CreateInputAction(NextPartyMember, _nextLeader, "Next");
            CreateInputAction(PreviousPartyMember, _previousLeader, "Previous");
        }

        private void CreateInputAction(Action<InputAction.CallbackContext> subscriber, InputAction action, string actionName)
        {
            action = _inputActionMap.FindAction(actionName);
            action.Enable();
            action.started += subscriber;
            action.performed += subscriber;
            action.canceled += subscriber;
        }

        public void NextPartyMember(InputAction.CallbackContext context)
        {
            if (!context.started)
            {
                return;
            }
            if (GameStateManager.Instance.state != GameState.Main && !overrideCanSwitchLeaders)
            {
                return;
            }
            if (ContextMenuHandler.Instance.contextMenuOpen)
            {
                return;
            }

            int index = (int)partyLeader;

            index++;
            if (index > 2)
            {
                index = 0;
            }
            else if (index < 0)
            {
                index = 2;
            }

            previousLeader = members[(int)partyLeader];
            partyLeader = (PartyLeader)index;
            cam.target = members[index].transform;
            cam.PartyLeaderChanged();
            PartyLeaderChanged();
        }

        public void PreviousPartyMember(InputAction.CallbackContext context)
        {
            if (!context.started)
            {
                return;
            }
            if (GameStateManager.Instance.state != GameState.Main && !overrideCanSwitchLeaders)
            {
                return;
            }
            if (ContextMenuHandler.Instance.contextMenuOpen)
            {
                return;
            }
            
            int index = (int)partyLeader;

            index--;
            if (index > 2)
            {
                index = 0;
            }
            else if (index < 0)
            {
                index = 2;
            }

            previousLeader = members[(int)partyLeader];
            partyLeader = (PartyLeader)index;
            cam.target = members[index].transform;
            cam.PartyLeaderChanged();
            PartyLeaderChanged();
        }

        private void PartyLeaderChanged()
        {
            foreach (var member in members)
            {
                PartyMember partyMember = member.GetComponent<PartyMember>();
                if (member == members[(int)partyLeader])
                {
                    partyMember.partyMemberState = PartyMemberState.CurrentLeader; 
                }
                else if (member == previousLeader)
                {
                    partyMember.partyMemberState = PartyMemberState.PreviousLeader; 
                }
                else
                { 
                    partyMember.partyMemberState = PartyMemberState.OldestLeader;
                }
            }

            if (OnPartyLeaderChanged != null)
            {
                OnPartyLeaderChanged();
            }
            if (OnPartyLeaderChangedEvent != null)
            {
                OnPartyLeaderChangedEvent.Invoke(GetCurrentLeader());
            }
        }

        private PartyMember StaticReturner_GetCurrentLeader()
        {
            foreach (var member in members)
            {
                var pm = member.GetComponent<PartyMember>();
                if (pm.partyMemberState == PartyMemberState.CurrentLeader) 
                {
                    return pm;
                }
            }

            return members[0].GetComponent<PartyMember>();
        }

        private PartyMember StaticReturner_GetMember(int i)
        {
            return members[i].GetComponent<PartyMember>();
        }

        public static PartyMember GetCurrentLeader()
        {
            return Instance.StaticReturner_GetCurrentLeader();
        }

        public static PartyMember GetMember(int i)
        {
            return Instance.StaticReturner_GetMember(i);
        }

        public static int GetPartyMemberIndex(PartyMember pm)
        {
            return Array.IndexOf(Instance.members, pm.gameObject);
        }

        private void Update()
        {
            foreach (GameObject member in members)
            {
                if (member != members[(int)partyLeader] && member != previousLeader)
                {
                    oldestLeader = member;
                }
            }
        }

        public static void SetLeaderChangeOverride(bool b)
        {
            Instance.overrideCanSwitchLeaders = b;
        }
    }
}
