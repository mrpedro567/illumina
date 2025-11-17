using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PuzzlePiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public Vector2 correctPosition;

    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private bool isPlacedCorrectly = false;

    private const float snapThreshold = 25f;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isPlacedCorrectly) return;

        // Sempre coloca a peça sendo arrastada acima das outras
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
            // Encaixa no lugar certo
            rectTransform.anchoredPosition = correctPosition;
            isPlacedCorrectly = true;

            // Bloqueia totalmente interação
            canvasGroup.blocksRaycasts = false;

            // Peça correta deve ir PARA BAIXO de todas
            transform.SetAsFirstSibling();

            PuzzleManager.Instance.PiecePlaced();
        }
    }
}
