using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spindash : MonoBehaviour {
    private Player player;
    private Laurie laurie;
    private PlayerAbilities playerAbilities;
    private Rigidbody2D rb;
    private PlayerMovement playerMovement;

    // private ParticleSystem particleSystem;

    // public float range; // default 5
    public float speed; // default 5
    public float time;

    [SerializeField]
    public Vector3 dashTarget;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        laurie = GameObject.FindGameObjectWithTag("Player").GetComponent<Laurie>();
        playerAbilities = GetComponent<PlayerAbilities>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        // particleSystem = GameObject.FindGameObjectWithTag("SpindustPS").GetComponent<ParticleSystem>();
        time = laurie.spindashDist * 0.1f;
    }

    private void Update() {
        if (player.auxilaryType == AuxilaryMovementType.Spindash && player.ability == AbilityState.AuxilaryMovement) {
            float range = laurie.spindashDist;
            dashTarget = player.transform.position + (Vector3)playerMovement.reconstructedMovement * range;

            time -= Time.deltaTime;

            float step =  speed * Time.deltaTime; // calculate distance to move
            player.transform.position = Vector3.MoveTowards(player.transform.position, dashTarget, step);

            // reset all timers and player ability state
            if (time <= 0f) {
                playerAbilities.abilitiesAvailable = false;
                playerAbilities.abilityCooldown = laurie.abilityCooldownLimit;
                time = laurie.spindashDist * 0.1f;
            }
        }
    }

    
}
