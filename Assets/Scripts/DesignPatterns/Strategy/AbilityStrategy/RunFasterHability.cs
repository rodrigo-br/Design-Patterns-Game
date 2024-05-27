public class RunFasterHability : BuffAbility
{
    public RunFasterHability(PlayerStateMachine playerStateMachine, float buffTime, float multiplier)
        : base(playerStateMachine,
            buffTime,
            () => playerStateMachine.SetMaxMovementSpeed(playerStateMachine.MaxMovementSpeed * multiplier),
            () => playerStateMachine.SetMaxMovementSpeed(playerStateMachine.defaultMaxMovementSpeed))
    { }
}
