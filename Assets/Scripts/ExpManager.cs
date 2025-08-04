using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpManager : MonoBehaviour
{
    public int level;
    public int currentExp;
    public int expToLevel = 10;
    public float expGrowthMultiplier = 1.2f;
    public Slider expSlider;
    public TextMeshProUGUI currentLevelText;

    public static event Action<int> OnLevelUp;


    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GainExperience(2);
        }
    }

    private void OnEnable()
    {
        EnemyHealth.OnMonsterDefeated += GainExperience;
    }

    private void OnDisable()
    {
        EnemyHealth.OnMonsterDefeated -= GainExperience;
    }

    public void GainExperience(int amount)
    {
        currentExp += amount;
        if(currentExp >= expToLevel)
        {
            LevelUp();
        }

        UpdateUI();
    }

    private void LevelUp()
    {
        level++;
        currentExp -= expToLevel;
        expToLevel = Mathf.RoundToInt(expToLevel * expGrowthMultiplier);
        OnLevelUp?.Invoke(1);
    }

    public void UpdateUI()
    {
        expSlider.maxValue = expToLevel;
        expSlider.value = currentExp;
        currentLevelText.text = "Level: " + level;
    }
}
