using UnityEngine;

public class StraightShoot : IShootStrategy
{
    public BulletSpawner BulletSpawner { get; }

    public StraightShoot(BulletSpawner bulletSpawner)
    {
        BulletSpawner = bulletSpawner;
    }

    public void Use()
    {
        BulletSpawner.Shoot();
    }
}
