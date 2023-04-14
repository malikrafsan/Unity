using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadSlotHandler
{
    public int Id { get; set; }
    private Button btn;
    private TMP_Text txt;

    public Button Btn
    {
        get { return btn; } 
        set
        {
            btn = value;
            txt = btn.GetComponentInChildren<TMP_Text>();
            btn.onClick.AddListener(() => OnClickBtn(Id));
        }
    }

    void OnClickBtn(int id)
    {
        // TODO
        GlobalManager.Instance.IdxSaveSlot = id;
        GlobalManager.Instance.IsFirstLoad = true;
        SceneManager.LoadScene("Quest");
    }

    public void Configure()
    {
        // TODO: check enabled, set and name
        var file = SaveLoadConfig.files[Id];
        var path = Application.persistentDataPath + "/" + file + ".json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            StateSave state = JsonUtility.FromJson<StateSave>(json);
            this.txt.text = state.metaStateSave.name;   
        }
        else
        {
            this.txt.text = "[EMPTY] Save Slot " + (Id + 1);
            this.Btn.enabled = false;
        }
    }
}

public class LoadDialogHandler : MonoBehaviour
{
    private LoadSlotHandler[] handlers = new LoadSlotHandler[3];

    private void Awake()
    {
        for (var i =0; i < handlers.Length; i++)
        {
            var slot = new LoadSlotHandler();
            slot.Id = i;

            var t = transform.Find("Canvas/Panel/Container/LoadSlot" + (i + 1));
            slot.Btn = t.GetComponent<Button>();
            slot.Configure();
        }
        /*gameObject.SetActive(false);*/
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
