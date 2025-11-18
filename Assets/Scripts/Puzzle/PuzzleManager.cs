using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;
    private InputAction _action;

    private int totalPieces;
    private int placedPieces = 0;

    void Awake()
    {
        Instance = this;
        _action = new InputAction(binding: "<Keyboard>/Escape", type: InputActionType.Button);
        _action.Enable();
    }

    private void Update()
    {
        if (_action.triggered)
        {
            // Reativa a UI do jogo
            GameObject principalUI = GameObject.Find("CanvasPrincipal");
            if (principalUI != null)
                principalUI.SetActive(true);

            // Fecha a cena do puzzle
            SceneManager.UnloadSceneAsync(gameObject.scene.name);
        }
    }

    public void SetTotalPieces(int total)
    {
        totalPieces = total;
    }

   public void PiecePlaced()
    {
        placedPieces++;

        if (placedPieces >= totalPieces)
        {
            Debug.Log("Puzzle concluído!");

            // Reativa a UI do jogo
            GameObject principalUI = GameObject.Find("CanvasPrincipal");
            if (principalUI != null)
                principalUI.SetActive(true);

            // Fecha a cena do puzzle
            SceneManager.UnloadSceneAsync(gameObject.scene.name);
        }
    }

}
