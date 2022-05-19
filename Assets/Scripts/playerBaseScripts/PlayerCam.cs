using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float speed = 1f;
    public Transform target;
    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector2 pos = transform.position;
            Vector2 targetpos = target.position;

            Vector3 slerped = Vector3.Slerp(pos, targetpos, 100 * speed);

            slerped.z = transform.position.z;

            transform.position = slerped;

            // mouse position shit but with calculations this time

            // Vector3 mouse = cam.ScreenToWorldPoint(Input.mousePosition);
            // mouse.z = 0;

            // Vector3 betweenPoint = (mouse.x + target.x) / 2;

            // Debug.Log(mouse);
        }
    }
}
