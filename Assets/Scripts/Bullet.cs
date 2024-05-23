using System;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolable
{
    private float bulletSpeed = 500;
    private float maxBulletDuration = 5;
    private Rigidbody2D myRigidbody2D;
    private float currentBulletDuration;
    private Action<IPoolable> disableCallback;

    private void Awake()
    {
        myRigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!this.enabled) return;

        myRigidbody2D.velocity = Vector2.right * bulletSpeed * Time.deltaTime;

        currentBulletDuration += Time.deltaTime;
        if (currentBulletDuration >= maxBulletDuration)
        {
            disableCallback?.Invoke(this);
        }
    }

    private void OnDisable()
    {
        myRigidbody2D.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.Damage();
        }
    }

    public void SetDisableCallbackAction(Action<IPoolable> callback)
    {
        disableCallback = callback;
    }

    public void StartBullet(float speed, float duration, Vector2 position)
    {
        this.transform.position = position;
        currentBulletDuration = 0;
        bulletSpeed = speed;
        maxBulletDuration = duration;
        this.gameObject.SetActive(true);
    }
}
