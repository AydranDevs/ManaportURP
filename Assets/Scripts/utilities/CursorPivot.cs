using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorPivot : MonoBehaviour {
    private GameObject player;
    [HideInInspector]
    public Vector3 difference;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    
    void FixedUpdate() {
        
        difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
 
        difference.Normalize();
 
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
 
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
    }
}
