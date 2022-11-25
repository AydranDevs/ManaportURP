// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input/InputMaster.inputactions'

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
                    ""name"": ""PrimaryAction"",
                    ""type"": ""Button"",
                    ""id"": ""db59fda0-763a-4b46-97b2-6c6e3bbf05a1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SecondaryAction"",
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
                    ""name"": ""AuxilaryAction"",
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
                },
                {
                    ""name"": ""ToggleDebug"",
                    ""type"": ""Button"",
                    ""id"": ""db01e565-3f24-4fe8-a439-5ab5c4c4ecbe"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Return"",
                    ""type"": ""Button"",
                    ""id"": ""9549112c-ad66-42ed-a0a6-087675ee8a70"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Previous"",
                    ""type"": ""Button"",
                    ""id"": ""d67a6a94-350b-47cc-ab22-bd15e768f68f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Next"",
                    ""type"": ""Button"",
                    ""id"": ""e55cca3b-f08c-49b1-a471-66b846394669"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToggleBag"",
                    ""type"": ""Button"",
                    ""id"": ""4878e56c-cded-4fa5-84b1-f25c8b0fc2b2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToggleEquip"",
                    ""type"": ""Button"",
                    ""id"": ""6e9f8615-5be2-4d40-a9eb-8ce74447a060"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToggleBeastiary"",
                    ""type"": ""Button"",
                    ""id"": ""a01bf15e-fb80-42e5-bdbe-a5d58523069b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""676582d2-5d0b-42d2-bc4e-fc13852eea3e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Continue"",
                    ""type"": ""Button"",
                    ""id"": ""caea9ad2-34ec-4337-b62a-148017668843"",
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
                    ""action"": ""PrimaryAction"",
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
                    ""action"": ""SecondaryAction"",
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
                    ""action"": ""AuxilaryAction"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""43c43507-4235-47c5-9b6a-0534188a9b17"",
                    ""path"": ""<Keyboard>/backquote"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""ToggleDebug"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""27e624c0-de9a-49e0-b7e9-4ca0be4dadea"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Return"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""793be0a5-bd8a-43cc-af23-7640103b490a"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Previous"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""70ae3a4a-de97-4abb-9ac1-0655aa42d9ce"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Next"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ed46320f-9f6d-4bbc-b42f-b922c299b6f1"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""ToggleBag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b9a38f06-24f8-42e4-9ac4-135fe972aa0c"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""ToggleEquip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2c57c439-dbc6-4ed4-b043-1df575f97e0c"",
                    ""path"": ""<Keyboard>/n"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""ToggleBeastiary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""04859e79-2ddf-491b-bde7-798d48587e02"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4b9390f2-1cfc-4b7b-926d-10a81b0e2cc3"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Continue"",
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
        m_Player_PrimaryAction = m_Player.FindAction("PrimaryAction", throwIfNotFound: true);
        m_Player_SecondaryAction = m_Player.FindAction("SecondaryAction", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_AuxilaryAction = m_Player.FindAction("AuxilaryAction", throwIfNotFound: true);
        m_Player_MousePosition = m_Player.FindAction("MousePosition", throwIfNotFound: true);
        m_Player_Sprint = m_Player.FindAction("Sprint", throwIfNotFound: true);
        m_Player_ToggleDebug = m_Player.FindAction("ToggleDebug", throwIfNotFound: true);
        m_Player_Return = m_Player.FindAction("Return", throwIfNotFound: true);
        m_Player_Previous = m_Player.FindAction("Previous", throwIfNotFound: true);
        m_Player_Next = m_Player.FindAction("Next", throwIfNotFound: true);
        m_Player_ToggleBag = m_Player.FindAction("ToggleBag", throwIfNotFound: true);
        m_Player_ToggleEquip = m_Player.FindAction("ToggleEquip", throwIfNotFound: true);
        m_Player_ToggleBeastiary = m_Player.FindAction("ToggleBeastiary", throwIfNotFound: true);
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
        m_Player_Continue = m_Player.FindAction("Continue", throwIfNotFound: true);
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
    private readonly InputAction m_Player_PrimaryAction;
    private readonly InputAction m_Player_SecondaryAction;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_AuxilaryAction;
    private readonly InputAction m_Player_MousePosition;
    private readonly InputAction m_Player_Sprint;
    private readonly InputAction m_Player_ToggleDebug;
    private readonly InputAction m_Player_Return;
    private readonly InputAction m_Player_Previous;
    private readonly InputAction m_Player_Next;
    private readonly InputAction m_Player_ToggleBag;
    private readonly InputAction m_Player_ToggleEquip;
    private readonly InputAction m_Player_ToggleBeastiary;
    private readonly InputAction m_Player_Interact;
    private readonly InputAction m_Player_Continue;
    public struct PlayerActions
    {
        private @InputMaster m_Wrapper;
        public PlayerActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @PrimaryAction => m_Wrapper.m_Player_PrimaryAction;
        public InputAction @SecondaryAction => m_Wrapper.m_Player_SecondaryAction;
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @AuxilaryAction => m_Wrapper.m_Player_AuxilaryAction;
        public InputAction @MousePosition => m_Wrapper.m_Player_MousePosition;
        public InputAction @Sprint => m_Wrapper.m_Player_Sprint;
        public InputAction @ToggleDebug => m_Wrapper.m_Player_ToggleDebug;
        public InputAction @Return => m_Wrapper.m_Player_Return;
        public InputAction @Previous => m_Wrapper.m_Player_Previous;
        public InputAction @Next => m_Wrapper.m_Player_Next;
        public InputAction @ToggleBag => m_Wrapper.m_Player_ToggleBag;
        public InputAction @ToggleEquip => m_Wrapper.m_Player_ToggleEquip;
        public InputAction @ToggleBeastiary => m_Wrapper.m_Player_ToggleBeastiary;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputAction @Continue => m_Wrapper.m_Player_Continue;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @PrimaryAction.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPrimaryAction;
                @PrimaryAction.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPrimaryAction;
                @PrimaryAction.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPrimaryAction;
                @SecondaryAction.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSecondaryAction;
                @SecondaryAction.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSecondaryAction;
                @SecondaryAction.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSecondaryAction;
                @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @AuxilaryAction.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAuxilaryAction;
                @AuxilaryAction.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAuxilaryAction;
                @AuxilaryAction.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAuxilaryAction;
                @MousePosition.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePosition;
                @Sprint.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprint;
                @ToggleDebug.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleDebug;
                @ToggleDebug.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleDebug;
                @ToggleDebug.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleDebug;
                @Return.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReturn;
                @Return.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReturn;
                @Return.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReturn;
                @Previous.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPrevious;
                @Previous.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPrevious;
                @Previous.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPrevious;
                @Next.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnNext;
                @Next.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnNext;
                @Next.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnNext;
                @ToggleBag.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleBag;
                @ToggleBag.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleBag;
                @ToggleBag.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleBag;
                @ToggleEquip.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleEquip;
                @ToggleEquip.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleEquip;
                @ToggleEquip.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleEquip;
                @ToggleBeastiary.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleBeastiary;
                @ToggleBeastiary.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleBeastiary;
                @ToggleBeastiary.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleBeastiary;
                @Interact.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Continue.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnContinue;
                @Continue.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnContinue;
                @Continue.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnContinue;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PrimaryAction.started += instance.OnPrimaryAction;
                @PrimaryAction.performed += instance.OnPrimaryAction;
                @PrimaryAction.canceled += instance.OnPrimaryAction;
                @SecondaryAction.started += instance.OnSecondaryAction;
                @SecondaryAction.performed += instance.OnSecondaryAction;
                @SecondaryAction.canceled += instance.OnSecondaryAction;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @AuxilaryAction.started += instance.OnAuxilaryAction;
                @AuxilaryAction.performed += instance.OnAuxilaryAction;
                @AuxilaryAction.canceled += instance.OnAuxilaryAction;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @ToggleDebug.started += instance.OnToggleDebug;
                @ToggleDebug.performed += instance.OnToggleDebug;
                @ToggleDebug.canceled += instance.OnToggleDebug;
                @Return.started += instance.OnReturn;
                @Return.performed += instance.OnReturn;
                @Return.canceled += instance.OnReturn;
                @Previous.started += instance.OnPrevious;
                @Previous.performed += instance.OnPrevious;
                @Previous.canceled += instance.OnPrevious;
                @Next.started += instance.OnNext;
                @Next.performed += instance.OnNext;
                @Next.canceled += instance.OnNext;
                @ToggleBag.started += instance.OnToggleBag;
                @ToggleBag.performed += instance.OnToggleBag;
                @ToggleBag.canceled += instance.OnToggleBag;
                @ToggleEquip.started += instance.OnToggleEquip;
                @ToggleEquip.performed += instance.OnToggleEquip;
                @ToggleEquip.canceled += instance.OnToggleEquip;
                @ToggleBeastiary.started += instance.OnToggleBeastiary;
                @ToggleBeastiary.performed += instance.OnToggleBeastiary;
                @ToggleBeastiary.canceled += instance.OnToggleBeastiary;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Continue.started += instance.OnContinue;
                @Continue.performed += instance.OnContinue;
                @Continue.canceled += instance.OnContinue;
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
        void OnPrimaryAction(InputAction.CallbackContext context);
        void OnSecondaryAction(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnAuxilaryAction(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnToggleDebug(InputAction.CallbackContext context);
        void OnReturn(InputAction.CallbackContext context);
        void OnPrevious(InputAction.CallbackContext context);
        void OnNext(InputAction.CallbackContext context);
        void OnToggleBag(InputAction.CallbackContext context);
        void OnToggleEquip(InputAction.CallbackContext context);
        void OnToggleBeastiary(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnContinue(InputAction.CallbackContext context);
    }
}
