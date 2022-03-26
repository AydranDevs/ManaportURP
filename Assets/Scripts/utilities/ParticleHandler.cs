using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHandler : MonoBehaviour {
    [HideInInspector]
    public Legumel legumel;
    [HideInInspector]
    public Ribbuki ribbuki;
    [HideInInspector]
    public Molera molera;

    public Transform parent;

    [SerializeField]
    private GameObject pfDust;
    [SerializeField]
    private GameObject pfFire;
    [SerializeField]
    private GameObject pfIce;
    [SerializeField]
    private GameObject pfLightning;
    [SerializeField]
    private GameObject pfToxic;
    [SerializeField]
    private GameObject pfCrit;

    // Particle prefabs
    GameObject critParticles;
    GameObject arcaneParticles;
    GameObject pyroParticles;
    GameObject cryoParticles;
    GameObject boltParticles;
    GameObject toxiParticles;

    private void Awake() {
        legumel = GetComponent<Legumel>();
        ribbuki = GetComponent<Ribbuki>();
        molera = GetComponent<Molera>();

        parent = this.gameObject.transform;
        
        
        if (legumel != null) { 
            legumel.OnLegumelSummonParticles += OnLegumelSummonParticles_HandleParticles;
            legumel.OnLegumelDestroyParticles += OnLegumelDestroyParticles_HandleParticles;
        }

        if (ribbuki != null) {
            ribbuki.OnRibbukiSummonParticles += OnRibbukiSummonParticles_HandleParticles;
            ribbuki.OnRibbukiDestroyParticles += OnRibbukiDestroyParticles_HandleParticles; 
        }

        if (molera != null) {
            molera.OnMoleraSummonParticles += OnMoleraSummonParticles_HandleParticles;
            molera.OnMoleraDestroyParticles += OnMoleraDestroyParticles_HandleParticles;
        }
    }

    // Legumel Particles

    private void OnLegumelSummonParticles_HandleParticles(object sender, Legumel.OnLegumelSummonParticlesEventArgs e) {

        // crit
        if (e.particleType == "Crit") {
            critParticles = Instantiate(pfCrit, GetPosition(), Quaternion.identity, parent);
        }
        
        // arcane
        if (e.particleType == "Arcane") {
            arcaneParticles = Instantiate(pfDust, GetPosition(), Quaternion.identity, parent);
        }
        
        // elements
        if (e.particleType == "Pyro") {
            pyroParticles = Instantiate(pfFire, GetPosition(), new Quaternion(-1f, 0f, 0f, 1f), parent);
        }
        if (e.particleType == "Cryo") {
            cryoParticles = Instantiate(pfIce, GetPosition(), Quaternion.identity, parent);
        }
        if (e.particleType == "Bolt") {
            boltParticles = Instantiate(pfLightning, GetPosition(), Quaternion.identity, parent);
        }
        if (e.particleType == "Toxi") {
            toxiParticles = Instantiate(pfToxic, GetPosition(), Quaternion.identity, parent);
        }
    }

    private void OnLegumelDestroyParticles_HandleParticles(object sender, Legumel.OnLegumelDestroyParticlesEventArgs e) {
        
        if (e.particleType == "Crit") {
            Destroy(critParticles);
        }
        
        if (e.particleType == "Arcane") {
            Destroy(arcaneParticles);
        }
        
        if (e.particleType == "Pyro") {
            Destroy(pyroParticles);
        }
        if (e.particleType == "Cryo") {
            Destroy(cryoParticles);
        }
        if (e.particleType == "Bolt") {
            Destroy(boltParticles);
        }
        if (e.particleType == "Toxi") {
            Destroy(toxiParticles);
        }
    }

    // Ribbuki Particles

    private void OnRibbukiSummonParticles_HandleParticles(object sender, Ribbuki.OnRibbukiSummonParticlesEventArgs e) {

        // crit
        if (e.particleType == "Crit") {
            critParticles = Instantiate(pfCrit, GetPosition(), Quaternion.identity, parent);
        }
        
        // arcane
        if (e.particleType == "Arcane") {
            arcaneParticles = Instantiate(pfDust, GetPosition(), Quaternion.identity, parent);
        }
        
        // elements
        if (e.particleType == "Pyro") {
            pyroParticles = Instantiate(pfFire, GetPosition(), new Quaternion(-1f, 0f, 0f, 1f), parent);
        }
        if (e.particleType == "Cryo") {
            cryoParticles = Instantiate(pfIce, GetPosition(), Quaternion.identity, parent);
        }
        if (e.particleType == "Bolt") {
            boltParticles = Instantiate(pfLightning, GetPosition(), Quaternion.identity, parent);
        }
        if (e.particleType == "Toxi") {
            toxiParticles = Instantiate(pfToxic, GetPosition(), Quaternion.identity, parent);
        }
    }

    private void OnRibbukiDestroyParticles_HandleParticles(object sender, Ribbuki.OnRibbukiDestroyParticlesEventArgs e) {
        
        if (e.particleType == "Crit") {
            Destroy(critParticles);
        }
        
        if (e.particleType == "Arcane") {
            Destroy(arcaneParticles);
        }
        
        if (e.particleType == "Pyro") {
            Destroy(pyroParticles);
        }
        if (e.particleType == "Cryo") {
            Destroy(cryoParticles);
        }
        if (e.particleType == "Bolt") {
            Destroy(boltParticles);
        }
        if (e.particleType == "Toxi") {
            Destroy(toxiParticles);
        }
    }

    // Ribbuki Particles

    private void OnMoleraSummonParticles_HandleParticles(object sender, Molera.OnMoleraSummonParticlesEventArgs e) {

        // crit
        if (e.particleType == "Crit") {
            critParticles = Instantiate(pfCrit, GetPosition(), Quaternion.identity, parent);
        }
        
        // arcane
        if (e.particleType == "Arcane") {
            arcaneParticles = Instantiate(pfDust, GetPosition(), Quaternion.identity, parent);
        }
        
        // elements
        if (e.particleType == "Pyro") {
            pyroParticles = Instantiate(pfFire, GetPosition(), new Quaternion(-1f, 0f, 0f, 1f), parent);
        }
        if (e.particleType == "Cryo") {
            cryoParticles = Instantiate(pfIce, GetPosition(), Quaternion.identity, parent);
        }
        if (e.particleType == "Bolt") {
            boltParticles = Instantiate(pfLightning, GetPosition(), Quaternion.identity, parent);
        }
        if (e.particleType == "Toxi") {
            toxiParticles = Instantiate(pfToxic, GetPosition(), Quaternion.identity, parent);
        }
    }

    private void OnMoleraDestroyParticles_HandleParticles(object sender, Molera.OnMoleraDestroyParticlesEventArgs e) {
        
        if (e.particleType == "Crit") {
            Destroy(critParticles);
        }
        
        if (e.particleType == "Arcane") {
            Destroy(arcaneParticles);
        }
        
        if (e.particleType == "Pyro") {
            Destroy(pyroParticles);
        }
        if (e.particleType == "Cryo") {
            Destroy(cryoParticles);
        }
        if (e.particleType == "Bolt") {
            Destroy(boltParticles);
        }
        if (e.particleType == "Toxi") {
            Destroy(toxiParticles);
        }
    }

    public Vector3 GetPosition() {
        return transform.position;
    }
}
