using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class InteractableTrigger : MonoBehaviour
{
    public GameObject uiCanvasPrincipal;
    public string nomeCenaPuzzle;
    public GameObject mensagemPressioneE; // referência para a UI

    private bool playerDentro = false;

    void Update()
    {
        if (playerDentro && Keyboard.current.eKey.wasPressedThisFrame)
        {
            uiCanvasPrincipal.SetActive(false);
            SceneManager.LoadScene(nomeCenaPuzzle, LoadSceneMode.Additive);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerDentro = true;
            if (mensagemPressioneE != null)
                mensagemPressioneE.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerDentro = false;
            if (mensagemPressioneE != null)
                mensagemPressioneE.SetActive(false);
        }
    }
}
