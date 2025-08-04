using TMPro;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;    
    public StatsUI statsUI;
    public TextMeshProUGUI healthText;

    [Header("Combat Stats")]
    public int damage;
    public int strenth;
    public int dexterity;
    public int agility;
    public int intelligence;
    public int magic;
    public int charisma;
    public float weaponRange;
    public float knockbackForce;
    public float knockbackTime;
    public float stunTime;

    [Header("Movement Stats")]
    public int speed;

    [Header("Health Stats")]
    public int maxHealth;
    public int currentHealth;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    public void UpdateMaxHealth(int amount)
    {
        maxHealth += amount;
        healthText.text = "HP: " + currentHealth + "/ " + maxHealth;
    }

    public void UpdateHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth >= maxHealth)
            currentHealth = maxHealth;

        healthText.text = "HP: " + currentHealth + "/ " + maxHealth;
    }

    public void UpdateSpeed(int amount)
    {
        speed += amount;
        statsUI.UpdateAllStats();
    }
}
