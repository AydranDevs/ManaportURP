using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour {
    private PlayerMovement playerMovement;

    public Transform parent;

    // prefab references
    public GameObject pfDashPoof;
    public GameObject pfDashDust;
    public GameObject pfRunDust;

    // gameObjects to be instantiated
    private GameObject dashPoofParticles;
    private GameObject dashDustParticles;
    private GameObject runDustParticles;

    private void Start() {
        playerMovement = GetComponent<PlayerMovement>();

        playerMovement.OnDashStart += SummonParticles_OnDashStart;
        playerMovement.OnDashEnd += DestroyParticles_OnDashEnd;
        playerMovement.OnRunStart += SummonParticles_OnRunStart;
        playerMovement.OnRunEnd += DestroyParticles_OnRunEnd;
    }

    public void SummonParticles_OnDashStart(object sender, PlayerMovement.OnDashStartEventArgs e) {
        dashPoofParticles = Instantiate(pfDashPoof, GetPosition(), Quaternion.identity, parent);
        dashDustParticles = Instantiate(pfDashDust, GetPosition(), Quaternion.identity, parent);
    }

    public void DestroyParticles_OnDashEnd(object sender, PlayerMovement.OnDashEndEventArgs e) {
        Destroy(dashPoofParticles);
        Destroy(dashDustParticles);
    }

    public void SummonParticles_OnRunStart(object sender, PlayerMovement.OnRunStartEventArgs e) {
        runDustParticles = Instantiate(pfRunDust, GetPosition(), Quaternion.identity, parent);
    }

    public void DestroyParticles_OnRunEnd(object sender, PlayerMovement.OnRunEndEventArgs e) {
        Destroy(runDustParticles);
    }

    public Vector3 GetPosition() {
        return transform.position;
    }
}
