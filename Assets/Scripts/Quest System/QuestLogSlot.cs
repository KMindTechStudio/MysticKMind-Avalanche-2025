using TMPro;
using UnityEngine;

public class QuestLogSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questNameText;
    [SerializeField] private TextMeshProUGUI questLevelText;

    public QuestSO currentQuest;

    private void OnValidate()
    {
        if(currentQuest != null)
            SetQuest(currentQuest);
    }

    public void SetQuest(QuestSO questSO)
    {
        currentQuest = questSO;

        questNameText.text = questSO.questName;
        questLevelText.text = "Lv. "+ questSO.questLevel.ToString();
    }
}
