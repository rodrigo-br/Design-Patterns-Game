using System.Collections;
using UnityEngine;

public class ShootDelayed : IShootStrategy
{
    private readonly float delay;
    private readonly IShootStrategy wrappedStrategy;
    private readonly MonoBehaviour monoBehaviour;
    public BulletSpawner BulletSpawner => throw new System.NotImplementedException();

    public ShootDelayed(IShootStrategy wrappedStrategy, MonoBehaviour monoBehaviour, float delay = 1f)
    {
        this.wrappedStrategy = wrappedStrategy;
        this.monoBehaviour = monoBehaviour;
        this.delay = delay;
    }


    public void Use()
    {
        monoBehaviour.StartCoroutine(DelayedUse());
    }

    private IEnumerator DelayedUse()
    {
        yield return new WaitForSeconds(delay);

        wrappedStrategy.Use();
    }
}
