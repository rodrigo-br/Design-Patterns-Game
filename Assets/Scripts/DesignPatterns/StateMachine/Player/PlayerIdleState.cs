using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        GameManager.Instance.InputObserver.OnJumpStarted += SetIsJumpStarted;
        stateMachine.FeetRigidBody2D.velocity = Vector2.zero;
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.MoveDirection != 0)
        {
            stateMachine.SwitchState(new PlayerWalkState(stateMachine));
        }
        stateMachine.Animator.SetFloat("TwoLegs", 0, 0.1f, deltaTime);
    }

    public override void Exit()
    {
        GameManager.Instance.InputObserver.OnJumpStarted -= SetIsJumpStarted;
    }

    private void SetIsJumpStarted(bool obj)
    {
        stateMachine.SwitchState(new PlayerJumpState(stateMachine));
    }
}
