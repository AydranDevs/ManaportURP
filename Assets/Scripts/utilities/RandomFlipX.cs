using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFlipX : MonoBehaviour {
    private SpriteRenderer mySpriteRenderer;

    private void Start() {
        mySpriteRenderer = GetComponent<SpriteRenderer>();

        int num = UnityEngine.Random.Range(0, 2);
        if (num == 1) mySpriteRenderer.flipX = true;
    }
}
