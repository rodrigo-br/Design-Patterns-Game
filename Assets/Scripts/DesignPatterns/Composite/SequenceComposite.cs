using UnityEngine;

public class SequenceComposite : IStrategyPattern
{
    private readonly IStrategyPattern[] children;

    public SequenceComposite(IStrategyPattern[] children)
    {
        this.children = children;
    }

    public void Use()
    {
        foreach (var child in children)
        {
            child.Use();
        }
    }
}
