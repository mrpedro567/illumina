using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PuzzlePiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public Vector2 correctPosition; // posição correta vinda do ImageDivider

    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private bool isPlacedCorrectly = false;

    // Distância de tolerância para o encaixe (pode ajustar)
    private const float snapThreshold = 25f;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isPlacedCorrectly) return; // não permitir mover se já encaixou

        transform.SetAsLastSibling();
        canvasGroup.alpha = 0.8f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isPlacedCorrectly) return;

        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isPlacedCorrectly) return;

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        float distance = Vector2.Distance(rectTransform.anchoredPosition, correctPosition);

        if (distance < snapThreshold)
        {
            // ✅ Encaixe suave
            rectTransform.anchoredPosition = correctPosition;
            isPlacedCorrectly = true;

            // Desabilita o CanvasGroup para evitar interações futuras
            canvasGroup.blocksRaycasts = false;

        }
    }
}
