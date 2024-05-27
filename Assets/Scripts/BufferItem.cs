using UnityEngine;

public class BufferItem : MonoBehaviour
{
    private enum Buff
    {
        JumpHigherHability,
        RunFasterHability
    }
    [SerializeField] private float buffTime;
    [SerializeField] private Buff buff;
    [SerializeField][Range(1, 3)] private float multiplier;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStateMachine playerStateMachine = collision.GetComponent<PlayerStateMachine>();

        if (playerStateMachine == null) return;

        IAbilityStrategy ability = null;

        switch (buff)
        {
            case Buff.JumpHigherHability:
                ability = new JumpHigherHability(playerStateMachine, buffTime, multiplier);
                break;
            case Buff.RunFasterHability:
                ability = new RunFasterHability(playerStateMachine, buffTime, multiplier);
                break;
            default:
                break;
        }

        ability?.Use();
    }
}
