using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Rendering;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance {  get; private set; }


    private SaveDialogHandler _saveDialog;
    public SaveDialogHandler saveDialog
    {
        get
        {
            if (_saveDialog == null)
            {
                _saveDialog = GameObject.Find("HUDCanvas").GetComponentInChildren<SaveDialogHandler>();
            }

            return _saveDialog;
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowUI()
    {
        this.saveDialog.Show();
    }

    public void CloseUI()
    {
        this.saveDialog.Close();
    }

    public void SaveState(int Id, string name)
    {
        var state = GlobalStateManager.Instance.GetState();
        state.metaStateSave.name = name;
        state.metaStateSave.Update();

        var json = JsonUtility.ToJson(state);

        var file = SaveLoadConfig.files[Id];
        var path = Application.persistentDataPath + "/" + file + ".json";
        File.WriteAllText(path, json);
        GlobalManager.Instance.IdxSaveSlot = Id;
        saveDialog.Close();
    }

    public void LoadState(int Id)
    {
        var state = GetSavedStateFromFile(Id);
        state.playerStateSave.playerName = GlobalManager.Instance.PlayerName;
        if (state != null)
        {
            Debug.Log("Load State " + Id);
            GlobalStateManager.Instance.SetState(state);
        }
    }

    public StateSave GetSavedStateFromFile(int id)
    {
        var file = SaveLoadConfig.files[id];
        var path = Application.persistentDataPath + "/" + file + ".json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Debug.Log("JSON:" + json);
            StateSave state = JsonUtility.FromJson<StateSave>(json);
            return state;
        }

        return null;
    }
}
