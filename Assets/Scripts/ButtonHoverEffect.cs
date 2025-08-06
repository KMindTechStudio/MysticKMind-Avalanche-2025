using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    private Color originalColor;
    public Color hoverColor = Color.yellow;

    void Start()
    {
        button = GetComponent<Button>();
        originalColor = button.image.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        button.image.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        button.image.color = originalColor;
    }
}
