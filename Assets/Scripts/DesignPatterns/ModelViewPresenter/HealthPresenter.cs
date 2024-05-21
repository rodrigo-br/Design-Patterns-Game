using UnityEngine;
using UnityEngine.UI;

public class HealthPresenter : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Image healthBar;

    private void Start()
    {
        health.OnHealthChange += UpdateUI;
    }

    private void UpdateUI()
    {
        healthBar.fillAmount = health.GetHealthPercentage();
    }
}
