using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 500;
    [SerializeField] private float maxBulletDuration = 5;
    private ObjectPooling<Bullet> bulletPool;
    private const int INITIAL_POOL_SIZE = 20;

    private void Awake()
    {
        bulletPool = new ObjectPooling<Bullet>(Constants.BULLET_PREFAB, INITIAL_POOL_SIZE);
    }

    public void Shoot()
    {
        BulletRent();
    }

    private void BulletRent()
    {
        Bullet bullet = bulletPool.Rent();
        bullet.transform.SetPositionAndRotation(transform.position, transform.rotation);
        bullet.StartBullet(bulletSpeed, maxBulletDuration, this.transform.position);
    }
}
