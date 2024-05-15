using System;

public class InputObserver
{
    private InputActions inputActions;
    public event Action<int> OnTestSelection;
    public event Action OnTestShoot;
    public event Action<bool> OnJump;
    public event Action OnJumpStarted;

    public InputObserver()
    {
        inputActions = new InputActions();
        inputActions.Enable();
        inputActions.Gameplay.TestSelection.performed += (ctx) => OnTestSelection?.Invoke((int)ctx.ReadValue<float>());
        inputActions.Gameplay.TestShoot.performed += (_) => OnTestShoot?.Invoke();
        inputActions.Gameplay.Jump.started += (_) => OnJumpStarted?.Invoke();
        inputActions.Gameplay.Jump.canceled += (_) => OnJump?.Invoke(false);
        inputActions.Gameplay.Jump.performed += (_) => OnJump?.Invoke(true);
    }
}
