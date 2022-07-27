using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manapotion.Pathfinding;

public class SetTileAtPosToUnwalkable : MonoBehaviour {
    private WorldGrid grid;
    public List<Vector2> extraPositions;

    private void Start() {
        grid = GameObject.FindGameObjectWithTag("WorldGrid").GetComponent<WorldGrid>();

        grid.tileModifiers.Add(this);
    }
}
