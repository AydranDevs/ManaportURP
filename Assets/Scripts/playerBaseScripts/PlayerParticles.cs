using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour {
    private PlayerMovement playerMovement;
    private Spindash spindash;

    public Transform parent;

    // prefab references
    public GameObject pfDashPoof;
    public GameObject pfDashDust;
    public GameObject pfRunDust;
    public GameObject pfSpinDashStars;
    public GameObject pfPushSweat;

    // gameObjects to be instantiated
    private GameObject dashPoofParticles;
    private GameObject dashDustParticles;
    private GameObject runDustParticles;
    private GameObject spinDashParticles;
    private GameObject pushSweatParticles;

    private void Start() {
        playerMovement = GetComponent<PlayerMovement>();
        spindash = GetComponentInChildren<Spindash>();

        playerMovement.OnDashStart += SummonParticles_OnDashStart;
        playerMovement.OnDashEnd += DestroyParticles_OnDashEnd;
        playerMovement.OnRunStart += SummonParticles_OnRunStart;
        playerMovement.OnRunEnd += DestroyParticles_OnRunEnd;
        playerMovement.OnPushStart += SummonParticles_OnPushStart;
        playerMovement.OnPushEnd += DestroyParticles_OnPushEnd;

        spindash.OnSpinDashStart += SummonParticles_OnSpinDashStart;
        spindash.OnSpinDashEnd += DestroyParticles_OnSpinDashEnd;
        
    }

    public void SummonParticles_OnDashStart(object sender, PlayerMovement.OnDashStartEventArgs e) {
        dashPoofParticles = Instantiate(pfDashPoof, GetPosition(), Quaternion.identity, parent);
        dashDustParticles = Instantiate(pfDashDust, GetPosition() - new Vector3(0, 1, 0), Quaternion.identity, parent);
    }

    public void DestroyParticles_OnDashEnd(object sender, PlayerMovement.OnDashEndEventArgs e) {
        if (dashPoofParticles != null) {
            ParticleSystem dashPoofParticleSystem = dashPoofParticles.GetComponentInChildren<ParticleSystem>();
            dashPoofParticleSystem.Stop();
        }
        if (dashDustParticles != null) { 
            ParticleSystem dashDustParticleSystem = dashDustParticles.GetComponentInChildren<ParticleSystem>();
            dashDustParticleSystem.Stop();
        }

        Destroy(dashPoofParticles, 2f);
        Destroy(dashDustParticles, 2f);
    }

    public void SummonParticles_OnRunStart(object sender, PlayerMovement.OnRunStartEventArgs e) {
        runDustParticles = Instantiate(pfRunDust, GetPosition(), Quaternion.identity, parent);
    }

    public void DestroyParticles_OnRunEnd(object sender, PlayerMovement.OnRunEndEventArgs e) {
        if (runDustParticles != null) {
            ParticleSystem runDustParticleSystem = runDustParticles.GetComponentInChildren<ParticleSystem>();
            runDustParticleSystem.Stop();
        }

        Destroy(runDustParticles, 2f);
    }

    public void SummonParticles_OnPushStart(object sender, PlayerMovement.OnPushStartEventArgs e) {
        pushSweatParticles = Instantiate(pfPushSweat, GetPosition() + new Vector3(0, 2, 0), Quaternion.identity, parent);
    }

    public void DestroyParticles_OnPushEnd(object sender, PlayerMovement.OnPushEndEventArgs e) {
        if (pushSweatParticles != null) {
            ParticleSystem pushSweatParticleSystem = pushSweatParticles.GetComponentInChildren<ParticleSystem>();
            pushSweatParticleSystem.Stop();
        }

        Destroy(pushSweatParticles, 2f);
    }

    public void SummonParticles_OnSpinDashStart(object sender, Spindash.OnSpinDashStartEventArgs e) {
        spinDashParticles = Instantiate(pfSpinDashStars, GetPosition() - new Vector3(0, 1, 0), Quaternion.identity, parent);
    }

    public void DestroyParticles_OnSpinDashEnd(object sender, Spindash.OnSpinDashEndEventArgs e) {
        if (spinDashParticles != null) {
            GameObject ps = spinDashParticles.gameObject.transform.GetChild(0).gameObject;
            GameObject ps2 = spinDashParticles.gameObject.transform.GetChild(1).gameObject;

            ParticleSystem spinDashParticleSystem = ps.GetComponent<ParticleSystem>();
            ParticleSystem spinDashParticleSystem2 = ps2.GetComponent<ParticleSystem>();
            spinDashParticleSystem.Stop();
            spinDashParticleSystem2.Stop();
        }
        Destroy(spinDashParticles, 2f);
    }

    public Vector3 GetPosition() {
        return transform.position;
    }
}
