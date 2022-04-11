using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour {
    public float seconds;

    void Start() {
        StartCoroutine(DestroyGameObject());
    }

    IEnumerator DestroyGameObject() {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
