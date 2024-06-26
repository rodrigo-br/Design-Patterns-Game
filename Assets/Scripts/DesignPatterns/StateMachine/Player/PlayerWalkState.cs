using System;
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    private readonly Rigidbody2D feetRigidBody;

    public PlayerWalkState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        feetRigidBody = stateMachine.FeetRigidBody2D;
    }

    public override void Enter()
    {
        GameManager.Instance.InputObserver.OnJumpStarted += SetIsJumpStarted;
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        if (Mathf.Abs(feetRigidBody.velocity.x) <= 0.2f && stateMachine.MoveDirection == 0)
        {
            stateMachine.SwitchState(new PlayerIdleState(stateMachine));
        }
        stateMachine.Animator.SetFloat("TwoLegs", 1, 0.1f, deltaTime);
    }

    public override void Exit()
    {
        GameManager.Instance.InputObserver.OnJumpStarted -= SetIsJumpStarted;
    }

    private void SetIsJumpStarted(bool obj)
    {
        stateMachine.SwitchState(new PlayerJumpState(stateMachine));
    }

    public void Move(float deltaTime)
    {
        float newSpeed = Mathf.Lerp(
            feetRigidBody.velocity.x,
            stateMachine.MoveDirection * stateMachine.MaxMovementSpeed,
            stateMachine.DeltaSpeed * deltaTime);

        feetRigidBody.velocity = new Vector2(newSpeed, feetRigidBody.velocity.y);
    }
}
