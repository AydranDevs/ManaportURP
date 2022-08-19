using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manapotion.PartySystem;
using Manapotion.Status;

namespace Manapotion.UI
{
    public class MainUIManager : MonoBehaviour
    {
        public CharacterUIHandle[] handles { get; private set; }

        private Party _party;

        private void Start() {
            _party = Party.Instance;

            handles = new CharacterUIHandle[_party.members.Count()];
            for (int i = 0; i < _party.members.Count(); i++)
            {
                handles[i] = new CharacterUIHandle(i);
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

