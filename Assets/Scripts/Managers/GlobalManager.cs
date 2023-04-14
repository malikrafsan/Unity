using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager Instance;
    private GlobalStateManager globalStateManager;
    private string playerName;
    public string PlayerName
    {
        get => playerName;
        set
        {
            playerName = value;
        }
    }

    private MetaStateSave metaStateSave;
    public MetaStateSave MetaStateSave
    {
        get => metaStateSave;
        set
        {
            metaStateSave = value;
        }
    }

    private double timePlayed;
    public double TimePlayed
    {
        get => timePlayed;
        set
        {
            timePlayed = value;
        }
    }

    private bool isFirstLoad = false;
    public bool IsFirstLoad
    {
        get => isFirstLoad;
        set
        {
            isFirstLoad = value;
        }
    }

    private int idxSaveSlot = -1;
    public int IdxSaveSlot
    {
        get => idxSaveSlot;
        set
        {
            idxSaveSlot = value;
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        OnAwake();
        DontDestroyOnLoad(gameObject);
    }

    private void OnAwake()
    {
        globalStateManager = GlobalStateManager.Instance;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == "Quest" && isFirstLoad)
        {
            Debug.Log("ON SCENE LOADED");
            SaveLoadManager.Instance.LoadState(idxSaveSlot);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
/*        if (Input.GetKeyDown(KeyCode.B))
        {
            SaveState();
        }   

        if (Input.GetKeyDown(KeyCode.N))
        {
            LoadState();
        }*/
    }
}
