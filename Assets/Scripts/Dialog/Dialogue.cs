using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Dialogue/New Dialog Container")]
public class Dialogue : ScriptableObject
{
    public string speakerName;

    [TextArea(5,10)]
    public string[] paragraphs;

    //Image ? 
}
