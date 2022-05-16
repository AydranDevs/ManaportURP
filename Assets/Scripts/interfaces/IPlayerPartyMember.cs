using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public struct PartyMembers {
    public GameObject leader;
    public List<GameObject> members;
}

public interface IPlayerPartyMember {
    public PartyMembers GetPartyMembers();
}
