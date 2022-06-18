using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyCam : MonoBehaviour {
    public float speed = 1f;
    public Transform target;
    private Vector3 _target;

    Camera cam;

    bool transitionOver = false;

    public float transitionDuration = 2.5f;

    void Start() {
        cam = GetComponent<Camera>();

        StartCoroutine(Transition());
    }

    void FixedUpdate() {
        _target = new Vector3(target.position.x, target.position.y, transform.position.z);

        if (transitionOver) {
            
            Vector2 pos = transform.position;
            Vector2 targetpos = target.position;

            Vector3 slerped = Vector3.Slerp(pos, targetpos, 100 * speed);

            slerped.z = transform.position.z;

            transform.position = slerped;
        }
    }

    public void PartyLeaderChanged() {
        if (target != null) {
            if (transitionOver) {
                transitionOver = false;
                StartCoroutine(Transition());
            }
        }
    }

    IEnumerator Transition() {
        float t = 0f;
        Vector3 startPos = transform.position;
        while (t < 1f) {
            t += Time.deltaTime * (Time.timeScale / transitionDuration);
            transform.position = Vector3.Lerp(startPos, _target, t);
            yield return 0;
        }
        transitionOver = true;
    }
}
