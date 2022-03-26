using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightspeed : MonoBehaviour {
    private Laurie laurie;
    private Player player;
    private PlayerAbilities playerAbilities;
    private Rigidbody2D rb;
    private PlayerMovement playerMovement;

    // public float range; // default 5
    public float speed; // default 5
    public float angle;
    public float dist;

    [SerializeField]
    public Vector3 dashTarget;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        laurie = GameObject.FindGameObjectWithTag("Player").GetComponent<Laurie>();
        playerAbilities = GetComponent<PlayerAbilities>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (player.auxilaryType == AuxilaryMovementType.BlinkDash && player.ability == AbilityState.AuxilaryMovement) {
            float range = laurie.blinkdashDist;
            
            dashTarget = player.transform.position + (Vector3)playerMovement.reconstructedMovement * range;

            player.transform.position = dashTarget;

            playerAbilities.abilitiesAvailable = false;
            playerAbilities.abilityCooldown = laurie.abilityCooldownLimit;
        }
    }
}
