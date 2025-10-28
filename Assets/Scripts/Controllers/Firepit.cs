

using UnityEngine;

public class Firepit : NPC, Talkable
{

    [SerializeField] private Dialogue dialogue;
    [SerializeField] private DialogueController dialogueController;
    public override void Interact()
    {
        Talk(dialogue);
    }

    public void Talk(Dialogue dialogueText)
    {
        dialogueController.DisplayNextParagraph(dialogueText);
    }
}
