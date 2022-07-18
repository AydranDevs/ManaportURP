using UnityEngine;
using UnityEngine.InputSystem;

public class WorldCursor : MonoBehaviour {

    private Animator animator;

    private bool primaryFire = true;
    private bool secondaryFire = false;

    Vector2 cursorPos;
    
    void Start() {
        // Cursor.visible = false;
    }

    public void OnMouseMove(InputAction.CallbackContext context) {
        cursorPos = context.ReadValue<Vector2>();
    }

    void Update() {
        Vector2 finalCursorPos = Camera.main.ScreenToWorldPoint(cursorPos);
        transform.position = new Vector3(finalCursorPos.x, finalCursorPos.y, 2);
    }
}
