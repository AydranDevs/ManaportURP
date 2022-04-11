using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Automa : MonoBehaviour {
    private Rigidbody2D rb;
    private ParticleSystem corePs;
    private ParticleSystem trailPs;

    public GameObject explosionPf;
    private GameObject explosion;

    private GameObject thisAutoma;

    public Vector3 direction;
    public float speed = 10;
    public float lifetime = 3;

    private bool hit = false;

    void Start() {
        rb = GetComponent<Rigidbody2D>();

        thisAutoma = this.gameObject;
        corePs = thisAutoma.transform.GetChild(0).GetComponent<ParticleSystem>();
        trailPs = thisAutoma.transform.GetChild(1).GetComponent<ParticleSystem>();

        StartCoroutine(StopParticles());
        StartCoroutine(DestroySpell());
    }

    void Update() {
        if (direction != null) {
            transform.position = transform.position + (direction * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.name == this.gameObject.name) return;
        if (col.gameObject.tag == "Player") return;

        StartCoroutine(DestroySpellImpact());
        corePs.Stop();
        trailPs.Stop();

        if (!hit) {
            GameObject explosion = Instantiate(explosionPf, transform.position, Quaternion.identity);
            hit = true;
        }
    }

    IEnumerator StopParticles() {
        yield return new WaitForSeconds(lifetime);
        if (gameObject != null) {
            corePs.Stop();
            trailPs.Stop();
        }
    }

    IEnumerator DestroySpell() {
        yield return new WaitForSeconds(lifetime + 2);
        if (gameObject != null) {
            Destroy(gameObject);
        }
    }

    IEnumerator DestroySpellImpact() {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
