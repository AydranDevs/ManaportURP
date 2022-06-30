using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Main, Inv }

public class GameStateManager : MonoBehaviour {
    public static GameStateManager Instance { get; private set; }
    // the game should start in the "main" (walk around) state
    // we will change this when opening a menu, loading, etc
    public GameState state = GameState.Main;
    
    public ChunkDetails currentChunk { get; private set; }
    public ChunkDetails previousChunk { get; private set; }

    private void Awake() {
        Instance = this;
    }

    public void SetCurrentChunk(ChunkDetails currChunk) {
        previousChunk = currentChunk;
        currentChunk = currChunk;
    }
}
