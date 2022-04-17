using UnityEngine;
using UnityEngine.InputSystem;

public class WorldCursor : MonoBehaviour {

    private Animator animator;

    private bool primaryFire = true;
    private bool secondaryFire = false;

    Vector2 cursorPos;
    
    void Start() {
        Cursor.visible = false;

        animator = GetComponent<Animator>();
    }

    public void OnMouseMove(InputAction.CallbackContext context) {
        cursorPos = context.ReadValue<Vector2>();
    }

    void Update() {
        Vector2 finalCursorPos = Camera.main.ScreenToWorldPoint(cursorPos);
        transform.position = new Vector3(finalCursorPos.x, finalCursorPos.y, 0);
    }
    
    public void OnPrimaryCast(InputAction.CallbackContext context) {
        if (!context.started) return;

        primaryFire = true;
        secondaryFire = false;

        animator.Rebind();
        animator.SetTrigger("PrimaryFire");
    }

    public void OnSecondaryCast(InputAction.CallbackContext context) {
        if (!context.started) return;
        
        primaryFire = false;
        secondaryFire = true;

        animator.Rebind();
        animator.SetTrigger("SecondaryFire");
    }
}
