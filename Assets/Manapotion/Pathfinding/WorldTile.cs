using System.Collections.Generic;
using UnityEngine;

namespace Manapotion.Pathfinding {
    /*
    Class for tile used in Grid system
    */
    public class WorldTile {
        private WorldGrid grid;
        public int gridX, gridY, cellX, cellY;
        public Vector3 worldPos;

        public int gCost;
        public int hCost;
        public int fCost;

        public WorldTile cameFromTile;
        public WorldTile nextTile;

        public bool walkable = true;
        public List<WorldTile> neighbours;

        public void CalculateFCost() {
            fCost = gCost + hCost;
        }

        public void GetNextTile() {
            foreach (WorldTile neighbour in neighbours) {
                if (neighbour.cameFromTile != this) { continue; }
                nextTile = neighbour;
                return;
            }
        }

        public Vector2 GetMovementVector() { // returns movement vector required to reach next tile
            GetNextTile();
            Vector2 movementVector = new Vector2(0, 0);
            if (nextTile == null) { return movementVector; } // if no next tile, don't move

            if (nextTile.gridX > gridX) { movementVector.x = 1; } else if (nextTile.gridX < gridX) { movementVector.x = -1; }
            if (nextTile.gridY > gridY) { movementVector.y = 1; } else if (nextTile.gridY < gridY) { movementVector.y = -1; }
            return movementVector;
        }

        public WorldTile(bool _walkable, int _gridX, int _gridY, Vector3 _worldPos) {
            walkable = _walkable;
            gridX = _gridX;
            gridY = _gridY;
            worldPos = _worldPos;
        }

        
    }
}
