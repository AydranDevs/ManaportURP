using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyroBurston : SpellProjectile {
    public GameObject myLight;

    public GameObject explosionPf;
    private GameObject explosion;

    private bool damageDealt = false;
    private bool hit = false;

    void Start() {
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider2D = GetComponent<CircleCollider2D>();
        thisSpellProjectile = this.gameObject;
        corePs = thisSpellProjectile.transform.GetChild(0).GetComponent<ParticleSystem>();
        trailPs = thisSpellProjectile.transform.GetChild(1).GetComponent<ParticleSystem>();

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
        myCollider2D.enabled = false;

        StartCoroutine(DestroySpellImpact());
        Destroy(myLight);
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
        myCollider2D.enabled = false;

        StartCoroutine(DestroySpellImpact());
        Destroy(myLight);
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
            myCollider2D.enabled = false;
            Destroy(myLight);
            corePs.Stop();
            trailPs.Stop();
        }
    }

    IEnumerator DestroySpell() {
        yield return new WaitForSeconds(lifetime + 4);
        if (gameObject != null) {
            Destroy(gameObject);
        }
    }

    IEnumerator DestroySpellImpact() {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
