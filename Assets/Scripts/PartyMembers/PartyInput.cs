using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyInput : MonoBehaviour {
    private GameStateManager gameStateManager;
    [SerializeField] private InputProvider provider;

    private void Start() {
        gameStateManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameStateManager>();
    }
}
