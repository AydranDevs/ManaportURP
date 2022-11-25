using System;
using UnityEngine.InputSystem;

namespace Manapotion.Utilities
{
    public static class UtilitiesClass
    {
        /// <summary>
        /// Subscribe to the InputAction Given.
        /// </summary>
        /// <param name="inputActionMap">InputActionMap that contains the InputAction</param>
        /// <param name="subscriber">Action callback</param>
        /// <param name="action"></param>
        /// <param name="actionName">InputAction name (found in InputMaster inspector</param>
        public static void CreateInputAction(InputActionMap inputActionMap, Action<InputAction.CallbackContext> subscriber, InputAction action, string actionName)
        {
            action = inputActionMap.FindAction(actionName);
            action.Enable();
            action.started += subscriber;
            action.performed += subscriber;
            action.canceled += subscriber;
        }
    }
}