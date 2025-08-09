using TMPro;
using UnityEngine;

public class QuestLogSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questNameText;
    [SerializeField] private TextMeshProUGUI questLevelText;

    public QuestSO currentQuest;

    public QuestLogUI questLogUI;

    private void OnValidate()
    {
        if(currentQuest != null)
            SetQuest(currentQuest);
        else
            gameObject.SetActive(false);
    }

    public void SetQuest(QuestSO questSO)
    {
        currentQuest = questSO;

        questNameText.text = questSO.questName;
        questLevelText.text = "Lv. "+ questSO.questLevel.ToString();

        gameObject.SetActive(true);
    }

    public void OnSlotClicked()
    {
        questLogUI.HandleQuestClicked(currentQuest);
    }
}
