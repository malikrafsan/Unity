using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance {  get; private set; }

    private SaveDialogHandler saveDialog;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            this.saveDialog = FindObjectOfType<SaveDialogHandler>();
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
        saveDialog.Close();
    }

    public void LoadState(int Id)
    {
        var file = SaveLoadConfig.files[Id];
        var path = Application.persistentDataPath + "/" + file + ".json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Debug.Log("JSON:" + json);
            StateSave state = JsonUtility.FromJson<StateSave>(json);
            GlobalStateManager.Instance.SetState(state);
        }
    }
}
