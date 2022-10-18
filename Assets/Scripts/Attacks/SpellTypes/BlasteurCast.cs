using UnityEngine;

public class BlasteurCast : Spell {
    public GameObject blasteur;
    public GameObject pyroBlasteur;
    public GameObject cryoBlasteur;
    public GameObject toxiBlasteur;
    public GameObject voltBlasteur;

    public float speed;
    public float lifetime;

    private void Start() {
        spellId = "blasteur";
        cooldownTime = cooldown;
    }

    public override void Cast(Vector2 direction, string element) {
        if (!coolingDown) {
            if (element == "pyro") {
                if (pyroBlasteur) {
                    var primaryRotation = Quaternion.FromToRotation(Vector2.up, direction);
                    var leftRotation = primaryRotation * Quaternion.Euler(0, 0, -20);
                    var rightRotation = primaryRotation * Quaternion.Euler(0, 0, 20);

                    var leftDirection = rotate(direction, Mathf.Deg2Rad * -20);
                    var rightDirection = rotate(direction, Mathf.Deg2Rad * 20);

                    CreatePyroBlasteur(direction, primaryRotation);
                    CreatePyroBlasteur(leftDirection, leftRotation);
                    CreatePyroBlasteur(rightDirection, rightRotation);
                }
            }else if (element == "cryo") {
                if (cryoBlasteur) {
                    var primaryRotation = Quaternion.FromToRotation(Vector2.up, direction);
                    var leftRotation = primaryRotation * Quaternion.Euler(0, 0, -20);
                    var rightRotation = primaryRotation * Quaternion.Euler(0, 0, 20);

                    var leftDirection = rotate(direction, Mathf.Deg2Rad * -20);
                    var rightDirection = rotate(direction, Mathf.Deg2Rad * 20);

                    CreateCryoBlasteur(direction, primaryRotation);
                    CreateCryoBlasteur(leftDirection, leftRotation);
                    CreateCryoBlasteur(rightDirection, rightRotation);
                }
            }else if (element == "toxi") {
                if (toxiBlasteur) {
                    var primaryRotation = Quaternion.FromToRotation(Vector2.up, direction);
                    var leftRotation = primaryRotation * Quaternion.Euler(0, 0, -20);
                    var rightRotation = primaryRotation * Quaternion.Euler(0, 0, 20);

                    var leftDirection = rotate(direction, Mathf.Deg2Rad * -20);
                    var rightDirection = rotate(direction, Mathf.Deg2Rad * 20);

                    CreateToxiBlasteur(direction, primaryRotation);
                    CreateToxiBlasteur(leftDirection, leftRotation);
                    CreateToxiBlasteur(rightDirection, rightRotation);
                }
            }else if (element == "volt") {
                if (voltBlasteur) {
                    var primaryRotation = Quaternion.FromToRotation(Vector2.up, direction);
                    var leftRotation = primaryRotation * Quaternion.Euler(0, 0, -20);
                    var rightRotation = primaryRotation * Quaternion.Euler(0, 0, 20);

                    var leftDirection = rotate(direction, Mathf.Deg2Rad * -20);
                    var rightDirection = rotate(direction, Mathf.Deg2Rad * 20);

                    CreateVoltBlasteur(direction, primaryRotation);
                    CreateVoltBlasteur(leftDirection, leftRotation);
                    CreateVoltBlasteur(rightDirection, rightRotation);
                }
            }else {
                if (blasteur) {
                    var primaryRotation = Quaternion.FromToRotation(Vector2.up, direction);
                    var leftRotation = primaryRotation * Quaternion.Euler(0, 0, -20);
                    var rightRotation = primaryRotation * Quaternion.Euler(0, 0, 20);

                    var leftDirection = rotate(direction, Mathf.Deg2Rad * -20);
                    var rightDirection = rotate(direction, Mathf.Deg2Rad * 20);

                    CreateBlasteur(direction, primaryRotation);
                    CreateBlasteur(leftDirection, leftRotation);
                    CreateBlasteur(rightDirection, rightRotation);
                }
            }
            coolingDown = true;
        }
    }

    private void CreatePyroBlasteur(Vector2 direction, Quaternion rotation) {
        GameObject go = Instantiate(pyroBlasteur, transform.position, rotation);
        var orb = go.GetComponent<PyroBlasteur>();
        orb.damage = damage;
        orb.direction = direction;
        orb.lifetime = lifetime;
        orb.speed = speed;
    }

    private void CreateCryoBlasteur(Vector2 direction, Quaternion rotation) {
        GameObject go = Instantiate(cryoBlasteur, transform.position, rotation);
        var orb = go.GetComponent<CryoBlasteur>();
        orb.damage = damage;
        orb.direction = direction;
        orb.lifetime = lifetime;
        orb.speed = speed;
    }

    private void CreateToxiBlasteur(Vector2 direction, Quaternion rotation) {
        GameObject go = Instantiate(toxiBlasteur, transform.position, rotation);
        var orb = go.GetComponent<ToxiBlasteur>();
        orb.damage = damage;
        orb.direction = direction;
        orb.lifetime = lifetime;
        orb.speed = speed;
    }

    private void CreateVoltBlasteur(Vector2 direction, Quaternion rotation) {
        GameObject go = Instantiate(voltBlasteur, transform.position, rotation);
        var orb = go.GetComponent<VoltBlasteur>();
        orb.damage = damage;
        orb.direction = direction;
        orb.lifetime = lifetime;
        orb.speed = speed;
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
