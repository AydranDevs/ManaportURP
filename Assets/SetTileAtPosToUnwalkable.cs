using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manapotion.Pathfinding;

public class SetTileAtPosToUnwalkable : MonoBehaviour {
    private WorldGrid grid;
    public List<Vector2> extraPositions;

    private void Start() {
        grid = GameObject.FindGameObjectWithTag("WorldGrid").GetComponent<WorldGrid>();

        WorldTile tile = grid.WorldPositionToTile(this.transform.position);
        Debug.Log(tile.gridX + ", " + tile.gridY);
        tile.walkable = false;

        foreach (var pos in extraPositions) {
            WorldTile wt = grid.WorldPositionToTile(new Vector2(this.transform.position.x + pos.x, this.transform.position.y + pos.y));
            wt.walkable = false;
        }
    }
}
