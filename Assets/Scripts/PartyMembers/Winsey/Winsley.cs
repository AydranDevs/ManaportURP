using System.Collections.Generic;
using UnityEngine;

public class Winsley : PartyMember {
    [SerializeField] public PartyMembers party;

    private void Start() {
        party = GetPartyMembers();

        party.members.Add(this.gameObject);
    }
}
