using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour {
    [SerializeField] private InputProvider provider;

    private void Start() {

    }
   
    
    public void OnMove(InputAction.CallbackContext context) {
        provider.inputState.movementDirection = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context) {
        if (!context.canceled) {
            provider.inputState.isSprinting = true;
        }else {
            provider.inputState.isSprinting = false;
        }
    }
}
