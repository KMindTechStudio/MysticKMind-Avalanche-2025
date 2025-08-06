using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI Reference")]
    public CanvasGroup canvasGroup;
    public Image portrait;
    public TextMeshProUGUI actorName;
    public TextMeshProUGUI dialogueText;
    public Button[] choiceButtons;

    public bool isDialogueActive;

    private DialogueSO currentDialogue;
    private int dialogueIndex;

    private float lastDialogueEndTime;
    private float dialogueCooldown = .1f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        foreach(var button in choiceButtons)
            button.gameObject.SetActive(false);
    }

    public void StartDialogue(DialogueSO dialogueSO)
    {
        if (Time.unscaledTime - lastDialogueEndTime < dialogueCooldown)
            return;

        currentDialogue = dialogueSO;
        dialogueIndex = 0;
        isDialogueActive = true;
        ShowDialogue();
    }

    public void AdvanceDialogue()
    {
        if (currentDialogue == null)
        {
            Debug.LogWarning("Không có hội thoại nào đang diễn ra.");
            return;
        }

        if (dialogueIndex < currentDialogue.lines.Length)
            ShowDialogue();
        else
            ShowChoices();
    }

    private void ShowDialogue()
    {
        DialogueLine line = currentDialogue.lines[dialogueIndex];

        DialogueHistoryTracker.Instance.RecordNPC(line.speaker);

        portrait.sprite = line.speaker.portrait;
        actorName.text = line.speaker.actorName;

        dialogueText.text = line.text;

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        dialogueIndex++;
    }

    private void ShowChoices()
    {
        ClearChoices();

        if(currentDialogue.options.Length > 0)
        {
            for(int i = 0; i < currentDialogue.options.Length; i++)
            {
                var option = currentDialogue.options[i];

                choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = option.optionText;
                choiceButtons[i].gameObject.SetActive(true);

                choiceButtons[i].onClick.AddListener(() => ChooseOption(option.nextDialogue));
            }
        }
        else
        {
            choiceButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = "End";
            choiceButtons[0].onClick.AddListener(EndDialogue); 
            choiceButtons[0].gameObject.SetActive(true);
        }

        EventSystem.current.SetSelectedGameObject(choiceButtons[0].gameObject);
    }

    private void ChooseOption(DialogueSO dialogueSO)
    {
        if (dialogueSO == null)
            EndDialogue();
        else
        {
            ClearChoices();
            StartDialogue(dialogueSO);
        }
    }


    private void EndDialogue()
    {
        dialogueIndex = 0;
        isDialogueActive = false;
        ClearChoices();

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        lastDialogueEndTime = Time.unscaledTime;
    }

    private void ClearChoices()
    {
        foreach(var button in choiceButtons)
        {
            button.gameObject.SetActive(false);
            button.onClick.RemoveAllListeners();
        }
    }
}
