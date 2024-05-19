using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    private readonly Rigidbody2D feetRigidBody;

    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        feetRigidBody = stateMachine.FeetRigidBody2D;
    }

    public override void Enter()
    {
        feetRigidBody.velocity = new Vector2(feetRigidBody.velocity.x, stateMachine.MaxJumpVelocity);
    }

    public override void Tick(float deltaTime)
    {
        MoveX(deltaTime);
        MoveY(deltaTime);

        if (feetRigidBody.velocity.y <= 0 && IsOnGround())
        {
            stateMachine.SwitchState(new PlayerWalkState(stateMachine));
        }
    }

    public override void Exit()
    {
    }

    public void MoveX(float deltaTime)
    {
        float newSpeed = Mathf.Lerp(
            feetRigidBody.velocity.x,
            stateMachine.MoveDirection * stateMachine.MaxAirMovementSpeed,
            stateMachine.DeltaSpeed * deltaTime);

        feetRigidBody.velocity = new Vector2(newSpeed, feetRigidBody.velocity.y);
    }

    private void MoveY(float deltaTime)
    {
        if (feetRigidBody.velocity.y < 0)
        {
            feetRigidBody.velocity += new Vector2(0, stateMachine.Gravity.y * (stateMachine.FallMultiplier - 1) * deltaTime);
        }
        else if (feetRigidBody.velocity.y > 0 && !stateMachine.IsJumpingPressed)
        {
            feetRigidBody.velocity += new Vector2(0, stateMachine.Gravity.y * (stateMachine.LowJumpMultiplier - 1) * deltaTime);
        }
    }

    private bool IsOnGround()
    {
        return feetRigidBody.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
}
