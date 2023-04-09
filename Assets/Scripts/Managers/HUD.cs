using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public GameObject MessagePanel;
    private Text textTransform;
    [SerializeField] GameObject textInput;

    private void Awake() {
        textTransform = MessagePanel.transform.Find("Text").GetComponent<Text>();
    }

    public void OpenMessagePanel (string text) {
        // Set the text of the message panel for other messages
        textTransform.text = text;
        MessagePanel.SetActive(true);
    }

    public void CloseMessagePanel () {
        MessagePanel.SetActive(false);
    }

    public void ShowTextInput() {
        textInput.SetActive(true);
    }

    public void CloseTextInput() {
        textInput.SetActive(false);
    }
}
