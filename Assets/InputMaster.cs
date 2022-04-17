// GENERATED AUTOMATICALLY FROM 'Assets/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""7ceba62f-8fa7-4f72-b700-f64ccf70d8fc"",
            ""actions"": [
                {
                    ""name"": ""PrimaryCast"",
                    ""type"": ""Button"",
                    ""id"": ""db59fda0-763a-4b46-97b2-6c6e3bbf05a1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SecondaryCast"",
                    ""type"": ""Button"",
                    ""id"": ""4c2a6f0b-809e-46d4-bfaf-8a136d67f382"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""158e59c7-648a-4bcf-9f2f-a75a99d8998a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AuxilaryMovement"",
                    ""type"": ""Button"",
                    ""id"": ""60a112de-bf57-4626-80ea-51c6e98184a8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""f4b28910-1294-4ac0-9bcb-53b7c92fb656"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""979bc7f2-e651-4ee8-9a13-4da87fc6b805"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5fc6f16b-6754-4791-82a2-d1f382216a50"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""PrimaryCast"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f7caffc3-94cc-4552-8381-f6565351bc8d"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""SecondaryCast"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""b23d576e-b166-41ad-9de8-5f5cbc881ef7"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""34b80daf-0959-4eaa-b79a-38dd62e31ee6"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""1742ea41-a3d9-411c-b017-27edacf07e27"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""1b13f34d-b2e5-4b1b-95fe-3b3b0d37a2fd"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""40a33360-ffb5-4a68-b25d-dfc745dee3a0"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""55dec5d0-09a5-41b5-b27c-7c8a99803083"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""AuxilaryMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d7301039-ec0e-4b62-84cc-e3d28c6e7cc9"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d5c478c1-bd70-4c6a-b5d7-5c07b5bee4df"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeyboardAndMouse"",
            ""bindingGroup"": ""KeyboardAndMouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_PrimaryCast = m_Player.FindAction("PrimaryCast", throwIfNotFound: true);
        m_Player_SecondaryCast = m_Player.FindAction("SecondaryCast", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_AuxilaryMovement = m_Player.FindAction("AuxilaryMovement", throwIfNotFound: true);
        m_Player_MousePosition = m_Player.FindAction("MousePosition", throwIfNotFound: true);
        m_Player_Sprint = m_Player.FindAction("Sprint", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_PrimaryCast;
    private readonly InputAction m_Player_SecondaryCast;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_AuxilaryMovement;
    private readonly InputAction m_Player_MousePosition;
    private readonly InputAction m_Player_Sprint;
    public struct PlayerActions
    {
        private @InputMaster m_Wrapper;
        public PlayerActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @PrimaryCast => m_Wrapper.m_Player_PrimaryCast;
        public InputAction @SecondaryCast => m_Wrapper.m_Player_SecondaryCast;
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @AuxilaryMovement => m_Wrapper.m_Player_AuxilaryMovement;
        public InputAction @MousePosition => m_Wrapper.m_Player_MousePosition;
        public InputAction @Sprint => m_Wrapper.m_Player_Sprint;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @PrimaryCast.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPrimaryCast;
                @PrimaryCast.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPrimaryCast;
                @PrimaryCast.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPrimaryCast;
                @SecondaryCast.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSecondaryCast;
                @SecondaryCast.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSecondaryCast;
                @SecondaryCast.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSecondaryCast;
                @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @AuxilaryMovement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAuxilaryMovement;
                @AuxilaryMovement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAuxilaryMovement;
                @AuxilaryMovement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAuxilaryMovement;
                @MousePosition.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePosition;
                @Sprint.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprint;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PrimaryCast.started += instance.OnPrimaryCast;
                @PrimaryCast.performed += instance.OnPrimaryCast;
                @PrimaryCast.canceled += instance.OnPrimaryCast;
                @SecondaryCast.started += instance.OnSecondaryCast;
                @SecondaryCast.performed += instance.OnSecondaryCast;
                @SecondaryCast.canceled += instance.OnSecondaryCast;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @AuxilaryMovement.started += instance.OnAuxilaryMovement;
                @AuxilaryMovement.performed += instance.OnAuxilaryMovement;
                @AuxilaryMovement.canceled += instance.OnAuxilaryMovement;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_KeyboardAndMouseSchemeIndex = -1;
    public InputControlScheme KeyboardAndMouseScheme
    {
        get
        {
            if (m_KeyboardAndMouseSchemeIndex == -1) m_KeyboardAndMouseSchemeIndex = asset.FindControlSchemeIndex("KeyboardAndMouse");
            return asset.controlSchemes[m_KeyboardAndMouseSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnPrimaryCast(InputAction.CallbackContext context);
        void OnSecondaryCast(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnAuxilaryMovement(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
    }
}
