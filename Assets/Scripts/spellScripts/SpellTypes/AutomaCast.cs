using System.Collections;
using UnityEngine;

public class AutomaCast : Spell {
    public GameObject automa;
    public float speed;
    public float lifetime;

    // private Quaternion primaryRotation;
    // private Quaternion leftRotation;
    // private Quaternion rightRotation;
    
    // private Vector2 leftDirection;
    // private Vector2 rightDirection;
    // private Vector2 direction;

    private float time;
    private float timeMax = 2f;

    bool firstShotActive = true;
    bool secondShotActive = true;
    bool thirdShotActive = true;

    public override void Cast(Vector2 direction, string element) {
        if (automa) {
            var primaryRotation = Quaternion.FromToRotation(Vector2.up, direction);
            var leftRotation = primaryRotation * Quaternion.Euler(0, 0, -2);
            var rightRotation = primaryRotation * Quaternion.Euler(0, 0, 2);

            var leftDirection = rotate(direction, Mathf.Deg2Rad * -2);
            var rightDirection = rotate(direction, Mathf.Deg2Rad * 2);

            time = timeMax;
            
            StartCoroutine(CreateAutoma1(direction, primaryRotation));
            StartCoroutine(CreateAutoma2(leftDirection, leftRotation));
            StartCoroutine(CreateAutoma3(rightDirection, rightRotation));
            
        }
    }

    // private void CreateAutoma(Vector2 direction, Quaternion rotation) {
    //     GameObject go = Instantiate(automa, transform.position, new Quaternion());
    //     var orb = go.GetComponent<Automa>();
    //     orb.direction = direction;
    //     orb.lifetime = lifetime;
    //     orb.speed = speed;
    //     go.transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
    // }

    private void Update() {
        if (automa) {
            if (firstShotActive) {
                time = time - Time.deltaTime;

                if (time <= 1f) {
                    secondShotActive = true;
                }
                if (time <= 0f) {
                    thirdShotActive = true;
                }
            }
        }
    }

    IEnumerator CreateAutoma1(Vector2 direction, Quaternion rotation) {
        yield return new WaitForSeconds(0);
        
        GameObject go = Instantiate(automa, transform.position, new Quaternion());
        var orb = go.GetComponent<Automa>();
        orb.direction = direction;
        orb.lifetime = lifetime;
        orb.speed = speed;
        go.transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
    }

    IEnumerator CreateAutoma2(Vector2 direction, Quaternion rotation) {
        yield return new WaitForSeconds(0.1f);
        
        GameObject go = Instantiate(automa, transform.position, new Quaternion());
        var orb = go.GetComponent<Automa>();
        orb.direction = direction;
        orb.lifetime = lifetime;
        orb.speed = speed;
        go.transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
    }

    IEnumerator CreateAutoma3(Vector2 direction, Quaternion rotation) {
        yield return new WaitForSeconds(0.2f);
        
        GameObject go = Instantiate(automa, transform.position, new Quaternion());
        var orb = go.GetComponent<Automa>();
        orb.direction = direction;
        orb.lifetime = lifetime;
        orb.speed = speed;
        go.transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
    }

    public static Vector2 rotate(Vector2 v, float delta) {
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
        );
    }

    
}
