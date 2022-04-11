using UnityEngine;

public class WorldCursor : MonoBehaviour {

    private Animator animator;

    private bool primaryFire = true;
    private bool secondaryFire = false;
    
    void Start() {
        Cursor.visible = false;

        animator = GetComponent<Animator>();
    }

    void Update() {
        var cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(cursorPos.x, cursorPos.y, 0);

        if (Input.GetMouseButtonDown(0)) {
            primaryFire = true;
            secondaryFire = false;

            animator.Rebind();
            animator.SetTrigger("PrimaryFire");
        }

        if (Input.GetMouseButtonDown(1)) {
            primaryFire = false;
            secondaryFire = true;

            animator.Rebind();
            animator.SetTrigger("SecondaryFire");
        }
    }
}
