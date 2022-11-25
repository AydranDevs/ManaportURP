using System.Collections.Generic;
using UnityEngine;
using Manapotion.AStarPathfinding;

namespace Manapotion.PartySystem.WinsleyCharacter
{
    // public class WinsleyPartyInput
    // {
    //     private const int FRAMES_TO_CALC_PATH = 50;
    //     private int _frames = 0;

    //     private Party _party;
    //     private Winsley _winsley;
    //     private GameStateManager _gameManager;
    //     private InputProvider _provider;

    //     private GameObject _leader;
    //     private Transform _leaderTransform;

    //     private WorldGrid _grid;
    //     private List<WorldTile> _path;
    //     private bool _goalReached = false;

    //     public WinsleyPartyInput(Winsley winsley)
    //     {
    //         _winsley = winsley;

    //         _party = _winsley.party;
    //         _gameManager = GameStateManager.Instance;
    //         _provider = _winsley.inputProvider;

    //         _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameStateManager>();
    //         _grid = GameObject.FindGameObjectWithTag("WorldGrid").GetComponent<WorldGrid>();

    //         _path = new List<WorldTile>();
    //     }

    //     public void Update()
    //     {
    //         _frames++;
    //         if (_frames == FRAMES_TO_CALC_PATH)
    //         {
    //             RecalcPath();
    //             _frames = 0;
    //         }

    //         if (_party.partyLeader == PartyLeader.Winsley)
    //         {
    //             return;
    //         }
            
            
    //         _leader = _party.members[(int)_party.partyLeader];
    //         _leaderTransform = _leader.transform;

    //         if (_path != null && _path.Count > 0)
    //         {
    //             PathfindingUpdate();
    //         }
    //         else
    //         {
    //             _provider.inputState.movementDirection = Vector2.zero;
    //         }
    //     }


    //     int pathIndex = 0;
    //     private void RecalcPath()
    //     {
    //         if (_party.partyLeader == PartyLeader.Winsley)
    //         {
    //             return;
    //         }
            
    //         _path = Pathfinding.FindPath(_grid, _winsley.transform.position, _leaderTransform.position);
    //         _goalReached = false;
    //         pathIndex = 0;
    //     }

    //     // runs when path isnt null
    //     private void PathfindingUpdate()
    //     {
    //         if (_goalReached)
    //         {
    //             return;
    //         }
            
    //         if (Vector2.Distance(_winsley.transform.position, _leaderTransform.position) > _party.maxDistance)
    //         {
    //             _provider.inputState.isSprinting = true;
    //         }
    //         else
    //         {
    //             _provider.inputState.isSprinting = false;
    //         }

    //         // runs while the char is not at the target
    //         if (_grid.WorldPositionToTile(_winsley.transform.position) != _path[pathIndex])
    //         {
    //             MoveToTile(_path[pathIndex]);
    //             return;
    //         }

    //         if (pathIndex == _path.Count - 1)
    //         {
    //             _provider.inputState.movementDirection = Vector2.zero;
    //             _goalReached = true;
    //         }
    //         else
    //         {
    //             pathIndex++;
    //             _goalReached = false;
    //         }
    //     }

    //     private void MoveToTile(WorldTile tile)
    //     {
    //         WorldTile currentTile = _grid.WorldPositionToTile(_winsley.transform.position);

    //         if (currentTile == tile) 
    //         {
    //             return;
    //         }

    //         if (currentTile.gridX > tile.gridX)
    //         {
    //             _provider.inputState.movementDirection.x = -1;
    //         }
    //         else if (currentTile.gridX < tile.gridX)
    //         {
    //             _provider.inputState.movementDirection.x = 1;
    //         }
    //         else
    //         {
    //             _provider.inputState.movementDirection.x = 0;
    //         }

    //         if (currentTile.gridY > tile.gridY)
    //         {
    //             _provider.inputState.movementDirection.y = -1;
    //         }
    //         else if (currentTile.gridY < tile.gridY)
    //         {
    //             _provider.inputState.movementDirection.y = 1;
    //         }
    //         else
    //         {
    //             _provider.inputState.movementDirection.y = 0;
    //         }
    //     }
    // }
}
