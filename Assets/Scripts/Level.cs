using System;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private float pointsPerLevel = 100;
    private float experiencePoints = 0;
    public event Action OnLevelUp;
    public event Action OnExperienceChange;

    public void GainExperience(int points)
    {
        int level = GetLevel();
        experiencePoints += points;
        OnExperienceChange?.Invoke();
        if (GetLevel() > level)
        {
            OnLevelUp?.Invoke();
        }
    }

    public float GetExperiencePercentage()
    {
        return experiencePoints % pointsPerLevel / pointsPerLevel;
    }

    public int GetLevel()
    {
        return (int)(experiencePoints / pointsPerLevel);
    }

}
