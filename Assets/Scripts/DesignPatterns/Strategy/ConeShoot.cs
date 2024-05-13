using UnityEngine;

public class ConeShoot : IShootStrategy
{
    public BulletSpawner BulletSpawner { get; }
    private const float CONE_ANGLE = 30f;

    public ConeShoot(BulletSpawner bulletSpawner)
    {
        BulletSpawner = bulletSpawner;
    }

    public void Use()
    {
        Quaternion originalRotation = BulletSpawner.transform.rotation;

        BulletSpawner.transform.eulerAngles = new Vector3(0, 0, originalRotation.eulerAngles.z + CONE_ANGLE);
        BulletSpawner.Shoot();

        BulletSpawner.transform.eulerAngles = new Vector3(0, 0, originalRotation.eulerAngles.z - CONE_ANGLE);
        BulletSpawner.Shoot();

        BulletSpawner.transform.rotation = originalRotation;
        BulletSpawner.Shoot();

    }
}
