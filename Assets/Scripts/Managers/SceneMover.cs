using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMover : MonoBehaviour
{
    public string SceneName = "MainMenu";
    public string Argument = "";
    void OnEnable()
    {
        if (Argument.Equals("Leaderboard"))
        {
            GlobalManager.Instance.isFromEnding = true;
        }
        SceneManager.LoadSceneAsync(SceneName);
    }
}
