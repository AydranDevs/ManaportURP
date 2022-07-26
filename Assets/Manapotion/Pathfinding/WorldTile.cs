using System.Collections.Generic;
using UnityEngine;

namespace Manapotion.Pathfinding {
    /*
    Class for tile used in Grid system
    */
    public class WorldTile {
        public int chunkX, chunkY;
        public int gridX, gridY, cellX, cellY;
        public Vector3 worldPos;

        public int gCost;
        public int hCost;
        public int fCost;

        public WorldTile cameFromTile;

        public bool walkable = true;
        public List<WorldTile> neighbours;

        public void CalculateFCost() {
            fCost = gCost + hCost;
        }

        public WorldTile GetNextTile() {
            WorldTile nextTile;

            foreach (WorldTile neighbour in neighbours) {
                if (neighbour.cameFromTile != this) { continue; }
                nextTile = neighbour;
                return nextTile;
            }

            // this is likely the final tile in the path
            return null;
        }

        // returns the vector needed to get to next tile
        public Vector3 GetMovmentDir() {
            WorldTile next = GetNextTile();
            // if (next == null) return Vector3.zero;

            float x = 0f;
            float y = 0f;

            if (next.gridX > gridX) { x = 1f; } else if (next.gridX < gridX) { x = -1f; }
            if (next.gridY > gridY) { y = 1f; } else if (next.gridY < gridY) { y = -1f; }

            return new Vector3(x, y, 0);  
        }

        public WorldTile(bool _walkable, int _gridX, int _gridY, Vector3 _worldPos, int _chunkX, int _chunkY) {
            chunkX = _chunkX;
            chunkY = _chunkY;
            worldPos = _worldPos;
            walkable = _walkable;
            gridX = _gridX + chunkX * 100;
            gridY = _gridY + chunkY * 100;

            // Debug.Log(gridX + ", " + gridY);
        }

        
    }
}
