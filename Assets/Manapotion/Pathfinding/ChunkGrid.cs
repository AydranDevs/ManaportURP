using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Manapotion.Pathfinding {

    public class ChunkGrid : MonoBehaviour {
        private Grid grid;
        public Tilemap floor;
        public List<Tilemap> unwalkableLayers;
        public GameObject gridNode;

        // these are the bounds of where we are searching in the world for tiles, have to use world coords to check for tiles in the tile map
        private int scanStartX = 0, scanStartY = 0, scanFinishX = 100, scanFinishY = 100;
        public int gridSizeX, gridSizeY;
        public int chunkX, chunkY;
        
        private List<WorldTile> unsortedTiles;
        public WorldTile[,] sortedTiles; // Sorted 2d array of tiles, may contain null entries
        [HideInInspector] public int gridBoundX = 0, gridBoundY = 0; // used for  checking if boundary is reached during scan, preventing stack overflow error when adding cells to the unsortedTiles list
        
        private void Start() {
            chunkX = (int)transform.position.x / 100;
            chunkY = (int)transform.position.y / 100;

            if (WorldGrid.IsChunkLoaded_Static(this.gameObject.name)) return;
            
            grid = GetComponent<Grid>();

            // define grid size with scanStart and scanFinish (modifiable in inspector)
            gridSizeX = Mathf.Abs(scanStartX) + Mathf.Abs(scanFinishX);
            gridSizeY = Mathf.Abs(scanStartY) + Mathf.Abs(scanFinishY);

            CreateGrid();
            WorldGrid.AddTilesToWorld_Static(unsortedTiles, this.gameObject.name);
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
                        WorldTile worldTile = new WorldTile(true, gridX, gridY, worldPos, chunkX, chunkY);
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
        }
    }
}

