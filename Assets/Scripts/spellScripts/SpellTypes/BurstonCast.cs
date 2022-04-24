using UnityEngine;

public class BurstonCast : Spell {
    public GameObject burston;
    public GameObject pyroBurston;
    public GameObject cryoBurston;
    public GameObject toxiBurston;
    public GameObject voltBurston;

    public float speed;
    public float lifetime;

    private void Start() {
        cooldownTime = cooldown;
    }

    public override void Cast(Vector2 direction, string element) {
        if (!coolingDown) {
            if (element == "pyro") {
                if (pyroBurston) {
                    GameObject go = Instantiate(pyroBurston, transform.position, new Quaternion());
                    var orb = go.GetComponent<PyroBurston>();
                    orb.direction = direction;
                    orb.lifetime = lifetime;
                    speed = orb.speed;
                    go.transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
                }
            }else if (element == "cryo") {
                if (cryoBurston) {
                    GameObject go = Instantiate(cryoBurston, transform.position, new Quaternion());
                    var orb = go.GetComponent<CryoBurston>();
                    orb.direction = direction;
                    orb.lifetime = lifetime;
                    speed = orb.speed;
                    go.transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
                }
            }else if (element == "toxi") {
                if (toxiBurston) {
                    GameObject go = Instantiate(toxiBurston, transform.position, new Quaternion());
                    var orb = go.GetComponent<ToxiBurston>();
                    orb.direction = direction;
                    orb.lifetime = lifetime;
                    speed = orb.speed;
                    go.transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
                }
            }else if (element == "volt") {
                if (voltBurston) {
                    GameObject go = Instantiate(voltBurston, transform.position, new Quaternion());
                    var orb = go.GetComponent<VoltBurston>();
                    orb.direction = direction;
                    orb.lifetime = lifetime;
                    speed = orb.speed;
                    go.transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
                }
            }else {
                if (burston) {
                    GameObject go = Instantiate(burston, transform.position, new Quaternion());
                    var orb = go.GetComponent<Burston>();
                    orb.damage = damage;
                    orb.direction = direction;
                    orb.lifetime = lifetime;
                    speed = orb.speed;
                    go.transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
                }
            }
            
            coolingDown = true;
        }
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
}
