using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI NPCNameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private float typeSpeed = 10f;

    private bool ended = false;
    private bool isTyping = false;

    private Queue<string> paragraphs = new Queue<string>();
    private Coroutine typeDialogueCoroutine;

    private string p;

    public void DisplayNextParagraph(Dialogue dialogue)
    {
        if (paragraphs.Count == 0)
        {
            if (ended)
            {
                EndConversation();
                return;
            }
            else
            {
                StartConversation(dialogue);
            }
        }

        if (!isTyping)
        {
            p = paragraphs.Dequeue();

            typeDialogueCoroutine = StartCoroutine(TypeDialogueText(p));
        }

        if(paragraphs.Count == 0)
        {
            ended = true;
        }
    }

    private void StartConversation(Dialogue dialogue)
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }

        NPCNameText.text = dialogue.speakerName;
        for (int i = 0; i < dialogue.paragraphs.Length; i++) { 
            paragraphs.Enqueue(dialogue.paragraphs[i]);
        }
    }

    private void EndConversation()
    {
        paragraphs.Clear();
        ended = false;

        if (gameObject.activeSelf) {
            gameObject.SetActive(false);
        }
    }

    private IEnumerator TypeDialogueText(string p)
    {
        float elapsedTime = 0f;
        int index = 0;

        index = Mathf.Clamp(index, 0, p.Length);

        while (index < p.Length) { 
            elapsedTime += Time.deltaTime * typeSpeed;
            index = Mathf.FloorToInt(elapsedTime);

            dialogueText.text = p.Substring(0, index);
            yield return null;
        }

        dialogueText.text = p;
    }
}
