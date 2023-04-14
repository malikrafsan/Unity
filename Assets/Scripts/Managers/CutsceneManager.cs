using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public GameObject dialogueUI;
    public GameObject HUD;

    private void Start()
    {
        HUD.SetActive(false);
        dialogueUI.SetActive(true);
    }

   
}
