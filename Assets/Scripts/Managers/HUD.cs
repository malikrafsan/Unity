using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public GameObject MessagePanel;
    private Text textTransform;
    [SerializeField] GameObject textInput;
    private GameOverCanvas gameOverCanvas;

    private void Awake() {
        textTransform = MessagePanel.transform.Find("Text").GetComponent<Text>();
        gameOverCanvas = FindObjectOfType<GameOverCanvas>();
        gameOverCanvas.gameObject.SetActive(false);
    }

    public void OpenMessagePanel (string text) {
        // Set the text of the message panel for other messages
        textTransform.text = text;
        MessagePanel.SetActive(true);
        StartCoroutine(executeAfter(3));
    }

    public void OpenPermanantMessage (string text) {
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

    private IEnumerator executeAfter(int secs) {
        yield return new WaitForSeconds(secs);
        CloseMessagePanel();
    }

    public IEnumerator GameOverHandler()
    {
        gameOverCanvas.gameObject.SetActive(true);

        yield return new WaitForSeconds(5);
        if (!gameOverCanvas.IsSet)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
