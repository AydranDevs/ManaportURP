using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSlot : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    private CircleCollider2D trigger;

    [SerializeField]
    private PuzzleSlotSprite puzzleSlotSprite;
    private int slotTypeId;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        trigger = GetComponent<CircleCollider2D>();

        slotTypeId = puzzleSlotSprite.slotTypeId;
        spriteRenderer.sprite = puzzleSlotSprite.sprite;
    }

    void Update() {
        
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag != "Pushable") return;
        
        var puzzleBoxScript = col.gameObject.GetComponent<PuzzleBox>();

        if (puzzleBoxScript.boxTypeId != slotTypeId) return;
        puzzleBoxScript.active = true;
        puzzleBoxScript.rb.transform.position = transform.position;
        Debug.Log("Ahh~ Such a big blue box...~~");
    }

}
