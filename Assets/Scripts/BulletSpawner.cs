using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    private enum PrefabPath
    {
        Bullet_TennisBall,
        Bullet_Bone,
        Bullet
    }
    [SerializeField] private PrefabPath prefabPath;
    [SerializeField] private float bulletSpeed = 500;
    [SerializeField] private float maxBulletDuration = 5;
    [SerializeField] private int initialPoolSize = 20;
    private ObjectPooling<Bullet> bulletPool;

    private void Awake()
    {
        bulletPool = new ObjectPooling<Bullet>("Prefabs/" + prefabPath.ToString(), initialPoolSize);
    }

    public void Shoot()
    {
        BulletRent();
    }

    private void BulletRent()
    {
        Bullet bullet = bulletPool.Rent();
        bullet.StartBullet(bulletSpeed, maxBulletDuration, transform.position, transform.rotation);
    }

    public void SetObjectPooling(ObjectPooling<Bullet> objectPooling)
    {
        bulletPool = objectPooling;
    }
}
