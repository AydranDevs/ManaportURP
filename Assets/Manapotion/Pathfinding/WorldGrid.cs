using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Manapotion.Pathfinding {
    /*
    Class for collecting all ChunkGrids into one large WorldGrid for pathfinding and such.
    */
    public class WorldGrid : MonoBehaviour {
        private Grid grid;
        public Tilemap floor;
        public List<ChunkGrid> chunkGrids;
        public List<Tilemap> unwalkableLayers;
        public GameObject gridNode; 
        public GameObject nodePrefab;

        // these are the bounds of where we are searching in the world for tiles, have to use world coords to check for tiles in the tile map
        public int scanStartX = -100, scanStartY = -100, scanFinishX = 100, scanFinishY = 100, gridSizeX, gridSizeY;

        
        private List<WorldTile> unsortedTiles;
        public WorldTile[,] sortedTiles; // Sorted 2d array of tiles, may contain null entries
        [HideInInspector] public int gridBoundX = 0, gridBoundY = 0; // used for  checking if boundary is reached during scan, preventing stack overflow error when adding cells to the unsortedTiles list

        private void Start() {
            grid = GetComponent<Grid>();

            // define grid size with scanStart and scanFinish (modifiable in inspector)
            gridSizeX = Mathf.Abs(scanStartX) + Mathf.Abs(scanFinishX);
            gridSizeY = Mathf.Abs(scanStartY) + Mathf.Abs(scanFinishY);

            chunkGrids = GetLoadedChunkGrids();
            // CreateGrid();
        }

        private void CreateGrid() {
            int gridX = 0, gridY = 0; // used to ensure we arent hitting the boundary of the scan area
            bool foundTileOnLastPass = false;
            unsortedTiles = new List<WorldTile>();

            // loop through every tile according to grid position
            for (int x = scanStartX; x < scanFinishX; x++) {
                for (int y = scanStartY; y < scanFinishY; y++) {

                    TileBase tile = floor.GetTile(new Vector3Int(x, y, 0));
                    if (tile != null) {
                        bool foundObstacle = false;

                        // for each unwalkable layer, check if the tile occupies an unwalkable space, if so, we've found an obstacle
                        foreach (Tilemap unwalkableLayer in unwalkableLayers) {
                            TileBase tile2 = unwalkableLayer.GetTile(new Vector3Int(x, y, 0));
                            if (tile2 != null) { foundObstacle = true; }
                        }

                        Vector3 worldPos = new Vector3(x + 0.5f + grid.transform.position.x, y + 0.5f + grid.transform.position.y, 0);
                        Vector3Int cellPos = floor.WorldToCell(worldPos);
                        WorldTile worldTile = new WorldTile(true, gridX, gridY, worldPos);
                        worldTile.cellX = cellPos.x;
                        worldTile.cellY = cellPos.y;

                        // set a tile as walkable or not if foundObstacle is equal to true
                        if (!foundObstacle) {
                            foundTileOnLastPass = true;
                            worldTile.walkable = true;
                        } else {
                            foundTileOnLastPass = true;
                            worldTile.walkable = false;
                        }

                        unsortedTiles.Add(worldTile);

                        gridY++;
                        if (gridX > gridBoundX) {
                            gridBoundX = gridX;
                        }
                        if (gridY > gridBoundY) {
                            gridBoundY = gridY;
                        }
                    }

                }
                
                if (foundTileOnLastPass) {
                    gridX++;
                    gridY = 0;
                    foundTileOnLastPass = false;
                }
            }

            sortedTiles = new WorldTile[gridBoundX + 1, gridBoundY + 1];

            foreach (WorldTile tile in unsortedTiles) {
                sortedTiles[tile.gridX, tile.gridY] = tile;
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
            Vector3Int cellPosition = floor.WorldToCell(worldPosition);
            WorldTile tile = sortedTiles[cellPosition.x, cellPosition.y];
            return tile;
        }

        public List<ChunkGrid> GetLoadedChunkGrids() {
            List<ChunkGrid> loadedChunkGrids = new List<ChunkGrid>();
            GameObject[] grids = GameObject.FindGameObjectsWithTag("ChunkGrid");

            foreach (GameObject grid in grids) {
                ChunkGrid chunkGrid = grid.GetComponent<ChunkGrid>();
                loadedChunkGrids.Add(chunkGrid);
                Debug.Log(chunkGrid);
            }

            return loadedChunkGrids;
        }
    }
}
