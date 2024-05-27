using System.Collections;
using UnityEngine;

public class ShootRunner : MonoBehaviour
{
    [SerializeField] private BulletSpawner bulletSpawner;
    [SerializeField] private BulletSpawner tennisBulletSpawner;
    private IShootStrategy shootPattern;
    private IShootStrategy[] ShootStrategies;

    private void Awake()
    {
        ShootStrategies = new IShootStrategy[]
        {
            new ConeShoot(bulletSpawner),
            new StraightShoot(tennisBulletSpawner),
            new ShootDelayed(new ConeShoot(bulletSpawner), this as MonoBehaviour),
            new ShootComposite(new IShootStrategy[]
            {
                new ConeShoot(bulletSpawner),
                new ShootDelayed(new StraightShoot(tennisBulletSpawner), this as MonoBehaviour, 0.5f),
                new ShootDelayed(new ConeShoot(bulletSpawner), this as MonoBehaviour),
                new ShootDelayed(new StraightShoot(tennisBulletSpawner), this as MonoBehaviour, 1.5f)
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
