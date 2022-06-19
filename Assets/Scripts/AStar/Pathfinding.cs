using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manapotion.Pathfinding {
    /* 
    Class for A* Pathfinding
    */
    public class Pathfinding {

        private const int MOVE_STRAIGHT_COST = 10;
        private const int MOVE_DIAGONAL_COST = 14;

        // private static WorldGrid _grid;
        private static List<WorldTile> openList;
        private static List<WorldTile> closedList;

        public static List<WorldTile> FindPath(WorldGrid grid, Vector3 startPosition, Vector3 endPosition) {
            WorldTile startTile = grid.WorldPositionToTile(startPosition);
            WorldTile endTile = grid.WorldPositionToTile(endPosition);
            
            openList = new List<WorldTile>();
            closedList = new List<WorldTile>();

            openList.Add(startTile);

            // loop through every tile and set their g cost up really high
            for (int x = 0; x < grid.gridBoundX; x++) {
                for (int y = 0; y < grid.gridBoundY; y++) {
                    WorldTile worldTile = grid.sortedTiles[x, y];
                    worldTile.gCost = 99999;
                    worldTile.CalculateFCost();
                    worldTile.cameFromTile = null;
                }
            }

            startTile.gCost = 0;
            startTile.hCost = CalculateDistanceCost(startTile, endTile);
            startTile.CalculateFCost();

            while (openList.Count > 0) {
                WorldTile currentTile = GetLowestFCostTile(openList);
                if (currentTile == endTile) {
                    // reached goal
                    return CalculatePath(endTile);
                }

                openList.Remove(currentTile);
                closedList.Add(currentTile);

                foreach (WorldTile neighbourTile in grid.GetNeighbours(currentTile.gridX, currentTile.gridY, grid.gridBoundX, grid.gridBoundY)) {
                    if (closedList.Contains(neighbourTile)) continue;
                    if (!neighbourTile.walkable) {
                        closedList.Add(neighbourTile);
                        continue;
                    }

                    int tentativeGCost = currentTile.gCost + CalculateDistanceCost(currentTile, neighbourTile);
                    if (tentativeGCost < neighbourTile.gCost) {
                        neighbourTile.cameFromTile = currentTile;
                        neighbourTile.gCost = tentativeGCost;
                        neighbourTile.hCost = CalculateDistanceCost(neighbourTile, endTile);
                        neighbourTile.CalculateFCost();

                        if (!openList.Contains(neighbourTile)) {
                            openList.Add(neighbourTile);
                        }
                    }
                }
            }
            
            // no path can be found
            return null;
        }

        private static List<WorldTile> CalculatePath(WorldTile endTile) {
            List<WorldTile> path = new List<WorldTile>();
            path.Add(endTile);
            WorldTile currentTile = endTile;
            while (currentTile.cameFromTile != null) {
                path.Add(currentTile.cameFromTile);
                currentTile = currentTile.cameFromTile;
            }
            path.Reverse();
            return path;
        }

        private static int CalculateDistanceCost(WorldTile a, WorldTile b) {
            int xDist = Mathf.Abs(a.gridX - b.gridX);
            int yDist = Mathf.Abs(a.gridY - b.gridY);
            int remaining = Mathf.Abs(xDist - yDist);
            return MOVE_DIAGONAL_COST * Mathf.Min(xDist, yDist) + MOVE_STRAIGHT_COST * remaining;
        }

        private static WorldTile GetLowestFCostTile(List<WorldTile> tiles) {
            WorldTile lowestFCostTile = tiles[0];
            for (int i = 1; i < tiles.Count; i++) {
                if (tiles[i].fCost < lowestFCostTile.fCost) {
                    lowestFCostTile = tiles[i];
                }
            }
            return lowestFCostTile;
        }   
    }
}
