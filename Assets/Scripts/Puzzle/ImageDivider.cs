using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageDivider : MonoBehaviour
{
    [Header("Configuração da Imagem")]
    public Sprite imageToDivide;
    public int rows = 3;
    public int cols = 3;

    [Header("Prefabs e Layout")]
    public GameObject piecePrefab;
    public RectTransform puzzleParent;

    private List<GameObject> pieces = new List<GameObject>();

    private float panelWidth;
    private float panelHeight;

    void Start()
    {
        StartCoroutine(InitializePuzzle());
    }

    private System.Collections.IEnumerator InitializePuzzle()
    {
        yield return null; // espera um frame para garantir layout atualizado
        LayoutRebuilder.ForceRebuildLayoutImmediate(puzzleParent);

        panelWidth = puzzleParent.rect.width;
        panelHeight = puzzleParent.rect.height;

        GeneratePuzzle();
        ShufflePieces();
    }

    void GeneratePuzzle()
{
    // Textura original do sprite
    Texture2D texture = imageToDivide.texture;
    float texWidth = texture.width;
    float texHeight = texture.height;

    // Obtem o RectTransform do painel onde as peças serão colocadas
    RectTransform parentRect = puzzleParent.GetComponent<RectTransform>();
    // Largura/altura do painel em unidades de UI (pixels no modo Screen Space - Overlay)
    float panelWidth = parentRect.rect.width;
    float panelHeight = parentRect.rect.height;

    // Opcional: padding interno (em pixels) para não encostar nas bordas
    float padding = 8f;
    float availableW = Mathf.Max(0.0f, panelWidth - padding * 2f);
    float availableH = Mathf.Max(0.0f, panelHeight - padding * 2f);

    // Calcula escala para caber dentro do painel mantendo proporção
    float scale = Mathf.Min(availableW / texWidth, availableH / texHeight);

    // Tamanho da imagem escalada em pixels UI
    float scaledWidth = texWidth * scale;
    float scaledHeight = texHeight * scale;

    // Tamanho de cada peça no UI (em pixels)
    float pieceWidthUI = scaledWidth / cols;
    float pieceHeightUI = scaledHeight / rows;

    // Limpa lista/filhos antigos se necessário
    foreach (var p in pieces) Destroy(p);
    pieces.Clear();

    // Força anchors/pivot do parent para centro (garante referência consistente)
    // (não altera se você já configurou no Editor)
    parentRect.pivot = new Vector2(0.5f, 0.5f);
    parentRect.anchorMin = parentRect.anchorMax = new Vector2(0.5f, 0.5f);

    // Posição inicial (centro do painel = (0,0) para anchoredPosition com pivô central)
    // Calculamos o canto superior-esquerdo relativo ao centro:
    float startX = -scaledWidth / 2f + pieceWidthUI / 2f;
    float startY = scaledHeight / 2f - pieceHeightUI / 2f; // note: Y positivo para cima

    for (int y = 0; y < rows; y++)
    {
        for (int x = 0; x < cols; x++)
        {
            GameObject piece = Instantiate(piecePrefab, puzzleParent);
            piece.name = $"Piece_{x}_{y}";

            // Garante escala local 1 para evitar herança estranha
            piece.transform.localScale = Vector3.one;

            Image img = piece.GetComponent<Image>();

            // Seleciona a parte correta da textura
            // Observação: Texturas têm origem no canto inferior esquerdo,
            // então usamos (rows - 1 - y) para mapear linhas top->bottom.
            Rect rect = new Rect(
                x * (texWidth / cols),
                (rows - 1 - y) * (texHeight / rows),
                texWidth / cols,
                texHeight / rows
            );

            // Cria sprite para a peça
            img.sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f), 100f);

            RectTransform rt = piece.GetComponent<RectTransform>();

            // Força anchors para central (para trabalhar com anchoredPosition)
            rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
            rt.pivot = new Vector2(0.5f, 0.5f);

            // Define o tamanho da peça no UI (em pixels)
            rt.sizeDelta = new Vector2(pieceWidthUI, pieceHeightUI);

            // Calcula a posição relativa ao centro do painel
            float posX = startX + x * pieceWidthUI;
            float posY = startY - y * pieceHeightUI; // subtrai y porque startY é top

            rt.anchoredPosition = new Vector2(posX, posY);

            // Define a posição correta no script da peça
            PuzzlePiece pieceScript = piece.GetComponent<PuzzlePiece>();
            pieceScript.correctPosition = rt.anchoredPosition;

            pieces.Add(piece);
        }
    }
}



    void ShufflePieces()
    {
        // Guarda as posições originais
        List<Vector2> positions = new List<Vector2>();
        foreach (var piece in pieces)
            positions.Add(piece.GetComponent<RectTransform>().anchoredPosition);

        // Embaralha
        for (int i = 0; i < pieces.Count; i++)
        {
            int randomIndex = Random.Range(0, positions.Count);
            pieces[i].GetComponent<RectTransform>().anchoredPosition = positions[randomIndex];
            positions.RemoveAt(randomIndex);
        }
    }
}
