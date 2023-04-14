using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private LoadDialogHandler loadDialogHandler;

    private void Awake()
    {
        this.loadDialogHandler = FindObjectOfType<LoadDialogHandler>();
        this.loadDialogHandler.gameObject.SetActive(false);
    }

    public void OnClickNewGame()
    {
        SceneManager.LoadScene("Cutscene2");
    }

    public void OnClickLoadGame()
    {
/*        GlobalManager.Instance.IdxSaveSlot = 2;
        GlobalManager.Instance.IsFirstLoad = true;
        SceneManager.LoadScene("Quest");*/
        this.loadDialogHandler.gameObject.SetActive(true);
    }

    public void OnClickSettings()
    {

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
