using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField nameInput;
    [SerializeField] 
    private GameObject volumeInput;
    // Start is called before the first frame update
    void Start()
    {
        nameInput.text = getGlobalName();
        (volumeInput.GetComponent<Slider>()).value = GlobalManager.Instance.Volume;
    }

    // Update is called once per frame
    void Update()
    {
        GlobalManager.Instance.PlayerName = nameInput.text;
        var volume = (volumeInput.GetComponent<Slider>()).value * 0.1f;
        GlobalManager.Instance.Volume = volume;
        AudioListener.volume = volume;

        Debug.Log("PLAYER NAME: " + GlobalManager.Instance.PlayerName);
        Debug.Log("VOLUME: " + GlobalManager.Instance.Volume);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
    }

    string getGlobalName()
    {
        var name = GlobalManager.Instance.PlayerName;
        if (name == null) return "";
        return name;
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
