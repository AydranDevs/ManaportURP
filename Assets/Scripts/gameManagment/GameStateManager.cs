using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Main, Inv }

public class GameStateManager : MonoBehaviour {
    public static GameStateManager Instance { get; private set; }
    // the game should start in the "main" (walk around) state
    // we will change this when opening a menu, loading, etc
    public GameState state = GameState.Main;
    public event EventHandler<OnGameStateChangedArgs> OnGameStateChanged;
    public class OnGameStateChangedArgs : EventArgs { }
    
    public ChunkDetails currentChunk { get; private set; }
    public ChunkDetails previousChunk { get; private set; }

    public Texture2D defaultCursor;
    public Texture2D inspectCursor;

    Vector2 hotSpot = new Vector2(0,0);
    CursorMode cursorMode = CursorMode.Auto;

    private void Awake() {
        Instance = this;
        ChangeCursor(defaultCursor);
    }

    public void SetCurrentChunk(ChunkDetails currChunk) {
        previousChunk = currentChunk;
        currentChunk = currChunk;
    }

    public void ChangeGameState(GameState gameState) {
        state = gameState;
        OnGameStateChanged?.Invoke(this, new OnGameStateChangedArgs { });
    }

    public void ChangeCursor(Texture2D cursor) {
        Cursor.SetCursor(cursor, hotSpot, cursorMode);
    }
}
