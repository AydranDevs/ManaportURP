using UnityEngine;

public class BlasteurCast : Spell {
    public GameObject blasteur;
    public float speed;
    public float lifetime;

    private void Start() {
        cooldownTime = cooldown;
    }

    public override void Cast(Vector2 direction, string element) {
        if (!coolingDown) {
            if (blasteur) {
                // PlayerMain.INSTANCE.playerAnimation.PlaySpellcast();

                var primaryRotation = Quaternion.FromToRotation(Vector2.up, direction);
                var leftRotation = primaryRotation * Quaternion.Euler(0, 0, -20);
                var rightRotation = primaryRotation * Quaternion.Euler(0, 0, 20);

                var leftDirection = rotate(direction, Mathf.Deg2Rad * -20);
                var rightDirection = rotate(direction, Mathf.Deg2Rad * 20);

                CreateBlasteur(direction, primaryRotation);
                CreateBlasteur(leftDirection, leftRotation);
                CreateBlasteur(rightDirection, rightRotation);
            }

            coolingDown = true;
        }
    }

    private void CreateBlasteur(Vector2 direction, Quaternion rotation) {
        GameObject go = Instantiate(blasteur, transform.position, rotation);
        var orb = go.GetComponent<Blasteur>();
        orb.damage = damage;
        orb.direction = direction;
        orb.lifetime = lifetime;
        orb.speed = speed;
    }

    private void Update() {
        if (coolingDown) {
            cooldownTime = cooldownTime - Time.deltaTime;
            if (cooldownTime <= 0f) {
                cooldownTime = cooldown;
                coolingDown = false;
            }
        }
    }

    public static Vector2 rotate(Vector2 v, float delta) {
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
        );
    }
}
