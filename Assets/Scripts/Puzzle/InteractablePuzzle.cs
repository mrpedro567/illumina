using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractablePuzzle : MonoBehaviour
{
    public string nomeCenaPuzzle;  // Nome da cena do puzzle

    private bool playerDentro = false;

    void Update()
    {
        if (playerDentro && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(nomeCenaPuzzle);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerDentro = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerDentro = false;
    }
}
