using UnityEngine;
using UnityEngine.UI;

public class LevelPresenter : MonoBehaviour
{
    [SerializeField] private Level level;

    [SerializeField] private Button increaseXpButton;
    [SerializeField] private Image experienceBar;

    private void Start()
    {
        increaseXpButton.onClick.AddListener(GainExperience);
        level.OnExperienceChange += UpdateUI;
        UpdateUI();
    }

    private void GainExperience()
    {
        level.GainExperience(10);
    }

    private void UpdateUI()
    {
        experienceBar.fillAmount = level.GetExperiencePercentage();
    }
}
