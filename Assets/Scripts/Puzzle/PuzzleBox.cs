using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PuzzleBox : MonoBehaviour {
    
    private SpriteRenderer spriteRenderer;
    private Light2D myLight;
    public Rigidbody2D rb;

    [SerializeField]
    private PuzzleBoxSprite puzzleBoxSprite;
    public int boxTypeId;

    public bool active = false;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        myLight = GetComponent<Light2D>();
        
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        
        myLight.enabled = false;

        boxTypeId = puzzleBoxSprite.boxTypeId;
    }

    private void OnCollisionStay2D(Collision2D col) {
        if (col.gameObject.tag != "Player") return;

        // Player player = col.gameObject.GetComponent<Player>();
        // if (player != null) {
        //     if (player.isPushing) {
        //         rb.constraints = RigidbodyConstraints2D.None;
        //         rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        //     }else {
        //         rb.constraints = RigidbodyConstraints2D.FreezeAll;
        //     }
        // }
    }

    void Update() {
        // if (!active) return;
        spriteRenderer.sprite = puzzleBoxSprite.inactiveSprite;

        if (!active) return;
        myLight.enabled = true;
        
        spriteRenderer.sprite = puzzleBoxSprite.activeSprite;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
