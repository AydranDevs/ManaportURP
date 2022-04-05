using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBox : MonoBehaviour {
    
    private SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;

    [SerializeField]
    private PuzzleBoxSprite puzzleBoxSprite;
    public int boxTypeId;

    public bool active = false;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        boxTypeId = puzzleBoxSprite.boxTypeId;
    }

    void Update() {
        spriteRenderer.sprite = puzzleBoxSprite.inactiveSprite;

        if (!active) return;
        
        spriteRenderer.sprite = puzzleBoxSprite.activeSprite;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
