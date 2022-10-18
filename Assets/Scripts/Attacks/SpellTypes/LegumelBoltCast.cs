using UnityEngine;

public class LegumelBoltCast : Spell {
    public GameObject legumelBolt;
    public float speed;
    public float lifetime;

    public override void Cast(Vector2 direction, string element) {
        if (legumelBolt) {
            GameObject go = Instantiate(legumelBolt, transform.position, new Quaternion());
            var orb = go.GetComponent<LegumelBolt>();
            orb.direction = direction;
            orb.lifetime = lifetime;
            orb.speed = speed;
            orb.damage = damage;
            go.transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
        }
    }
}
