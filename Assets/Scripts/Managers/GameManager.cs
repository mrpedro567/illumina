using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
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
            SceneManager.LoadScene("Ingame_menu");
        }
    }
}