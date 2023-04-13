using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMover : MonoBehaviour
{
    public string SceneName = "MainMenu";
    
    void OnEnable()
    {
        SceneManager.LoadSceneAsync(SceneName);
    }
}
