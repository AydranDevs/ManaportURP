using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CreateGrid : MonoBehaviour {
    private Grid grid;
    public Tilemap walkable;
    public List<Tilemap> unwalkableLayers;
    public GameObject gridNode; // Generated tiles stored here
    public GameObject nodePrefab;

    // these are the bounds of where we are searching in the world for tiles, have to use world coords to check for tiles in the tile map
    public int scanStartX = -100, scanStartY = -100, scanFinishX = 100, scanFinishY = 100, gridSizeX, gridSizeY;

    private List<GameObject> unsortedTiles;
    public GameObject[,] sortedTiles; // Sorted 2d array of tiles, may contain null entries
    private int gridBoundX = 0, gridBoundY = 0; // used fo checking if boundary is reached during scan, preventing stack overflow error when adding cells to the unsortedTiles list

    private void Start() {
        grid = GetComponent<Grid>();

        // define grid size with scanStart and scanFinish (modifiable in inspector)
        gridSizeX = Mathf.Abs(scanStartX) + Mathf.Abs(scanFinishX);
        gridSizeY = Mathf.Abs(scanStartY) + Mathf.Abs(scanFinishY);

        HandleCreateGrid();
    }

    private void HandleCreateGrid() {
        int gridX = 0, gridY = 0; // used to ensure we arent hitting the boundary of the scan area
        bool foundTileOnLastPass = false;

        // loop through every tile according to grid position
        for (int x = scanStartX; x < scanFinishX; x++) {
            for (int y = scanStartY; y < scanFinishY; y++) {
                TileBase tile = walkable.GetTile(new Vector3Int(x, y, 0));
                if (tile != null) {
                    bool foundObstacle = false;

                    // for each unwalkable layer, check if the tile occupies an unwalkable space, if so, we've found an obstacle
                    foreach (Tilemap unwalkableLayer in unwalkableLayers) {
                        TileBase tile2 = unwalkableLayer.GetTile(new Vector3Int(x, y, 0));
                        if (tile2 != null) { foundObstacle = true; }
                    }

                    Vector3 worldPos = new Vector3(x + 0.5f + grid.transform.position.x, y + 0.5f + grid.transform.position.y, 0);
                    GameObject node = (GameObject)Instantiate(nodePrefab, worldPos, Quaternion.Euler(0, 0, 0));
                    Vector3Int cellPos = walkable.WorldToCell(worldPos);
                    WorldTile worldTile = node.GetComponent<WorldTile>();
                    worldTile.gridX = gridX;
                    worldTile.gridY = gridY;
                    worldTile.cellX = cellPos.x;
                    worldTile.cellY = cellPos.y;
                    node.transform.parent = gridNode.transform;

                }
            }
        }
    }

    public List<WorldTile> GetNeighbours(int x, int y, int width, int height) {
        List<WorldTile> myNeighbours = new List<WorldTile>();
 
        if (x > 0 && x < width - 1) {
            if (y > 0 && y < height - 1) {
                if (sortedTiles[x + 1, y] != null) { 
                    WorldTile wt1 = sortedTiles[x + 1, y].GetComponent<WorldTile>();
                    if (wt1 != null) myNeighbours.Add(wt1);
                }
 
                if (sortedTiles[x - 1, y] != null) {
                    WorldTile wt2 = sortedTiles[x - 1, y].GetComponent<WorldTile>();
                    if (wt2 != null) myNeighbours.Add(wt2);
                }
 
                if (sortedTiles[x, y + 1] != null) {
                    WorldTile wt3 = sortedTiles[x, y + 1].GetComponent<WorldTile>();
                    if (wt3 != null) myNeighbours.Add(wt3);
                }
 
                if (sortedTiles[x, y - 1] != null) {
                    WorldTile wt4 = sortedTiles[x, y - 1].GetComponent<WorldTile>();
                    if (wt4 != null) myNeighbours.Add(wt4);
                }
            }
            else if (y == 0)
            {
                if (sortedTiles[x + 1, y] != null) {
                    WorldTile wt1 = sortedTiles[x + 1, y].GetComponent<WorldTile>();
                    if (wt1 != null) myNeighbours.Add(wt1);
                }
 
                if (sortedTiles[x - 1, y] != null) {
                    WorldTile wt2 = sortedTiles[x - 1, y].GetComponent<WorldTile>();
                    if (wt2 != null) myNeighbours.Add(wt2);
                }
 
                if (sortedTiles[x, y + 1] == null) {
                    WorldTile wt3 = sortedTiles[x, y + 1].GetComponent<WorldTile>();
                    if (wt3 != null) myNeighbours.Add(wt3);
                }
            }
            else if (y == height - 1)
            {
                if (sortedTiles[x, y - 1] != null) {
                    WorldTile wt4 = sortedTiles[x, y - 1].GetComponent<WorldTile>();
                    if (wt4 != null) myNeighbours.Add(wt4);
                }
                if (sortedTiles[x + 1, y] != null) {
                    WorldTile wt1 = sortedTiles[x + 1, y].GetComponent<WorldTile>();
                    if (wt1 != null) myNeighbours.Add(wt1);
                }
 
                if (sortedTiles[x - 1, y] != null) {
                    WorldTile wt2 = sortedTiles[x - 1, y].GetComponent<WorldTile>();
                    if (wt2 != null) myNeighbours.Add(wt2);
                }
            }
        }
        else if (x == 0)
        {
            if (y > 0 && y < height - 1)
            {
                if (sortedTiles[x + 1, y] != null) {
                    WorldTile wt1 = sortedTiles[x + 1, y].GetComponent<WorldTile>();
                    if (wt1 != null) myNeighbours.Add(wt1);
                }
 
                if (sortedTiles[x, y - 1] != null) {
                    WorldTile wt4 = sortedTiles[x, y - 1].GetComponent<WorldTile>();
                    if (wt4 != null)myNeighbours.Add(wt4);
                }
 
                if (sortedTiles[x, y + 1] != null) {
                    WorldTile wt3 = sortedTiles[x, y + 1].GetComponent<WorldTile>();
                    if (wt3 != null) myNeighbours.Add(wt3);                    
                }
            }
            else if (y == 0)
            {
                if (sortedTiles[x + 1, y] != null) {
                    WorldTile wt1 = sortedTiles[x + 1, y].GetComponent<WorldTile>();
                    if (wt1 != null) myNeighbours.Add(wt1);
                }
 
                if (sortedTiles[x, y + 1] != null) {
                    WorldTile wt3 = sortedTiles[x, y + 1].GetComponent<WorldTile>();
                    if (wt3 != null) myNeighbours.Add(wt3);
                }
            }
            else if (y == height - 1)
            {
                if (sortedTiles[x + 1, y] != null) {
                    WorldTile wt1 = sortedTiles[x + 1, y].GetComponent<WorldTile>();
                    if (wt1 != null) myNeighbours.Add(wt1);
                }
 
                if (sortedTiles[x, y - 1] != null) {
                    WorldTile wt4 = sortedTiles[x, y - 1].GetComponent<WorldTile>();
                    if (wt4 != null) myNeighbours.Add(wt4);
                }
            }
        }
        else if (x == width - 1)
        {
            if (y > 0 && y < height - 1)
            {
                if (sortedTiles[x - 1, y] != null) {
                    WorldTile wt2 = sortedTiles[x - 1, y].GetComponent<WorldTile>();
                    if (wt2 != null) myNeighbours.Add(wt2);
                }
 
                if (sortedTiles[x, y + 1] != null) {
                    WorldTile wt3 = sortedTiles[x, y + 1].GetComponent<WorldTile>();
                    if (wt3 != null)myNeighbours.Add(wt3);
                }
 
                if (sortedTiles[x, y - 1] != null) {
                    WorldTile wt4 = sortedTiles[x, y - 1].GetComponent<WorldTile>();
                    if (wt4 != null)  myNeighbours.Add(wt4);
                }
            }
            else if (y == 0)
            {
                if (sortedTiles[x - 1, y] != null) {
                    WorldTile wt2 = sortedTiles[x - 1, y].GetComponent<WorldTile>();
                    if (wt2 != null) myNeighbours.Add(wt2);
                }
                if (sortedTiles[x, y + 1] != null) {
                    WorldTile wt3 = sortedTiles[x, y + 1].GetComponent<WorldTile>();
                    if (wt3 != null) myNeighbours.Add(wt3);
                }
            }
            else if (y == height - 1)
            {
                if (sortedTiles[x - 1, y] != null) {
                    WorldTile wt2 = sortedTiles[x - 1, y].GetComponent<WorldTile>();
                    if (wt2 != null) myNeighbours.Add(wt2);
                }
 
                if (sortedTiles[x, y - 1] != null) {
                    WorldTile wt4 = sortedTiles[x, y - 1].GetComponent<WorldTile>();
                    if (wt4 != null) myNeighbours.Add(wt4);
                }
            }
        }
 
        return myNeighbours;
    }
}
