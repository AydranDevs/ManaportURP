using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestHandler : InteractableBase {
    public LootTableData lootTable;
    public int tryForLoot = 1;
    public SpriteRenderer renderer;
    public GameObject particleSys;
    
    public Sprite closedSprite;
    public Sprite newlyOpenedSprite;
    public Sprite openedSprite;

    public bool spawnParticlesOnOpen = false;
    public float particleLifetimeMax = 5f;
    public float particleLifetime = 0f;

    private GameObject _particleSys;
    private bool particleSysActive = false;
    private bool dormant = false;


    private void OnTriggerEnter2D(Collider2D col) {
        if (dormant) return;
        if (col.gameObject.tag != "PlayerPartyLeader") return;
        
        var interaction = col.gameObject.GetComponent<CharacterInteractionHandler>();
        if (!interaction.interactablesList.Contains(transform)) {
            interaction.interactablesList.Add(transform);
        }else {
            return;
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        if (dormant) return;
        if (col.gameObject.tag != "PlayerPartyLeader") return;

        var interaction = col.gameObject.GetComponent<CharacterInteractionHandler>();
        interaction.interactablesList.Remove(transform);
    }

    public override void Interact() {
        if (dormant) return;

        OpenChest();
        particleLifetime = particleLifetimeMax;
    }

    private void OpenChest() {
        renderer.sprite = newlyOpenedSprite;
        _particleSys = Instantiate(particleSys, transform.position, Quaternion.identity, transform);
        particleSysActive = true;
        DropLoot();
    }

    private void MakeChestDormant() {
        renderer.sprite = openedSprite;
        _particleSys.GetComponentInChildren<ParticleSystem>().Stop();
        particleSysActive = false;
        dormant = true;
    }

    private void Update() {
        if (dormant) return;

        if (particleSysActive) {
            particleLifetime -= Time.deltaTime;
            if (particleLifetime <= 0f) {
                MakeChestDormant();
                particleLifetime = particleLifetimeMax;
            }
        }
    }

    private void DropLoot() {
        for (int i = 0; i < tryForLoot; i++) {
            GameObject go = Instantiate(lootTable.GetRandomItem().item, transform.position, Quaternion.identity);
        }
    }
}
