using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public TextMeshProUGUI healthText;
    public Animator healthTextAnim;

    void Start()
    {
        healthText.text = "HP: " + currentHealth + "/" + maxHealth;
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        healthTextAnim.Play("TextUpdateAnim") ;

        healthText.text = "HP: " + currentHealth + "/" + maxHealth;

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

}
