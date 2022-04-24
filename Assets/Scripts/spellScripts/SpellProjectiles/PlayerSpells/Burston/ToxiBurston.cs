using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxiBurston : MonoBehaviour {
    private Rigidbody2D rb;
    private CircleCollider2D collider;
    private ParticleSystem corePs;
    private ParticleSystem trailPs;

    public GameObject explosionPf;
    private GameObject explosion;

    private GameObject thisBurston;

    public Vector3 direction;
    public float speed = 10;
    public float lifetime = 3;
    public float damage;

    private bool damageDealt = false;
    private bool hit = false;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();

        thisBurston = this.gameObject;
        corePs = thisBurston.transform.GetChild(0).GetComponent<ParticleSystem>();
        trailPs = thisBurston.transform.GetChild(1).GetComponent<ParticleSystem>();

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

        if (col.gameObject.tag == "Creature") {
            var creatureScript = col.gameObject.GetComponent<Creature>();
            if (!damageDealt) {
                damageDealt = true;
                creatureScript.Damage(damage);
            }
        }
        collider.enabled = false;

        StartCoroutine(DestroySpellImpact());
        corePs.Stop();
        trailPs.Stop();

        if (!hit) {
            GameObject explosion = Instantiate(explosionPf, transform.position, Quaternion.identity);
            hit = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.name == this.gameObject.name) return;
        if (col.gameObject.tag == "Player") return;

        if (col.gameObject.tag == "Creature") {
            var creatureScript = col.gameObject.GetComponent<Creature>();
            if (!damageDealt) {
                damageDealt = true;
                creatureScript.Damage(damage);
            }
        }
        collider.enabled = false;

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
            collider.enabled = false;
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
