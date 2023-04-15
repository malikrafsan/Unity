using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isSet = false;
    public bool IsSet { get => isSet; set => isSet = value; }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
    }

    public void OnClickMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        IsSet = true;
    }

    public void OnClickLatestSave()
    {
        GlobalManager.Instance.IsFirstLoad = true;
        SceneManager.LoadScene("Quest");
        IsSet = true;
    }
}
