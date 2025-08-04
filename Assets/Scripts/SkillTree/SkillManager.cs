using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public PlayerCombat combat;

    private void OnEnable()
    {
        SkillSlot.OnAbilityPointSpent += HandleAbilityPointSpent;
    }

    private void OnDiable()
    {
        SkillSlot.OnAbilityPointSpent -= HandleAbilityPointSpent;
    }

    private void HandleAbilityPointSpent(SkillSlot slot)
    {
        string skillName = slot.skillSO.skillName;

        switch (skillName)
        {
            case "Health":
                StatsManager.Instance.UpdateMaxHealth(1);
                break;

            case "Sword Slash":
                combat.enabled = true;
                break;

            default:
                Debug.LogWarning("Unknow skill: " + skillName);
                break;
        }
    }
}
