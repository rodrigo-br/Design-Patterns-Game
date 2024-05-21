using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float fullHealth = 100f;
    [SerializeField] private float drainPerSecond = 2f;
    [SerializeField] private Level level;
    private float currentHealth = 0;
    public event Action OnHealthChange;

    private void Awake()
    {
        ResetHealth();
        StartCoroutine(HealthDrain());
    }

    private void OnEnable()
    {
        level.OnLevelUp += ResetHealth;
    }

    private void OnDisable()
    {
        level.OnLevelUp -= ResetHealth;
    }

    public float GetHealthPercentage()
    {
        return currentHealth / fullHealth;
    }

    void ResetHealth()
    {
        currentHealth = fullHealth;
        OnHealthChange?.Invoke();
    }

    private IEnumerator HealthDrain()
    {
        while (currentHealth > 0)
        {
            currentHealth -= drainPerSecond;
            OnHealthChange?.Invoke();
            yield return new WaitForSeconds(1);
        }
    }

}
