using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlace : MonoBehaviour
{
    private bool onArea = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (onArea && Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("SAVING");
            if (SaveLoadManager.Instance == null )
            {
                Debug.Log("NULL INSTANCE");
            }
            SaveLoadManager.Instance.ShowUI();
        }

        if (onArea && Input.GetKeyDown(KeyCode.N))
        {
            SaveLoadManager.Instance.LoadState(2);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var onquest = GlobalStateManager.Instance.OnQuest;
        if (other.CompareTag("Player") && !onquest)
        {
            ToastManager.Instance.ShowToast("Press B to save", 1);
            onArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onArea = false;
            SaveLoadManager.Instance.CloseUI();
        }
    }
}
