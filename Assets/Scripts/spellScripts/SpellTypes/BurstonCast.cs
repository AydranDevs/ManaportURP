using UnityEngine;

public class BurstonCast : Spell {
    public GameObject burston;
    public float speed;
    public float lifetime;

    public override void Cast(Vector2 direction, string element) {
        if (burston) {
            GameObject go = Instantiate(burston, transform.position, new Quaternion());
            var orb = go.GetComponent<Burston>();
            orb.direction = direction;
            orb.lifetime = lifetime;
            orb.speed = speed;
            go.transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
        }
    }
}
