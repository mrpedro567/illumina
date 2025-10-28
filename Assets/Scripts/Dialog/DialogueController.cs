using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI NPCNameText;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private bool ended = false;

    private Queue<string> paragraphs = new Queue<string>();

    public void DisplayNextParagraph(Dialogue dialogue)
    {
        if (paragraphs.Count > 0)
        {
            if (ended)
            {
                EndConversation();
            }
            else
            {
                StartConversation(dialogue);
            }
        }
    }

    private void StartConversation(Dialogue dialogue)
    {
        gameObject.SetActive(true);

        NPCNameText.text = dialogue.speakerName;
        for (int i = 0; i < dialogue.paragraphs.Length; i++) { 
            paragraphs.Enqueue(dialogue.paragraphs[i]);
        }
    }

    private void EndConversation()
    {

    }
}
