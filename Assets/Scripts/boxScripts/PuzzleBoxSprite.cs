using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PuzzleBoxSprite : ScriptableObject {
    public Sprite inactiveSprite;
    public Sprite activeSprite;

    public int boxTypeId;
}
