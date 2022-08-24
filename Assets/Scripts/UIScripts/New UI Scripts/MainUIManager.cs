using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        public static MainUIManager Instance;

        private Party _party;

        public StatusUIManager statusUIManager { get; private set; }
    
        public CharacterUIHandle[] handles { get; private set; }
        private Dictionary<int, Action> _handleDict;

        public PartyMember currentLeader { get; private set; }

        public AbilityIconSprites abilityIconSprites;

        public GameObject abilityIconParent;
        public GameObject statusBarParent;

        public GameObject abilityIconPrefab;
        public GameObject healthBarPrefab;
        public GameObject manaBarPrefab;

        private void Awake() {
            Instance = this;
        }

        private void Start()
        {
            _party = Party.Instance;

            statusUIManager = new StatusUIManager(this);

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

