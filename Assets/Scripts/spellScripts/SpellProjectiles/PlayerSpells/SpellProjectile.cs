using UnityEngine;

public class SpellProjectile : MonoBehaviour {
    public GameObject thisSpellProjectile;
    
    public Rigidbody2D rigidbody;
    public Collider2D collider;
    public ParticleSystem corePs;
    public ParticleSystem trailPs; 

    public Vector3 direction;
    public float speed = 10;
    public float lifetime = 3;
    public float damage;
}