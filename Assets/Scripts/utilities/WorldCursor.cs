using UnityEngine;

public class WorldCursor : MonoBehaviour
{
    void Start() {
        Cursor.visible = false;
    }

    void Update()
    {
        var cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(cursorPos.x, cursorPos.y, 0);
    }
}
