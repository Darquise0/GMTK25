using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI entryText;
    public void OnPointerEnter(PointerEventData eventData)
    {
        entryText.color = Color.darkRed;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        entryText.color = Color.black;
    }
}
