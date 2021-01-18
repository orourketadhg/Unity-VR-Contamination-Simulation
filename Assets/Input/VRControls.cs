// GENERATED AUTOMATICALLY FROM 'Assets/Input/VRControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @VRControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @VRControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""VRControls"",
    ""maps"": [
        {
            ""name"": ""XRRight"",
            ""id"": ""3c8fd46a-0dc1-42e7-b076-29d6379b444a"",
            ""actions"": [
                {
                    ""name"": ""Grip"",
                    ""type"": ""Button"",
                    ""id"": ""9ad36a68-dd80-4e97-bc7b-16758b465398"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TriggerPress"",
                    ""type"": ""Button"",
                    ""id"": ""96263056-11a4-4e38-bad9-ae35ff0d4c1f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TriggerTouch"",
                    ""type"": ""Value"",
                    ""id"": ""855d564f-2732-4841-9429-c5d5599d1a95"",
                    ""expectedControlType"": ""Analog"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PrimaryButton"",
                    ""type"": ""Button"",
                    ""id"": ""92ebd2ba-9586-4c3c-b749-0bb6029cc134"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SecondaryButton"",
                    ""type"": ""Button"",
                    ""id"": ""c661fd39-7835-4b14-b9d7-e420b295bc3a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Joystick"",
                    ""type"": ""Value"",
                    ""id"": ""eacea3c4-fedb-4ccc-9761-08ba6e7a4e53"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""59c04a12-9582-4669-bfa4-277086ac15b2"",
                    ""path"": ""<XRController>{RightHand}/gripPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7fe18ebe-c918-457f-8355-46b6bb8cf597"",
                    ""path"": ""<XRController>{RightHand}/triggerPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TriggerPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8d817c86-2866-4b9d-9545-b39aeacb52ea"",
                    ""path"": ""<XRController>{RightHand}/primaryButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""92605016-04d5-4580-9d27-3fae15573e62"",
                    ""path"": ""<XRController>{RightHand}/secondaryButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondaryButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c8c388e8-9181-4138-8c51-931fc1ba8ea0"",
                    ""path"": ""<XRController>{RightHand}/thumbstick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Joystick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""464f6409-9852-4545-be18-c8259e491047"",
                    ""path"": ""<XRController>{RightHand}/triggerTouched"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TriggerTouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""XRLeft"",
            ""id"": ""4038f130-7b63-408f-8bc4-1b309c58f744"",
            ""actions"": [
                {
                    ""name"": ""Grip"",
                    ""type"": ""Button"",
                    ""id"": ""29b18b20-c062-4374-806c-e0e434fcf59b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TriggerTouch"",
                    ""type"": ""Value"",
                    ""id"": ""3a9f6cb7-b703-4756-9d02-11a2cfc90841"",
                    ""expectedControlType"": ""Analog"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TriggerPress"",
                    ""type"": ""Button"",
                    ""id"": ""06b748bf-5b89-4adc-8717-20941c22b5a9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PrimaryButton"",
                    ""type"": ""Button"",
                    ""id"": ""c047c9c4-fda5-4944-ae57-cb028ac351b3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SecondaryButton"",
                    ""type"": ""Button"",
                    ""id"": ""131318dd-3899-4617-bcee-098bbeee6420"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Joystick"",
                    ""type"": ""Value"",
                    ""id"": ""42601efe-ee76-4997-ad97-878c546c53ae"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""fef42440-7312-4c3c-bd85-80dd1cec111f"",
                    ""path"": ""<XRController>{LeftHand}/gripPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""844d8fb2-79b0-496b-a2ef-3ae4e835cbbf"",
                    ""path"": ""<XRController>{LeftHand}/primaryButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""16da2993-4998-4458-804f-4ddd00c9f324"",
                    ""path"": ""<XRController>{LeftHand}/secondaryButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondaryButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""179e82a9-178a-46ce-a0e4-43d787dcc595"",
                    ""path"": ""<XRController>{LeftHand}/thumbstick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Joystick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""03104bf7-c85c-40d0-aaef-d447e23923be"",
                    ""path"": ""<XRController>{LeftHand}/triggerPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TriggerPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7b8a7cf4-17cc-44f9-9598-57a8b5983671"",
                    ""path"": ""<XRController>{LeftHand}/triggerTouched"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TriggerTouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // XRRight
        m_XRRight = asset.FindActionMap("XRRight", throwIfNotFound: true);
        m_XRRight_Grip = m_XRRight.FindAction("Grip", throwIfNotFound: true);
        m_XRRight_TriggerPress = m_XRRight.FindAction("TriggerPress", throwIfNotFound: true);
        m_XRRight_TriggerTouch = m_XRRight.FindAction("TriggerTouch", throwIfNotFound: true);
        m_XRRight_PrimaryButton = m_XRRight.FindAction("PrimaryButton", throwIfNotFound: true);
        m_XRRight_SecondaryButton = m_XRRight.FindAction("SecondaryButton", throwIfNotFound: true);
        m_XRRight_Joystick = m_XRRight.FindAction("Joystick", throwIfNotFound: true);
        // XRLeft
        m_XRLeft = asset.FindActionMap("XRLeft", throwIfNotFound: true);
        m_XRLeft_Grip = m_XRLeft.FindAction("Grip", throwIfNotFound: true);
        m_XRLeft_TriggerTouch = m_XRLeft.FindAction("TriggerTouch", throwIfNotFound: true);
        m_XRLeft_TriggerPress = m_XRLeft.FindAction("TriggerPress", throwIfNotFound: true);
        m_XRLeft_PrimaryButton = m_XRLeft.FindAction("PrimaryButton", throwIfNotFound: true);
        m_XRLeft_SecondaryButton = m_XRLeft.FindAction("SecondaryButton", throwIfNotFound: true);
        m_XRLeft_Joystick = m_XRLeft.FindAction("Joystick", throwIfNotFound: true);
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

    // XRRight
    private readonly InputActionMap m_XRRight;
    private IXRRightActions m_XRRightActionsCallbackInterface;
    private readonly InputAction m_XRRight_Grip;
    private readonly InputAction m_XRRight_TriggerPress;
    private readonly InputAction m_XRRight_TriggerTouch;
    private readonly InputAction m_XRRight_PrimaryButton;
    private readonly InputAction m_XRRight_SecondaryButton;
    private readonly InputAction m_XRRight_Joystick;
    public struct XRRightActions
    {
        private @VRControls m_Wrapper;
        public XRRightActions(@VRControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Grip => m_Wrapper.m_XRRight_Grip;
        public InputAction @TriggerPress => m_Wrapper.m_XRRight_TriggerPress;
        public InputAction @TriggerTouch => m_Wrapper.m_XRRight_TriggerTouch;
        public InputAction @PrimaryButton => m_Wrapper.m_XRRight_PrimaryButton;
        public InputAction @SecondaryButton => m_Wrapper.m_XRRight_SecondaryButton;
        public InputAction @Joystick => m_Wrapper.m_XRRight_Joystick;
        public InputActionMap Get() { return m_Wrapper.m_XRRight; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(XRRightActions set) { return set.Get(); }
        public void SetCallbacks(IXRRightActions instance)
        {
            if (m_Wrapper.m_XRRightActionsCallbackInterface != null)
            {
                @Grip.started -= m_Wrapper.m_XRRightActionsCallbackInterface.OnGrip;
                @Grip.performed -= m_Wrapper.m_XRRightActionsCallbackInterface.OnGrip;
                @Grip.canceled -= m_Wrapper.m_XRRightActionsCallbackInterface.OnGrip;
                @TriggerPress.started -= m_Wrapper.m_XRRightActionsCallbackInterface.OnTriggerPress;
                @TriggerPress.performed -= m_Wrapper.m_XRRightActionsCallbackInterface.OnTriggerPress;
                @TriggerPress.canceled -= m_Wrapper.m_XRRightActionsCallbackInterface.OnTriggerPress;
                @TriggerTouch.started -= m_Wrapper.m_XRRightActionsCallbackInterface.OnTriggerTouch;
                @TriggerTouch.performed -= m_Wrapper.m_XRRightActionsCallbackInterface.OnTriggerTouch;
                @TriggerTouch.canceled -= m_Wrapper.m_XRRightActionsCallbackInterface.OnTriggerTouch;
                @PrimaryButton.started -= m_Wrapper.m_XRRightActionsCallbackInterface.OnPrimaryButton;
                @PrimaryButton.performed -= m_Wrapper.m_XRRightActionsCallbackInterface.OnPrimaryButton;
                @PrimaryButton.canceled -= m_Wrapper.m_XRRightActionsCallbackInterface.OnPrimaryButton;
                @SecondaryButton.started -= m_Wrapper.m_XRRightActionsCallbackInterface.OnSecondaryButton;
                @SecondaryButton.performed -= m_Wrapper.m_XRRightActionsCallbackInterface.OnSecondaryButton;
                @SecondaryButton.canceled -= m_Wrapper.m_XRRightActionsCallbackInterface.OnSecondaryButton;
                @Joystick.started -= m_Wrapper.m_XRRightActionsCallbackInterface.OnJoystick;
                @Joystick.performed -= m_Wrapper.m_XRRightActionsCallbackInterface.OnJoystick;
                @Joystick.canceled -= m_Wrapper.m_XRRightActionsCallbackInterface.OnJoystick;
            }
            m_Wrapper.m_XRRightActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Grip.started += instance.OnGrip;
                @Grip.performed += instance.OnGrip;
                @Grip.canceled += instance.OnGrip;
                @TriggerPress.started += instance.OnTriggerPress;
                @TriggerPress.performed += instance.OnTriggerPress;
                @TriggerPress.canceled += instance.OnTriggerPress;
                @TriggerTouch.started += instance.OnTriggerTouch;
                @TriggerTouch.performed += instance.OnTriggerTouch;
                @TriggerTouch.canceled += instance.OnTriggerTouch;
                @PrimaryButton.started += instance.OnPrimaryButton;
                @PrimaryButton.performed += instance.OnPrimaryButton;
                @PrimaryButton.canceled += instance.OnPrimaryButton;
                @SecondaryButton.started += instance.OnSecondaryButton;
                @SecondaryButton.performed += instance.OnSecondaryButton;
                @SecondaryButton.canceled += instance.OnSecondaryButton;
                @Joystick.started += instance.OnJoystick;
                @Joystick.performed += instance.OnJoystick;
                @Joystick.canceled += instance.OnJoystick;
            }
        }
    }
    public XRRightActions @XRRight => new XRRightActions(this);

    // XRLeft
    private readonly InputActionMap m_XRLeft;
    private IXRLeftActions m_XRLeftActionsCallbackInterface;
    private readonly InputAction m_XRLeft_Grip;
    private readonly InputAction m_XRLeft_TriggerTouch;
    private readonly InputAction m_XRLeft_TriggerPress;
    private readonly InputAction m_XRLeft_PrimaryButton;
    private readonly InputAction m_XRLeft_SecondaryButton;
    private readonly InputAction m_XRLeft_Joystick;
    public struct XRLeftActions
    {
        private @VRControls m_Wrapper;
        public XRLeftActions(@VRControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Grip => m_Wrapper.m_XRLeft_Grip;
        public InputAction @TriggerTouch => m_Wrapper.m_XRLeft_TriggerTouch;
        public InputAction @TriggerPress => m_Wrapper.m_XRLeft_TriggerPress;
        public InputAction @PrimaryButton => m_Wrapper.m_XRLeft_PrimaryButton;
        public InputAction @SecondaryButton => m_Wrapper.m_XRLeft_SecondaryButton;
        public InputAction @Joystick => m_Wrapper.m_XRLeft_Joystick;
        public InputActionMap Get() { return m_Wrapper.m_XRLeft; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(XRLeftActions set) { return set.Get(); }
        public void SetCallbacks(IXRLeftActions instance)
        {
            if (m_Wrapper.m_XRLeftActionsCallbackInterface != null)
            {
                @Grip.started -= m_Wrapper.m_XRLeftActionsCallbackInterface.OnGrip;
                @Grip.performed -= m_Wrapper.m_XRLeftActionsCallbackInterface.OnGrip;
                @Grip.canceled -= m_Wrapper.m_XRLeftActionsCallbackInterface.OnGrip;
                @TriggerTouch.started -= m_Wrapper.m_XRLeftActionsCallbackInterface.OnTriggerTouch;
                @TriggerTouch.performed -= m_Wrapper.m_XRLeftActionsCallbackInterface.OnTriggerTouch;
                @TriggerTouch.canceled -= m_Wrapper.m_XRLeftActionsCallbackInterface.OnTriggerTouch;
                @TriggerPress.started -= m_Wrapper.m_XRLeftActionsCallbackInterface.OnTriggerPress;
                @TriggerPress.performed -= m_Wrapper.m_XRLeftActionsCallbackInterface.OnTriggerPress;
                @TriggerPress.canceled -= m_Wrapper.m_XRLeftActionsCallbackInterface.OnTriggerPress;
                @PrimaryButton.started -= m_Wrapper.m_XRLeftActionsCallbackInterface.OnPrimaryButton;
                @PrimaryButton.performed -= m_Wrapper.m_XRLeftActionsCallbackInterface.OnPrimaryButton;
                @PrimaryButton.canceled -= m_Wrapper.m_XRLeftActionsCallbackInterface.OnPrimaryButton;
                @SecondaryButton.started -= m_Wrapper.m_XRLeftActionsCallbackInterface.OnSecondaryButton;
                @SecondaryButton.performed -= m_Wrapper.m_XRLeftActionsCallbackInterface.OnSecondaryButton;
                @SecondaryButton.canceled -= m_Wrapper.m_XRLeftActionsCallbackInterface.OnSecondaryButton;
                @Joystick.started -= m_Wrapper.m_XRLeftActionsCallbackInterface.OnJoystick;
                @Joystick.performed -= m_Wrapper.m_XRLeftActionsCallbackInterface.OnJoystick;
                @Joystick.canceled -= m_Wrapper.m_XRLeftActionsCallbackInterface.OnJoystick;
            }
            m_Wrapper.m_XRLeftActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Grip.started += instance.OnGrip;
                @Grip.performed += instance.OnGrip;
                @Grip.canceled += instance.OnGrip;
                @TriggerTouch.started += instance.OnTriggerTouch;
                @TriggerTouch.performed += instance.OnTriggerTouch;
                @TriggerTouch.canceled += instance.OnTriggerTouch;
                @TriggerPress.started += instance.OnTriggerPress;
                @TriggerPress.performed += instance.OnTriggerPress;
                @TriggerPress.canceled += instance.OnTriggerPress;
                @PrimaryButton.started += instance.OnPrimaryButton;
                @PrimaryButton.performed += instance.OnPrimaryButton;
                @PrimaryButton.canceled += instance.OnPrimaryButton;
                @SecondaryButton.started += instance.OnSecondaryButton;
                @SecondaryButton.performed += instance.OnSecondaryButton;
                @SecondaryButton.canceled += instance.OnSecondaryButton;
                @Joystick.started += instance.OnJoystick;
                @Joystick.performed += instance.OnJoystick;
                @Joystick.canceled += instance.OnJoystick;
            }
        }
    }
    public XRLeftActions @XRLeft => new XRLeftActions(this);
    public interface IXRRightActions
    {
        void OnGrip(InputAction.CallbackContext context);
        void OnTriggerPress(InputAction.CallbackContext context);
        void OnTriggerTouch(InputAction.CallbackContext context);
        void OnPrimaryButton(InputAction.CallbackContext context);
        void OnSecondaryButton(InputAction.CallbackContext context);
        void OnJoystick(InputAction.CallbackContext context);
    }
    public interface IXRLeftActions
    {
        void OnGrip(InputAction.CallbackContext context);
        void OnTriggerTouch(InputAction.CallbackContext context);
        void OnTriggerPress(InputAction.CallbackContext context);
        void OnPrimaryButton(InputAction.CallbackContext context);
        void OnSecondaryButton(InputAction.CallbackContext context);
        void OnJoystick(InputAction.CallbackContext context);
    }
}
