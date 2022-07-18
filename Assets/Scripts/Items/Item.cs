using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    private float range = 2f;
    private GameObject leader;
    private Vector3 playerPos;
    
    bool pickedUp = false;

    private void Update() {
        if (pickedUp) {
            Destroy(gameObject);
            return;
        }

        leader = GameObject.FindGameObjectWithTag("PlayerPartyLeader");
        float dist = Vector3.Distance(leader.transform.position, transform.position);
        if (dist < range) {
            var step =  7 * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(transform.position, leader.transform.position, step);
        }

        if (dist <= 0.05) {
            var handle = leader.GetComponent<CharacterInteractionHandler>();
            handle.AddToInventory(gameObject.name);
            pickedUp = true;
        }
    }
}
