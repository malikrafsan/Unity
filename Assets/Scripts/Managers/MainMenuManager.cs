using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private LoadDialogHandler loadDialogHandler;
    private SettingManager settingPanel;
    private ScoreBoard scoreboard;

    private void Awake()
    {
        this.loadDialogHandler = FindObjectOfType<LoadDialogHandler>();
        this.loadDialogHandler.gameObject.SetActive(false);
        this.settingPanel = FindObjectOfType<SettingManager>();
        this.settingPanel.gameObject.SetActive(false);
        this.scoreboard = FindObjectOfType<ScoreBoard>();
        this.scoreboard.gameObject.SetActive(false);
    }

    public void OnClickNewGame()
    {
        SceneManager.LoadScene("Cutscene2");
    }

    public void OnClickLoadGame()
    {
        this.loadDialogHandler.gameObject.SetActive(true);
    }

    public void OnClickSettings()
    {
        this.settingPanel.gameObject.SetActive(true);
    }

    public void OnClickLeaderboard()
    {
        this.scoreboard.gameObject.SetActive(true) ;
    }

    private void Update()
    {
        var gm = GlobalManager.Instance;
        if (gm != null)
        {
            Debug.Log("NOT NULL");
        }
        else
        {
            Debug.Log("null");
        }
    }
}
