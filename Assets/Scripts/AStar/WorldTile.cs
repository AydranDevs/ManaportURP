using System.Collections.Generic;
using UnityEngine;

public class WorldTile : MonoBehaviour {
    private WorldGrid grid;
    public int gridX, gridY, cellX, cellY;

    public int gCost;
    public int hCost;
    public int fCost;

    public WorldTile cameFromTile;

    public bool walkable = true;
    public List<WorldTile> neighbours;

    public void CalculateFCost() {
        fCost = gCost + hCost;
    }

    public WorldTile(bool _walkable, int _gridX, int _gridY) {
        walkable = _walkable;
        gridX = _gridX;
        gridY = _gridY;
    }

    
}
