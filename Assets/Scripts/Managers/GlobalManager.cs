using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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

    private int idxLoadSlot = -1;
    public int IdxLoadSlot
    {
        get => idxLoadSlot;
        set
        {
            idxLoadSlot = value;
            stateSave = SaveLoadManager.Instance.GetStateFromFile(idxLoadSlot);
            OnLoad();
        }
    }

    private StateSave stateSave = null;
    public StateSave StateSave
    {
        get => stateSave;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        globalStateManager = GlobalStateManager.Instance;
        DontDestroyOnLoad(gameObject);
    }

    private void OnLoad()
    {
        playerName = stateSave.playerStateSave.playerName;
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
