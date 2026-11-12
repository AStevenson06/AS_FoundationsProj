using UnityEngine;
using UnityEngine.EventSystems;

public class QuitButtonHover : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler
{
    public float hoverScale = 1.25f;
    public float animationSpeed = 8f;

    private RectTransform textTransform;
    private Vector3 originalScale;
    private Vector3 targetScale;

    void Start()
    {
        textTransform = transform.Find("Text (TMP)")
            .GetComponent<RectTransform>();

        originalScale = textTransform.localScale;
        targetScale = originalScale;
    }

    void Update()
    {
        if (textTransform == null)
            return;

        textTransform.localScale = Vector3.Lerp(
            textTransform.localScale,
            targetScale,
            Time.unscaledDeltaTime * animationSpeed
        );
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("HOVER DETECTED!");
        targetScale = originalScale * hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("HOVER ENDED!");
        targetScale = originalScale;
    }
}