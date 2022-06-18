using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGrid : MonoBehaviour {
    private Grid grid;
    public Tilemap floor;
    public List<Tilemap> unwalkableLayers;
    public GameObject gridNode; 
    public GameObject nodePrefab;

    // these are the bounds of where we are searching in the world for tiles, have to use world coords to check for tiles in the tile map
    public int scanStartX = -100, scanStartY = -100, scanFinishX = 100, scanFinishY = 100, gridSizeX, gridSizeY;

    public List<GameObject> unsortedTiles;
    public GameObject[,] sortedTiles; // Sorted 2d array of tiles, may contain null entries
    [HideInInspector] public int gridBoundX = 0, gridBoundY = 0; // used for  checking if boundary is reached during scan, preventing stack overflow error when adding cells to the unsortedTiles list

    private void Start() {
        grid = GetComponent<Grid>();

        // define grid size with scanStart and scanFinish (modifiable in inspector)
        gridSizeX = Mathf.Abs(scanStartX) + Mathf.Abs(scanFinishX);
        gridSizeY = Mathf.Abs(scanStartY) + Mathf.Abs(scanFinishY);

        CreateGrid();
    }

    private void CreateGrid() {
        int gridX = 0, gridY = 0; // used to ensure we arent hitting the boundary of the scan area
        bool foundTileOnLastPass = false;
        unsortedTiles = new List<GameObject>();

        // loop through every tile according to grid position
        for (int x = scanStartX; x < scanFinishX; x++) {
            for (int y = scanStartY; y < scanFinishY; y++) {
                // Debug.Log(x + ", " + y);

                TileBase tile = floor.GetTile(new Vector3Int(x, y, 0));
                if (tile != null) {
                    bool foundObstacle = false;

                    // for each unwalkable layer, check if the tile occupies an unwalkable space, if so, we've found an obstacle
                    foreach (Tilemap unwalkableLayer in unwalkableLayers) {
                        TileBase tile2 = unwalkableLayer.GetTile(new Vector3Int(x, y, 0));
                        if (tile2 != null) { foundObstacle = true; }
                    }

                    Vector3 worldPos = new Vector3(x + 0.5f + grid.transform.position.x, y + 0.5f + grid.transform.position.y, 0);
                    GameObject node = Instantiate(nodePrefab, worldPos, Quaternion.Euler(0, 0, 0));
                    Vector3Int cellPos = floor.WorldToCell(worldPos);
                    WorldTile worldTile = node.GetComponent<WorldTile>();
                    worldTile.gridX = gridX;
                    worldTile.gridY = gridY;
                    worldTile.cellX = cellPos.x;
                    worldTile.cellY = cellPos.y;
                    node.transform.parent = gridNode.transform;

                    // set a tile as walkable or not if foundObstacle is equal to true
                    if (!foundObstacle) {
                        foundTileOnLastPass = true;
                        node.name = "Walkable_" + gridX.ToString() + ", " + gridY.ToString();
                        node.transform.parent = floor.transform;
                        worldTile.walkable = true;
                    } else {
                        foundTileOnLastPass = true;
                        node.name = "Unwalkable_" + gridX.ToString() + ", " + gridY.ToString();
                        node.transform.parent = unwalkableLayers[0].transform;
                        worldTile.walkable = false;
                        node.GetComponent<SpriteRenderer>().color = Color.black;
                    }

                    unsortedTiles.Add(node);

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

        sortedTiles = new GameObject[gridBoundX + 1, gridBoundY + 1];

        foreach (GameObject tile in unsortedTiles) {
            WorldTile wt = tile.GetComponent<WorldTile>();
            sortedTiles[wt.gridX, wt.gridY] = tile;
        }
        for (int x = 0; x < gridBoundX; x++) {
            for (int y = 0; y < gridBoundY; y++) {
                if (sortedTiles[x, y] != null) {
                    WorldTile wt = sortedTiles[x, y].GetComponent<WorldTile>();
                    wt.neighbours = GetNeighbours(x, y, gridBoundX, gridBoundY);
                }
            }
        }
    }

    public List<WorldTile> GetNeighbours(int x, int y, int width, int height) {
        List<WorldTile> myNeighbours = new List<WorldTile>();

        if (x < width - 1) {
            if (sortedTiles[x + 1, y]  != null) { // right
                WorldTile rightTile = sortedTiles[x + 1, y].GetComponent<WorldTile>();
                myNeighbours.Add(rightTile);
            }

            if (y > 0) {
                if (sortedTiles[x + 1, y - 1]  != null) { // down right
                    WorldTile downRightTile = sortedTiles[x + 1, y - 1].GetComponent<WorldTile>();
                    myNeighbours.Add(downRightTile);
                }
            }
        }
        if (y > 0) {
            if (sortedTiles[x, y - 1]  != null) { // down
                WorldTile downTile = sortedTiles[x, y - 1].GetComponent<WorldTile>();
                myNeighbours.Add(downTile);
            }
            if (x > 0) {
                if (sortedTiles[x - 1, y - 1]  != null) { // down left
                    WorldTile downLeftTile = sortedTiles[x - 1, y - 1].GetComponent<WorldTile>();
                    myNeighbours.Add(downLeftTile);
                }
            }
        }
        if (x > 0) {
            if (sortedTiles[x - 1, y]  != null) { // left
                WorldTile leftTile = sortedTiles[x - 1, y].GetComponent<WorldTile>();
                myNeighbours.Add(leftTile);
            }
            if (y < height - 1) {
                if (sortedTiles[x - 1, y + 1]  != null) { // up left
                    WorldTile upLeftTile = sortedTiles[x - 1, y + 1].GetComponent<WorldTile>();
                    myNeighbours.Add(upLeftTile);
                }
            }
        }
        if (y < height - 1) {
            if (sortedTiles[x, y + 1]  != null) { // up
                WorldTile upTile = sortedTiles[x, y + 1].GetComponent<WorldTile>();
                myNeighbours.Add(upTile);
            }
            if (x < width - 1) {
                if (sortedTiles[x + 1, y + 1]  != null) { // up right
                    WorldTile upRightTile = sortedTiles[x + 1, y + 1].GetComponent<WorldTile>();
                    myNeighbours.Add(upRightTile);
                }
            } 
        }
        
        return myNeighbours;
    }

    public WorldTile WorldPositionToTile(Vector3 worldPosition) {
        Vector3Int cellPosition = floor.WorldToCell(worldPosition);
        WorldTile tile = sortedTiles[cellPosition.x, cellPosition.y].GetComponent<WorldTile>();
        sortedTiles[cellPosition.x, cellPosition.y].GetComponent<SpriteRenderer>().color = Color.green;
        return tile;
    }
}
