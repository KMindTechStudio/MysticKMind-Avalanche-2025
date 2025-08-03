using TMPro;
using UnityEngine;

public class StatsUI : MonoBehaviour
{
    public GameObject[] statsSlots;
    public CanvasGroup statsCanvas;

    private bool statsOpen = false;

    private void Start()
    {
        UpdateAllStats();
    }

    private void Update()
    {
        if (Input.GetButtonDown("ToggleStats"))
            if(statsOpen)
            {
                Time.timeScale = 1;
                UpdateAllStats();
                statsCanvas.alpha = 0;
                statsOpen = false;
            }
            else 
            { 
                Time.timeScale = 0;
                UpdateAllStats();
                statsCanvas.alpha = 1;
                statsOpen = true;
            }
    }

    public void UpdateDamage()
    {
        statsSlots[0].GetComponentInChildren<TextMeshProUGUI>().text = "Damage: " + StatsManager.Instance.damage;
    }

    public void UpdateSpeed()
    {
        statsSlots[1].GetComponentInChildren<TextMeshProUGUI>().text = "Speed: " + StatsManager.Instance.speed;
    }

    public void UpdateStrenth()
    {
        statsSlots[2].GetComponentInChildren<TextMeshProUGUI>().text = "Strenth: " + StatsManager.Instance.strenth;
    }

    public void UpdateDexterity()
    {
        statsSlots[3].GetComponentInChildren<TextMeshProUGUI>().text = "Dexterity: " + StatsManager.Instance.dexterity;
    }

    public void UpdateAgility()
    {
        statsSlots[4].GetComponentInChildren<TextMeshProUGUI>().text = "Agility: " + StatsManager.Instance.agility;
    }

    public void UpdateIntelligence()
    {
        statsSlots[5].GetComponentInChildren<TextMeshProUGUI>().text = "Intelligence: " + StatsManager.Instance.intelligence;
    }

    public void UpdateMagic()
    {
        statsSlots[6].GetComponentInChildren<TextMeshProUGUI>().text = "Magic: " + StatsManager.Instance.magic;
    }

    public void UpdateCharisma()
    {
        statsSlots[7].GetComponentInChildren<TextMeshProUGUI>().text = "Charisma: " + StatsManager.Instance.charisma;
    }

    public void UpdateAllStats()
    {
        UpdateDamage();
        UpdateSpeed();
        UpdateStrenth();
        UpdateDexterity();
        UpdateAgility();
        UpdateIntelligence();
        UpdateMagic();
        UpdateCharisma();
    }
}
