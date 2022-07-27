using System.Collections.Generic;
using UnityEngine;

public class TileModifierBase : MonoBehaviour {
    public Vector2 position;
    public List<Vector2> extraPositions;

    public bool walkable = false;
}
