using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Manapotion.PartySystem.Inventory;
using Manapotion.UI;
using Manapotion.Utilities;

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

        [SerializeField] private InputActionAsset _controls;
        private InputActionMap _inputActionMap;

        private InputAction _nextLeader;
        private InputAction _previousLeader;
        
        public PartyMember partyLeader;
        public PartyMember previousLeader;
        public PartyMember oldestLeader;

        public PartyMember[] members;

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
            partyLeader = members[0];
            previousLeader = members[1];
            oldestLeader = members[2];

            foreach (var member in members)
            {
                member.characterInput.SetLeader(partyLeader.transform);
            }

            _inputActionMap = _controls.FindActionMap("Player");
            UtilitiesClass.CreateInputAction(_inputActionMap, NextPartyMember, _nextLeader, "Next");
            UtilitiesClass.CreateInputAction(_inputActionMap, PreviousPartyMember, _previousLeader, "Previous");
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

            int index = Array.IndexOf(members, partyLeader);

            index++;
            if (index > 2)
            {
                index = 0;
            }
            else if (index < 0)
            {
                index = 2;
            }

            previousLeader = partyLeader;
            partyLeader = members[index];
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
            
            int index = Array.IndexOf(members, partyLeader);

            index--;
            if (index > 2)
            {
                index = 0;
            }
            else if (index < 0)
            {
                index = 2;
            }

            previousLeader = partyLeader;
            partyLeader = members[index];
            cam.target = members[index].transform;
            cam.PartyLeaderChanged();
            PartyLeaderChanged();
        }

        private void PartyLeaderChanged()
        {
            // make every member but the leader an AI controlled member
            foreach (var member in members)
            {
                if (member == partyLeader)
                { 
                    member.characterInput.controlType = Input.CharacterInput.ControlType.Player;
                }
                else if (member == previousLeader)
                {
                    member.characterInput.controlType = Input.CharacterInput.ControlType.AI;
                    member.characterInput.SetLeader(partyLeader.transform);
                }
                else
                {
                    member.characterInput.controlType = Input.CharacterInput.ControlType.AI;
                    member.characterInput.SetLeader(partyLeader.transform);
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
            return partyLeader;
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
            return Array.IndexOf(Instance.members, pm);
        }

        private void Update()
        {
            foreach (var member in members)
            {
                if (member != partyLeader && member != previousLeader)
                {
                    oldestLeader = member;
                }

                if (member == partyLeader)
                {
                    if (member.characterTargeting != null)
                    {
                        if (member.characterTargeting.isTargeting)
                        {
                            overrideCanSwitchLeaders = false;
                        }
                        else
                        {
                            overrideCanSwitchLeaders = true;
                        }
                    }
                }
            }
        }

        public static void SetLeaderChangeOverride(bool b)
        {
            Instance.overrideCanSwitchLeaders = b;
        }
    }
}
