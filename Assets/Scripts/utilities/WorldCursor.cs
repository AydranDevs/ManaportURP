using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class WorldCursor : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private InputActionAsset _controls;
    private InputActionMap _inputActionMap;

    private InputAction _mouseMovement;

    private bool primaryFire = true;
    private bool secondaryFire = false;

    Vector2 cursorPos;
    
    void Start()
    {
        _inputActionMap = _controls.FindActionMap("Player");
        CreateInputAction(OnMouseMove, _mouseMovement, "MousePosition");
    }

    private void CreateInputAction(Action<InputAction.CallbackContext> subscriber, InputAction action, string actionName)
    {
        action = _inputActionMap.FindAction(actionName);
        action.Enable();
        action.started += subscriber;
        action.performed += subscriber;
        action.canceled += subscriber;
    }

    public void OnMouseMove(InputAction.CallbackContext context)
    {
        cursorPos = context.ReadValue<Vector2>();
    }

    void Update()
    {
        Vector2 finalCursorPos = Camera.main.ScreenToWorldPoint(cursorPos);
        transform.position = new Vector3(finalCursorPos.x, finalCursorPos.y, 2);
    }
}
