using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoltBurston : SpellProjectile {
    private ParticleSystem lightningPs;

    public GameObject explosionPf;
    private GameObject explosion;

    private GameObject thisBurston;

    private bool damageDealt = false;
    private bool hit = false;

    void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();

        thisSpellProjectile = this.gameObject;
        corePs = thisSpellProjectile.transform.GetChild(0).GetComponent<ParticleSystem>();
        lightningPs = thisSpellProjectile.transform.GetChild(1).GetComponent<ParticleSystem>();
        trailPs = thisSpellProjectile.transform.GetChild(2).GetComponent<ParticleSystem>();

        StartCoroutine(StopParticles());
        StartCoroutine(DestroySpell());
    }

    void Update() {
        if (direction != null) {
            var d = direction.normalized * speed * Time.deltaTime;
            transform.position = transform.position + d;   
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
        lightningPs.Stop();
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
        lightningPs.Stop();
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
            lightningPs.Stop();
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
