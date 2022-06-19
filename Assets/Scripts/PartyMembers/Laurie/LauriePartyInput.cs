using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manapotion.Pathfinding;

namespace PartyNamespace {
    namespace LaurieNamespace {
        public class LauriePartyInput : MonoBehaviour {
            private Party party;
            private Laurie laurie;
            private GameStateManager gameStateManager;
            [SerializeField] private InputProvider provider;

            private GameObject leader;
            private Transform leaderTransform;

            private WorldGrid grid;
            public List<WorldTile> path;

            private void Start() {
                laurie = GetComponentInParent<Laurie>();
                party = laurie.party;
                gameStateManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameStateManager>();
                grid = GameObject.FindGameObjectWithTag("WorldGrid").GetComponent<WorldGrid>();
            }

            private void Update() {
                if (party.partyLeader == PartyLeader.Laurie) { return; }
                leader = party.members[(int)party.partyLeader];
                leaderTransform = leader.transform;

                FollowTheLeader();
            }

            private void FollowTheLeader() {
                path = Pathfinding.FindPath(grid, laurie.transform.position, leaderTransform.position);
                foreach (WorldTile t in path) {
                    Debug.Log(t.gridX + ", " + t.gridY);
                }
            }
        }
    }
}
