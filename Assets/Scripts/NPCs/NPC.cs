using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC-Wizard file", menuName="NPC Wizard Files Archive")]
public class NPC : ScriptableObject
{
    public string name;
    [TextArea(3, 15)]
    public string[] dialogues;
    [TextArea(3, 15)]
    public string[] playerDialogues;
}
