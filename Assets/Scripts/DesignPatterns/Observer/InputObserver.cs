using System;

public class InputObserver
{
    private InputActions inputActions;
    public event Action<int> OnTestSelection;
    public event Action OnTestShoot;

    public InputObserver()
    {
        inputActions = new InputActions();
        inputActions.Enable();
        inputActions.Gameplay.TestSelection.performed += (c) => OnTestSelection?.Invoke((int)c.ReadValue<float>());
        inputActions.Gameplay.TestShoot.performed += (c) => OnTestShoot?.Invoke();
    }
}
