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
            private float pathfindingUpdateTimer = 1f;

            Vector3 lastDirection = Vector3.zero;
            bool moveDone = false;   
            List<WorldTile> reachedPathTiles = new List<WorldTile>(); 
            Vector3 movePoint;

            private float aiUpdateTimer = 1f;

            private void Start() {
                laurie = GetComponentInParent<Laurie>();
                party = laurie.party;
                gameStateManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameStateManager>();
                grid = GameObject.FindGameObjectWithTag("WorldGrid").GetComponent<WorldGrid>();
            }

            private void Update() {
                pathfindingUpdateTimer = pathfindingUpdateTimer - Time.fixedDeltaTime;
                if (pathfindingUpdateTimer <= 0f) { PathfindingUpdate(); pathfindingUpdateTimer = 1f; }

                if (party.partyLeader == PartyLeader.Laurie) { return; }
                leader = party.members[(int)party.partyLeader];
                leaderTransform = leader.transform;

                if (path != null && path.Count > 0) {
                    if (!moveDone) {
                        for (int i = 0; i < path.Count; i++) {
                            if (reachedPathTiles.Contains(path[i])) {
                                movePoint = new Vector3(path[i].worldPos.x, path[i].worldPos.y, 0); 
                                continue;
                            }else { 
                                reachedPathTiles.Add(path[i]);
                                break;
                            }
                        }

                        WorldTile wt = reachedPathTiles[reachedPathTiles.Count - 1];
                        lastDirection = new Vector3(Mathf.Ceil(wt.cellX - laurie.transform.position.x), Mathf.Ceil(wt.cellY - laurie.transform.position.y), 0);
                        if (lastDirection.Equals(Vector3.up)) provider.inputState.movementDirection.y = 1;
                        if (lastDirection.Equals(Vector3.down)) provider.inputState.movementDirection.y = -1;
                        if (lastDirection.Equals(Vector3.left)) provider.inputState.movementDirection.x = -1;
                        if (lastDirection.Equals(Vector3.right)) provider.inputState.movementDirection.x = 1;
                        moveDone = true;
                    }else {
                        provider.inputState.movementDirection = Vector2.zero;
                        if (Vector3.Distance(laurie.transform.position, movePoint) <= .001f) {
                            moveDone = false;
                        }
                    }
                }
            }

            private void PathfindingUpdate() {
                if (party.partyLeader == PartyLeader.Laurie) { return; }
                // path = Pathfinding.FindPath(grid, laurie.transform.position, leaderTransform.position);
            }

            
        }
    }
}
