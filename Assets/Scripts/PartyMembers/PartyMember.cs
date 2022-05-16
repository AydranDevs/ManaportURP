using System.Collections.Generic;
using UnityEngine;

public class PartyMember : MonoBehaviour, IPlayerPartyMember {
    public PartyMembers partyMembers;
    
    public PartyMembers GetPartyMembers() { 
        return partyMembers;
    }

    private void Start() {
        PartyMembers party = GetPartyMembers();
        if (party.members == null) { party.members = new List<GameObject>(); }
    }
}
