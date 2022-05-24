using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PartyNamespace {
    namespace LaurieNamespace {
        public class LauriePartyInput : MonoBehaviour {
            private Party party;
            private Laurie laurie;
            private GameStateManager gameStateManager;
            [SerializeField] private InputProvider provider;

            private GameObject leader;
            private Transform leaderTransform;

            private void Start() {
                laurie = GetComponentInParent<Laurie>();
                party = laurie.party;
                gameStateManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameStateManager>();
            }

            private void Update() {
                if (party.partyLeader == PartyLeader.Laurie) { return; }
                leader = party.members[(int)party.partyLeader];
                leaderTransform = leader.transform;
            }
        }
    }
}
