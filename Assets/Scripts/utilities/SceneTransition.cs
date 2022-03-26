using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {
    public string sceneToLoad;
    public Vector2 playerPos;
    public VectorValue vectorValue;

    public void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && !other.isTrigger){
            vectorValue.initialValue = playerPos;
            SceneManager.LoadScene(sceneToLoad);
        }
    }

}
