using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFlipX : MonoBehaviour {
    private SpriteRenderer renderer;

    private void Start() {
        renderer = GetComponent<SpriteRenderer>();

        int num = UnityEngine.Random.Range(0, 2);
        if (num == 1) renderer.flipX = true;
    }
}
