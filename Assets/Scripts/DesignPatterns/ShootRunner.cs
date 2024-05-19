using System.Collections;
using UnityEngine;

public class ShootRunner : MonoBehaviour
{
    private BulletSpawner bulletSpawner;
    private IShootStrategy shootPattern;
    private IShootStrategy[] ShootStrategies;

    private void Awake()
    {
        bulletSpawner = GetComponent<BulletSpawner>();
        ShootStrategies = new IShootStrategy[]
        {
            new StraightShoot(bulletSpawner),
            new ConeShoot(bulletSpawner),
            new ShootDelayed(new ConeShoot(bulletSpawner), this as MonoBehaviour),
            new ShootComposite(new IShootStrategy[]
            {
                new ConeShoot(bulletSpawner),
                new ShootDelayed(new ConeShoot(bulletSpawner), this as MonoBehaviour),
                new StraightShoot(bulletSpawner),
            })
        };
        shootPattern = ShootStrategies[0];
    }

    private void OnEnable()
    {
        StartCoroutine(AssignInputs());
    }

    private IEnumerator AssignInputs()
    {
        yield return new WaitUntil(() => GameManager.Instance != null);
        GameManager.Instance.InputObserver.OnShoot += Shoot;
        GameManager.Instance.InputObserver.OnTestSelection += (index) => SetShootPattern(index);
    }

    private void SetShootPattern(int index)
    {
        shootPattern = ShootStrategies[index];
    }

    private void OnDisable()
    {
        GameManager.Instance.InputObserver.OnShoot -= Shoot;
        GameManager.Instance.InputObserver.OnTestSelection -= SetShootPattern;
    }

    public void Shoot()
    {
        shootPattern.Use();
    }
}
