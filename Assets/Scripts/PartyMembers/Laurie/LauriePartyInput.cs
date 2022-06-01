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

                FollowTheLeader();
            }

            private void FollowTheLeader() {
                float distance = Vector2.Distance((Vector2)laurie.transform.position, (Vector2)leaderTransform.position);
                if (distance > party.maxDistance) { return; }

                Vector2 direction = (Vector2)leaderTransform.position - (Vector2)laurie.transform.position; 

                provider.inputState.movementDirection = direction;
            }
        }
    }
}
