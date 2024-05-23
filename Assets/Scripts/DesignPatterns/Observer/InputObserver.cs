using System;
using UnityEngine.InputSystem;

public class InputObserver : InputActions.IGameplayActions
{
    private readonly InputActions inputActions;
    public event Action<int> OnTestSelection;
    public event Action OnShoot;
    public event Action<bool> OnJump;
    public event Action<bool> OnJumpStarted;
    public event Action<float> OnMove;

    public InputObserver()
    {
        inputActions = new InputActions();
        inputActions.Gameplay.SetCallbacks(this);
        inputActions.Enable();
    }

    void InputActions.IGameplayActions.OnTestSelection(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnTestSelection?.Invoke((int)context.ReadValue<float>());
        }
    }

    void InputActions.IGameplayActions.OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnShoot?.Invoke();
        }
    }

    void InputActions.IGameplayActions.OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnJumpStarted?.Invoke(true);
        }
        else if (context.performed)
        {
            OnJump?.Invoke(true);
        }
        else if (context.canceled)
        {
            OnJump?.Invoke(false);
        }
    }

    void InputActions.IGameplayActions.OnMove(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnMove?.Invoke(context.ReadValue<float>());
        }
        else if (context.canceled)
        {
            OnMove?.Invoke(0);
        }
    }
}
