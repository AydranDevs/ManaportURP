using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegumelAttacking : MonoBehaviour {
    private Legumel legumel;
    private GameStateManager gameStateManager;

    public Spell spell;

    private string element = Elements.Toxi;

    bool once = false;

    public event EventHandler<OnSpellCastEventArgs> OnSpellCast;
    public class OnSpellCastEventArgs : EventArgs { }

    private void Start() {
        legumel = GetComponent<Legumel>();
        gameStateManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameStateManager>();

        var spells = GetComponentsInChildren<Spell>();

        SetSpell(spells[0]);
    }

    void SetSpell(Spell spell) {
        this.spell = spell;
    }

    private void Update() {
        if (legumel.dead) return;

        if (legumel.aggro) {
            if (!once) {
                StartCoroutine(Wait());
                once = true;
            }
        }
    }

    IEnumerator Wait() {
        yield return new WaitForSeconds(2);
        if (gameStateManager.state == GameState.Main && !legumel.dead) {
            OnSpellCast?.Invoke(this, new OnSpellCastEventArgs { });
            var direction = (legumel.targetPosition - (Vector2)transform.position).normalized;
            spell.Cast(direction, element);
        }
        once = false;
    }
}
