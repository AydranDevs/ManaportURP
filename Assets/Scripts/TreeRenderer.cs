using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aarthificial.Reanimation;

public class TreeRenderer : MonoBehaviour {
    [SerializeField]
    private int sortingOrderBase = 5000;
    [SerializeField]
    private int offset = 0;
    [SerializeField]
    private bool runOnlyOnce = false;
    
    private float timer;
    private float timerMax = .1f;

    private Renderer myRenderer;
    private Reanimator reanimator;
    

    private void Awake() {
        myRenderer = gameObject.GetComponent<Renderer>();
        reanimator = gameObject.GetComponent<Reanimator>();
    }

    private void Update() {
        timer -= Time.deltaTime;
        if (timer <= 0f) {
            int rand = Random.Range(0, 6);
            if (rand == 5 && reanimator.enabled == false) reanimator.enabled = true; 
        }
        myRenderer.sortingOrder = (int)(sortingOrderBase - transform.position.y - offset);
        if (runOnlyOnce)
            Destroy(this);
    }
}
