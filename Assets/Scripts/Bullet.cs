using DG.Tweening;
using System;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolable
{
    private float bulletSpeed = 500;
    private float maxBulletDuration = 5;
    private Rigidbody2D myRigidbody2D;
    private float currentBulletDuration;
    private Action<IPoolable> disableCallback;
    private Vector2 direction;

    private void Awake()
    {
        myRigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!this.enabled) return;

        myRigidbody2D.velocity = bulletSpeed * Time.deltaTime * direction;

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
            return;
        }
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        if (rb != null && !rb.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Ammo"))
        {
            disableCallback?.Invoke(this);
        }
    }

    public void SetDisableCallbackAction(Action<IPoolable> callback)
    {
        disableCallback = callback;
    }

    public void StartBullet(float speed, float duration, Vector2 position, Quaternion rotation, bool rotating = true)
    {
        this.gameObject.transform.SetPositionAndRotation(position, rotation);
        direction = gameObject.transform.right;
        currentBulletDuration = 0;
        bulletSpeed = speed;
        maxBulletDuration = duration;
        this.gameObject.SetActive(true);
        if (rotating)
        {
            myRigidbody2D.DORotate(myRigidbody2D.rotation + UnityEngine.Random.Range(90, 620), duration);
        }
    }
}
