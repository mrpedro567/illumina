
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class IngameMenu : MonoBehaviour
{

    private InputAction _action;

    private void Start()
    {
        _action = new InputAction(binding: "<Keyboard>/Escape", type: InputActionType.Button);
        _action.Enable();
    }

    private void Update()
    {

        if (_action.triggered)
        {
            // Reativa a UI do jogo
            GameObject principalUI =  GameObject.Find("CanvasPrincipal");
            if (principalUI != null)
                principalUI.SetActive(true);

            SceneManager.UnloadSceneAsync(gameObject.scene.name);
        }
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Reload()
    {
        SceneManager.LoadScene("Map_1");
    }
}