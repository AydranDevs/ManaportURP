using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightspeed : MonoBehaviour {
    private Laurie laurie;
    private Player player;
    private PlayerAbilities playerAbilities;
    private Rigidbody2D rb;
    private PlayerMovement playerMovement;

    public float speed; // default 30
    public float time;
    public float dist;

    [SerializeField]
    public Vector3 dashTarget;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        laurie = GameObject.FindGameObjectWithTag("Player").GetComponent<Laurie>();
        playerAbilities = GetComponent<PlayerAbilities>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

        speed = 50f;
        time = 0.1f;
    }

    private void Update() {
        if (player.auxilaryType == AuxilaryMovementType.BlinkDash && player.ability == AbilityState.AuxilaryMovement) {
            float range = laurie.blinkdashDist;
            dashTarget = player.transform.position + (Vector3)playerMovement.reconstructedMovement * range;

            time -= Time.deltaTime;

            float step =  speed * Time.deltaTime; // calculate distance to move
            player.transform.position = Vector3.MoveTowards(player.transform.position, dashTarget, step);

            // reset all timers and player ability state
            if (time <= 0f) {
                player.ability = AbilityState.None;
                player.movementType = MovementState.Idle;
                
                playerAbilities.abilitiesAvailable = false;
                playerAbilities.abilityCooldown = laurie.abilityCooldownLimit;
                time = 0.1f;

                // spinDashParActive = false;
            }
        }
    }
}
