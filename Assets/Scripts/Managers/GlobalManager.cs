using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager Instance;
    private GlobalStateManager globalState;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        globalState = GlobalStateManager.Instance;
        DontDestroyOnLoad(gameObject);
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
