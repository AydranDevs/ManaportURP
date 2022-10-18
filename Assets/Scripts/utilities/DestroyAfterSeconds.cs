using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour {
    [SerializeField] private float seconds;
    [SerializeField] private ParticleSystem ps;

    void Start() {
        if (ps) {
            StartCoroutine(DestroyParticleSystem());
        }
        
        StartCoroutine(DestroyGameObject());
    }

    IEnumerator DestroyGameObject() {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

    IEnumerator DestroyParticleSystem() {
        yield return new WaitForSeconds(seconds);
        ps.Stop();
    }

    private void DestroyAfterParticleSystemStopped() {
        Destroy(gameObject);
    }
}
