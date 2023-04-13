using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void OnClickNewGame()
    {
        SceneManager.LoadScene("Cutscene2");
    }

    public void OnClickLoadGame()
    {
        SceneManager.LoadScene("Quest");
    }

    public void OnClickSettings()
    {

    }
}
