using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChunkDetails : MonoBehaviour {

    public List<ChunkDetails> connectedChunks;
    public bool isLoaded { get; private set;}

    public int chunkX, chunkY;
    public Vector3 worldPos;

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.tag != "PlayerPartyLeader") return;
        LoadChunk();
        GameStateManager.Instance.SetCurrentChunk(this);
        
        // load connected scenes
        foreach (ChunkDetails chunk in connectedChunks) {
            chunk.LoadChunk();
        }

        // unload unconnected scenes
        if (GameStateManager.Instance.previousChunk != null) {
            var previouslyLoadedChunks = GameStateManager.Instance.previousChunk.connectedChunks;
            foreach (var chunk in previouslyLoadedChunks) {
                if (!connectedChunks.Contains(chunk) && chunk != this) {
                    chunk.UnloadChunk();
                }
            }
        }

    }

    public void LoadChunk() {
        if (isLoaded) return;
        Debug.Log("Loading " + gameObject.name);
        SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
        isLoaded = true;
    }

    public void UnloadChunk() {
        if (!isLoaded) return;
        Debug.Log("Loading " + gameObject.name);
        SceneManager.UnloadSceneAsync(gameObject.name);
        isLoaded = false;
    }
}
