using System.Collections.Generic;
using UnityEngine;
using Manapotion.Pathfinding;

namespace PartyNamespace {
    namespace LaurieNamespace {
        public class LauriePartyInput : MonoBehaviour {
            private const int FRAMES_TO_CALC_PATH = 50;
            private int frames = 0;


            private Party party;
            private Laurie laurie;
            private Pathfinding pathfinding;
            private GameStateManager gameStateManager;
            [SerializeField] private InputProvider provider;

            private GameObject leader;
            private Transform leaderTransform;

            private WorldGrid grid;
            private List<WorldTile> path;
            private bool goalReached = false;

            private void Start() {
                laurie = GetComponentInParent<Laurie>();
                party = laurie.party;
                gameStateManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameStateManager>();
                grid = GameObject.FindGameObjectWithTag("WorldGrid").GetComponent<WorldGrid>();

                path = new List<WorldTile>();
                pathfinding = new Pathfinding();
            }

            private void Update() {
                frames++;
                if (frames == FRAMES_TO_CALC_PATH) {
                    RecalcPath();
                    frames = 0;
                }

                if (party.partyLeader == PartyLeader.Laurie) { return; }
                
                leader = party.members[(int)party.partyLeader];
                leaderTransform = leader.transform;

                if (path != null && path.Count > 0) {
                    PathfindingUpdate();
                }
            }


            int pathIndex = 0;
            private void RecalcPath() {
                if (party.partyLeader == PartyLeader.Laurie) { return; }
                
                path = Pathfinding.FindPath_Static(grid, laurie.transform.position, leaderTransform.position);
                goalReached = false;
                pathIndex = 0;
            }

            // runs when path isnt null
            private void PathfindingUpdate() {
                if (goalReached) { return; }
                // Debug.Log(string.Format("current path: path from Point A at ({0}, {1}) to Point B at ({2}, {3})", path[0].gridX, path[0].gridY, path[path.Count - 1].gridX, path[path.Count - 1].gridY));
                if (Vector2.Distance(laurie.transform.position, leaderTransform.position) > party.maxDistance) {
                    provider.inputState.isSprinting = true;
                }else {
                    provider.inputState.isSprinting = false;
                }



                // runs while the char is not at the target
                if (grid.WorldPositionToTile(laurie.transform.position) != path[pathIndex]) {
                    MoveToTile(path[pathIndex]);
                    return;
                }

                if (pathIndex == path.Count - 1) {
                    provider.inputState.movementDirection = Vector2.zero;
                    goalReached = true;
                }else {
                    pathIndex++;
                    goalReached = false;
                }
            }

            private void MoveToTile(WorldTile tile) {
                WorldTile currentTile = grid.WorldPositionToTile(laurie.transform.position);

                if (currentTile == tile) return;

                if (currentTile.gridX > tile.gridX) {
                    provider.inputState.movementDirection.x = -1;
                }else if (currentTile.gridX < tile.gridX) {
                    provider.inputState.movementDirection.x = 1;
                }else {
                    provider.inputState.movementDirection.x = 0;
                }

                if (currentTile.gridY > tile.gridY) {
                    provider.inputState.movementDirection.y = -1;
                }else if (currentTile.gridY < tile.gridY) {
                    provider.inputState.movementDirection.y = 1;
                }else {
                    provider.inputState.movementDirection.y = 0;
                }
            }
        }
    }
}
