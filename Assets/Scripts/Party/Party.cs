using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Manapotion.PartySystem.Inventory;
using Manapotion.UI;
using Manapotion.PartySystem.Cam;
using Manapotion.Utilities;

namespace Manapotion.PartySystem
{
    public enum PartyMemberState { CurrentLeader, PreviousLeader, OldestLeader }
    public enum PartyLeader { Laurie, Mirabelle, Winsley }

    public class Party : MonoBehaviour
    {
        public static Party Instance;
        [SerializeField]
        private PartyCameraManagerScriptableObject _partyCameraManager;

        public static Action OnPartyLeaderChanged;
        [NonSerialized]
        public static UnityEvent<PartyMember> OnPartyLeaderChangedEvent;

        public bool overrideCanSwitchLeaders { get; private set; } = false;

        public BagScriptableObject bagScriptableObject;

        [SerializeField] private InputActionAsset _controls;
        private InputActionMap _inputActionMap;

        private InputAction _nextLeader;
        private InputAction _previousLeader;

        public PartyMember startingLeader;
        
        public PartyMember partyLeader;
        public PartyMember previousLeader;
        public PartyMember oldestLeader;

        public PartyMember[] members;

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
            
            partyLeader = members[0];
            previousLeader = members[1];
            oldestLeader = members[2];
            
            if (startingLeader != null)
            {
                while (partyLeader != startingLeader)
                {
                    NextPartyMember();
                }
            }
            
            StartCoroutine(_partyCameraManager.SetCameraTargets(new List<Transform>{ partyLeader.transform }));
            _partyCameraManager.SetCameraMode(CameraMode.Soft_Follow);

            foreach (var member in members)
            {
                member.characterInput.SetLeader(partyLeader.transform);
            }

            _inputActionMap = _controls.FindActionMap("Player");
            UtilitiesClass.CreateInputAction(_inputActionMap, OnNext, _nextLeader, "Next");
            UtilitiesClass.CreateInputAction(_inputActionMap, OnPrevious, _previousLeader, "Previous");
        }

        public void OnNext(InputAction.CallbackContext context)
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

            NextPartyMember();
        }

        public void NextPartyMember()
        {

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
            StartCoroutine(_partyCameraManager.SetCameraTargets(new List<Transform>{ members[index].transform }));
            PartyLeaderChanged();
        }

        public void OnPrevious(InputAction.CallbackContext context)
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
            
            PreviousPartyMember();
        }
        
        public void PreviousPartyMember()
        {
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
            StartCoroutine(_partyCameraManager.SetCameraTargets(new List<Transform>{ members[index].transform }));
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
            }
        }

        public static void SetLeaderChangeOverride(bool b)
        {
            Instance.overrideCanSwitchLeaders = b;
        }
    }
}
