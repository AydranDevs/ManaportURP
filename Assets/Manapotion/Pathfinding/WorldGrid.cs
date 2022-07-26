using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manapotion.Pathfinding {
    /*
    Class for collecting all ChunkGrids into one large WorldGrid for pathfinding and such.
    */
    public class WorldGrid : MonoBehaviour {
        private static WorldGrid Instance;
        private Grid grid;
        [SerializeField] private List<string> chunkIds;
        public GameObject gridNode; 
        public GameObject nodePrefab;

        // these are the bounds of where we are searching in the world for tiles, have to use world coords to check for tiles in the tile map
        public int scanStartX = -100, scanStartY = -100, scanFinishX = 100, scanFinishY = 100, gridSizeX, gridSizeY;
        
        private List<WorldTile> unsortedTiles;
        public WorldTile[,] sortedTiles; // Sorted 2d array of tiles, may contain null entries
        public int gridBoundX = 1000, gridBoundY = 1000; // used for  checking if boundary is reached during scan, preventing stack overflow error when adding cells to the unsortedTiles list
        
        private void Start() {
            gridBoundX = 1000;
            gridBoundY = 1000;
            Instance = this;
            chunkIds = new List<string>();
            unsortedTiles = new List<WorldTile>();
            sortedTiles = new WorldTile[gridBoundX, gridBoundY];
            
            grid = GetComponent<Grid>();

            // define grid size with scanStart and scanFinish (modifiable in inspector)
            gridSizeX = Mathf.Abs(scanStartX) + Mathf.Abs(scanFinishX);
            gridSizeY = Mathf.Abs(scanStartY) + Mathf.Abs(scanFinishY);

        }
    
        private void SortTiles() {
            foreach (var tile in unsortedTiles) {
                if (sortedTiles[tile.gridX, tile.gridY] == null) {
                    sortedTiles[tile.gridX, tile.gridY] = tile;
                }
            }

            for (int x = 0; x < gridBoundX; x++) {
                for (int y = 0; y < gridBoundY; y++) { 
                    if (sortedTiles[x, y] != null) {
                        sortedTiles[x, y].neighbours = GetNeighbours(x, y, gridBoundX, gridBoundY);
                    }
                }
            }
        }

        public List<WorldTile> GetNeighbours(int x, int y, int width, int height) {
            List<WorldTile> myNeighbours = new List<WorldTile>();

            if (x < width - 1) {
                if (sortedTiles[x + 1, y]  != null) { // right
                    WorldTile rightTile = sortedTiles[x + 1, y];
                    myNeighbours.Add(rightTile);
                }

                if (y > 0) {
                    if (sortedTiles[x + 1, y - 1]  != null) { // down right
                        WorldTile downRightTile = sortedTiles[x + 1, y - 1];
                        myNeighbours.Add(downRightTile);
                    }
                }
            }
            if (y > 0) {
                if (sortedTiles[x, y - 1]  != null) { // down
                    WorldTile downTile = sortedTiles[x, y - 1];
                    myNeighbours.Add(downTile);
                }
                if (x > 0) {
                    if (sortedTiles[x - 1, y - 1]  != null) { // down left
                        WorldTile downLeftTile = sortedTiles[x - 1, y - 1];
                        myNeighbours.Add(downLeftTile);
                    }
                }
            }
            if (x > 0) {
                if (sortedTiles[x - 1, y]  != null) { // left
                    WorldTile leftTile = sortedTiles[x - 1, y];
                    myNeighbours.Add(leftTile);
                }
                if (y < height - 1) {
                    if (sortedTiles[x - 1, y + 1]  != null) { // up left
                        WorldTile upLeftTile = sortedTiles[x - 1, y + 1];
                        myNeighbours.Add(upLeftTile);
                    }
                }
            }
            if (y < height - 1) {
                if (sortedTiles[x, y + 1]  != null) { // up
                    WorldTile upTile = sortedTiles[x, y + 1];
                    myNeighbours.Add(upTile);
                }
                if (x < width - 1) {
                    if (sortedTiles[x + 1, y + 1]  != null) { // up right
                        WorldTile upRightTile = sortedTiles[x + 1, y + 1];
                        myNeighbours.Add(upRightTile);
                    }
                } 
            }
            
            return myNeighbours;
        }

        public WorldTile WorldPositionToTile(Vector3 worldPosition) {
            return sortedTiles[(int)worldPosition.x, (int)worldPosition.y];
        }

        private void AddTilesToWorld(List<WorldTile> tiles, string chunkId) {
            foreach (var tile in tiles) {
                unsortedTiles.Add(tile);
            }
            chunkIds.Add(chunkId);

            // rebuild grid
            SortTiles();
        }

        private bool IsChunkLoaded(string chunkId) {
            if (chunkIds.Contains(chunkId)) {
                return true;
            }else {
                return false;
            }
        }

        public static void AddTilesToWorld_Static(List<WorldTile> tiles, string chunkId) {
            Instance.AddTilesToWorld(tiles, chunkId);
        }

        public static bool IsChunkLoaded_Static(string chunkId) {
            return Instance.IsChunkLoaded(chunkId);
        }
    }
}