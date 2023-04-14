using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization;
using System;

public class SaveSlotHandler
{
    public int Id { get; set; }

    private TMP_InputField inputField;
    public TMP_InputField InputField
    {
        get => inputField;
        set
        {
            inputField = value;
        }
    }

    private Button btn;
    public Button Btn
    {
        get { return btn; } 
        set 
        {
            btn = value;
            btn.onClick.AddListener(() => OnClickBtn(Id));
        }
    }

    void OnClickBtn(int Id)
    {
        Debug.Log("Btn Clicked " + Id);
        SaveLoadManager.Instance.SaveState(Id, this.inputField.text);
    }

    public void SetDefault()
    {
        var file = SaveLoadConfig.files[Id];
        var path = Application.persistentDataPath + "/" + file + ".json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            StateSave state = JsonUtility.FromJson<StateSave>(json);
            this.InputField.text = state.metaStateSave.name;
        }
        else
        {
            this.InputField.text = DateTime.Now.ToString(SaveLoadConfig.dateTimeFormat);
        }
    }
}

public class SaveDialogHandler : MonoBehaviour
{
    private SaveSlotHandler[] saveSlotHandlers = new SaveSlotHandler[3];

    private void Awake()
    {
        // this.gameObject.SetActive(false);
        for (var i = 0; i < saveSlotHandlers.Length; i++)
        {
            var slot = new SaveSlotHandler();
            slot.Id = i;

            Debug.Log("Iterating i" + i);
            
            var t = transform.Find("PanelSaveDialog/Container/SaveSlot" + (i+1));
            Debug.Log("transform: " + t);
            slot.InputField = t.GetComponentInChildren<TMP_InputField>();
            slot.Btn = t.GetComponentInChildren<Button>();

            // add event listener to input field on change value

            this.saveSlotHandlers[i] = slot;
        }
        /*        var children = this.gameObject.GetComponentsInChildren<GameObject>();
                foreach (var child in children)
                {
                    child.gameObject.SetActive(false);
                }*/
        Close();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show()
    {
        var children = transform.childCount;
        for (var i = 0; i < children; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void Close()
    {
        var children = transform.childCount;
        for (var i = 0; i < children; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        foreach (var slot in this.saveSlotHandlers)
        {
            slot.SetDefault();
        }
    }
}
