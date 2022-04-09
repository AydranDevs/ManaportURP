using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PuzzleBox : MonoBehaviour {
    
    private SpriteRenderer spriteRenderer;
    private Light2D light;
    public Rigidbody2D rb;

    [SerializeField]
    private PuzzleBoxSprite puzzleBoxSprite;
    public int boxTypeId;

    public bool active = false;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        light = GetComponent<Light2D>();
        
        light.enabled = false;

        boxTypeId = puzzleBoxSprite.boxTypeId;
    }

    void Update() {
        if (!active) return;
        spriteRenderer.sprite = puzzleBoxSprite.inactiveSprite;

        if (!active) return;
        light.enabled = true;
        
        spriteRenderer.sprite = puzzleBoxSprite.activeSprite;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
