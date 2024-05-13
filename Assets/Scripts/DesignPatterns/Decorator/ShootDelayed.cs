using System.Collections;
using UnityEngine;

public class ShootDelayed : IShootStrategy
{
    float delay = 1f;
    private IShootStrategy wrappedStrategy;
    private MonoBehaviour monoBehaviour;
    public BulletSpawner BulletSpawner => throw new System.NotImplementedException();

    public ShootDelayed(IShootStrategy wrappedStrategy, MonoBehaviour monoBehaviour)
    {
        this.wrappedStrategy = wrappedStrategy;
        this.monoBehaviour = monoBehaviour;
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
