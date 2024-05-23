using System.Collections;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    #region auto-properties
    [field: SerializeField] public Rigidbody2D FeetRigidBody2D { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public float MaxJumpVelocity { get; private set;  } = 5;
    [field: SerializeField] public float FallMultiplier { get; private set;  } = 2.5f;
    [field: SerializeField] public float LowJumpMultiplier { get; private set;  } = 2f;
    [field: SerializeField] public float MaxMovementSpeed { get; private set; } = 10f;
    [field: SerializeField] public float DeltaSpeed { get; private set; } = 1f;
    [field: SerializeField] public float MaxAirMovementSpeed { get; private set; } = 8f;
    public bool IsJumpingPressed { get; private set; } = false;
    public float MoveDirection { get; private set; } = 0;
    public Vector2 Gravity { get; private set; }
    #endregion

    #region Unity Methods
    private void Start()
    {
        Gravity = Physics2D.gravity;
        SwitchState(new PlayerIdleState(this));
    }

    private void OnEnable()
    {
        StartCoroutine(AssignInputs());
    }

    private void OnDisable()
    {
        GameManager.Instance.InputObserver.OnJump -= SetIsJumpingPressed;
        GameManager.Instance.InputObserver.OnMove -= SetMoveDirection;
    }
    #endregion

    private IEnumerator AssignInputs()
    {
        yield return new WaitUntil(() => GameManager.Instance != null);
        GameManager.Instance.InputObserver.OnJump += SetIsJumpingPressed;
        GameManager.Instance.InputObserver.OnMove += SetMoveDirection;
    }

    private void SetMoveDirection(float direction)
    {
        MoveDirection = direction;
    }

    private void SetIsJumpingPressed(bool input)
    {
        IsJumpingPressed = input;
    }
}
