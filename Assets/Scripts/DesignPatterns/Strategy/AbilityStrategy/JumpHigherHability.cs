public class JumpHigherHability : BuffAbility
{
    public JumpHigherHability(PlayerStateMachine playerStateMachine, float buffTime, float multiplier)
        : base(playerStateMachine,
            buffTime,
            () => playerStateMachine.SetMaxJumpVelocity(playerStateMachine.MaxJumpVelocity * multiplier),
            () => playerStateMachine.SetMaxJumpVelocity(playerStateMachine.defaultMaxJumpVelocity))
    { }
}
