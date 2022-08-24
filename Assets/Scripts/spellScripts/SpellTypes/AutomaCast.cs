using System.Collections;
using UnityEngine;

public class AutomaCast : Spell {
    public GameObject automa;
    public GameObject pyroAutoma;
    public GameObject cryoAutoma;
    public GameObject toxiAutoma;
    public GameObject voltAutoma;

    public float speed;
    public float lifetime;

    private string thisElement;

    private void Start() {
        spellId = "automa";
        cooldownTime = cooldown;
    }

    public override void Cast(Vector2 direction, string element) {
        if (!coolingDown) {
            thisElement = element;

            if (element == "pyro") {
                if (pyroAutoma) {
                    var primaryRotation = Quaternion.FromToRotation(Vector2.up, direction);
                    var leftRotation = primaryRotation * Quaternion.Euler(0, 0, -2);
                    var rightRotation = primaryRotation * Quaternion.Euler(0, 0, 2);

                    var leftDirection = rotate(direction, Mathf.Deg2Rad * -2);
                    var rightDirection = rotate(direction, Mathf.Deg2Rad * 2);

                    StartCoroutine(CreatePyroAutoma1(pyroAutoma, direction, primaryRotation));
                    StartCoroutine(CreatePyroAutoma2(pyroAutoma, leftDirection, leftRotation));
                    StartCoroutine(CreatePyroAutoma3(pyroAutoma, rightDirection, rightRotation));
                }
            }else if (element == "cryo") {
                if (cryoAutoma) {
                    var primaryRotation = Quaternion.FromToRotation(Vector2.up, direction);
                    var leftRotation = primaryRotation * Quaternion.Euler(0, 0, -2);
                    var rightRotation = primaryRotation * Quaternion.Euler(0, 0, 2);

                    var leftDirection = rotate(direction, Mathf.Deg2Rad * -2);
                    var rightDirection = rotate(direction, Mathf.Deg2Rad * 2);
                
                    StartCoroutine(CreateCryoAutoma1(cryoAutoma, direction, primaryRotation));
                    StartCoroutine(CreateCryoAutoma2(cryoAutoma, leftDirection, leftRotation));
                    StartCoroutine(CreateCryoAutoma3(cryoAutoma, rightDirection, rightRotation));
                }
            }else if (element == "toxi") {
                if (toxiAutoma) {
                    var primaryRotation = Quaternion.FromToRotation(Vector2.up, direction);
                    var leftRotation = primaryRotation * Quaternion.Euler(0, 0, -2);
                    var rightRotation = primaryRotation * Quaternion.Euler(0, 0, 2);

                    var leftDirection = rotate(direction, Mathf.Deg2Rad * -2);
                    var rightDirection = rotate(direction, Mathf.Deg2Rad * 2);
                
                    StartCoroutine(CreateToxiAutoma1(toxiAutoma, direction, primaryRotation));
                    StartCoroutine(CreateToxiAutoma2(toxiAutoma, leftDirection, leftRotation));
                    StartCoroutine(CreateToxiAutoma3(toxiAutoma, rightDirection, rightRotation));
                }
            }else if (element == "volt") {
                if (voltAutoma) {
                    var primaryRotation = Quaternion.FromToRotation(Vector2.up, direction);
                    var leftRotation = primaryRotation * Quaternion.Euler(0, 0, -2);
                    var rightRotation = primaryRotation * Quaternion.Euler(0, 0, 2);

                    var leftDirection = rotate(direction, Mathf.Deg2Rad * -2);
                    var rightDirection = rotate(direction, Mathf.Deg2Rad * 2);
                
                    StartCoroutine(CreateToxiAutoma1(voltAutoma, direction, primaryRotation));
                    StartCoroutine(CreateToxiAutoma2(voltAutoma, leftDirection, leftRotation));
                    StartCoroutine(CreateToxiAutoma3(voltAutoma, rightDirection, rightRotation));
                }
            }else {
                if (automa) {
                    var primaryRotation = Quaternion.FromToRotation(Vector2.up, direction);
                    var leftRotation = primaryRotation * Quaternion.Euler(0, 0, -2);
                    var rightRotation = primaryRotation * Quaternion.Euler(0, 0, 2);

                    var leftDirection = rotate(direction, Mathf.Deg2Rad * -2);
                    var rightDirection = rotate(direction, Mathf.Deg2Rad * 2);
                    
                    StartCoroutine(CreateAutoma1(automa, direction, primaryRotation));
                    StartCoroutine(CreateAutoma2(automa, leftDirection, leftRotation));
                    StartCoroutine(CreateAutoma3(automa, rightDirection, rightRotation));
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

    IEnumerator CreateAutoma1(GameObject spell, Vector2 direction, Quaternion rotation) {
        yield return new WaitForSeconds(0);
        
        GameObject go = Instantiate(spell, transform.position, new Quaternion());
        
        var orb = go.GetComponent<Automa>();
        orb.damage = damage;
        orb.direction = direction;
        orb.lifetime = lifetime;
        speed = orb.speed;
        go.transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
    }
    IEnumerator CreateAutoma2(GameObject spell, Vector2 direction, Quaternion rotation) {
        yield return new WaitForSeconds(0.1f);
        
        GameObject go = Instantiate(spell, transform.position, new Quaternion());
        
        var orb = go.GetComponent<Automa>();
        orb.damage = damage;
        orb.direction = direction;
        orb.lifetime = lifetime;
        speed = orb.speed;
        go.transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
    }
    IEnumerator CreateAutoma3(GameObject spell, Vector2 direction, Quaternion rotation) {
        yield return new WaitForSeconds(0.2f);
        
        GameObject go = Instantiate(spell, transform.position, new Quaternion());
        
        var orb = go.GetComponent<Automa>();
        orb.damage = damage;
        orb.direction = direction;
        orb.lifetime = lifetime;
        speed = orb.speed;
        go.transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
    }

    IEnumerator CreatePyroAutoma1(GameObject spell, Vector2 direction, Quaternion rotation) {
        yield return new WaitForSeconds(0);
        
        GameObject go = Instantiate(spell, transform.position, new Quaternion());
        
        var orb = go.GetComponent<PyroAutoma>();
        orb.damage = damage;
        orb.direction = direction;
        orb.lifetime = lifetime;
        speed = orb.speed;
        go.transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
    }
    IEnumerator CreatePyroAutoma2(GameObject spell, Vector2 direction, Quaternion rotation) {
        yield return new WaitForSeconds(0.1f);
        
        GameObject go = Instantiate(spell, transform.position, new Quaternion());
        
        var orb = go.GetComponent<PyroAutoma>();
        orb.damage = damage;
        orb.direction = direction;
        orb.lifetime = lifetime;
        speed = orb.speed;
        go.transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
    }
    IEnumerator CreatePyroAutoma3(GameObject spell, Vector2 direction, Quaternion rotation) {
        yield return new WaitForSeconds(0.2f);
        
        GameObject go = Instantiate(spell, transform.position, new Quaternion());
        
        var orb = go.GetComponent<PyroAutoma>();
        orb.damage = damage;
        orb.direction = direction;
        orb.lifetime = lifetime;
        speed = orb.speed;
        go.transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
    }

    IEnumerator CreateCryoAutoma1(GameObject spell, Vector2 direction, Quaternion rotation) {
        yield return new WaitForSeconds(0);
        
        GameObject go = Instantiate(spell, transform.position, new Quaternion());
        
        var orb = go.GetComponent<CryoAutoma>();
        orb.damage = damage;
        orb.direction = direction;
        orb.lifetime = lifetime;
        speed = orb.speed;
        go.transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
    }
    IEnumerator CreateCryoAutoma2(GameObject spell, Vector2 direction, Quaternion rotation) {
        yield return new WaitForSeconds(0.1f);
        
        GameObject go = Instantiate(spell, transform.position, new Quaternion());
        
        var orb = go.GetComponent<CryoAutoma>();
        orb.damage = damage;
        orb.direction = direction;
        orb.lifetime = lifetime;
        speed = orb.speed;
        go.transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
    }
    IEnumerator CreateCryoAutoma3(GameObject spell, Vector2 direction, Quaternion rotation) {
        yield return new WaitForSeconds(0.2f);
        
        GameObject go = Instantiate(spell, transform.position, new Quaternion());
        
        var orb = go.GetComponent<CryoAutoma>();
        orb.damage = damage;
        orb.direction = direction;
        orb.lifetime = lifetime;
        speed = orb.speed;
        go.transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
    }

    IEnumerator CreateToxiAutoma1(GameObject spell, Vector2 direction, Quaternion rotation) {
        yield return new WaitForSeconds(0);
        
        GameObject go = Instantiate(spell, transform.position, new Quaternion());
        
        var orb = go.GetComponent<ToxiAutoma>();
        orb.damage = damage;
        orb.direction = direction;
        orb.lifetime = lifetime;
        speed = orb.speed;
        go.transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
    }
    IEnumerator CreateToxiAutoma2(GameObject spell, Vector2 direction, Quaternion rotation) {
        yield return new WaitForSeconds(0.1f);
        
        GameObject go = Instantiate(spell, transform.position, new Quaternion());
        
        var orb = go.GetComponent<ToxiAutoma>();
        orb.damage = damage;
        orb.direction = direction;
        orb.lifetime = lifetime;
        speed = orb.speed;
        go.transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
    }
    IEnumerator CreateToxiAutoma3(GameObject spell, Vector2 direction, Quaternion rotation) {
        yield return new WaitForSeconds(0.2f);
        
        GameObject go = Instantiate(spell, transform.position, new Quaternion());
        
        var orb = go.GetComponent<ToxiAutoma>();
        orb.damage = damage;
        orb.direction = direction;
        orb.lifetime = lifetime;
        speed = orb.speed;
        go.transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
    }

    IEnumerator CreateVoltAutoma1(GameObject spell, Vector2 direction, Quaternion rotation) {
        yield return new WaitForSeconds(0);
        
        GameObject go = Instantiate(spell, transform.position, new Quaternion());
        
        var orb = go.GetComponent<VoltAutoma>();
        orb.damage = damage;
        orb.direction = direction;
        orb.lifetime = lifetime;
        speed = orb.speed;
        go.transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
    }
    IEnumerator CreateVoltAutoma2(GameObject spell, Vector2 direction, Quaternion rotation) {
        yield return new WaitForSeconds(0.1f);
        
        GameObject go = Instantiate(spell, transform.position, new Quaternion());
        
        var orb = go.GetComponent<VoltAutoma>();
        orb.damage = damage;
        orb.direction = direction;
        orb.lifetime = lifetime;
        speed = orb.speed;
        go.transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
    }
    IEnumerator CreateVoltAutoma3(GameObject spell, Vector2 direction, Quaternion rotation) {
        yield return new WaitForSeconds(0.2f);
        
        GameObject go = Instantiate(spell, transform.position, new Quaternion());
        
        var orb = go.GetComponent<VoltAutoma>();
        orb.damage = damage;
        orb.direction = direction;
        orb.lifetime = lifetime;
        speed = orb.speed;
        go.transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
    }

    public static Vector2 rotate(Vector2 v, float delta) {
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
        );
    }

    
}
