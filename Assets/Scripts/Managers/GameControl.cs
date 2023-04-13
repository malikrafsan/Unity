using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    // Static Reference
    private static string prefabPath = "Assets/Prefabs/GameControl.prefab";

    public static GameControl control;

    public static GameControl Control {
        set {
            control = value;
        }
        get {
            if (control == null) {
                // initialise reference game object
                Object gameControlRef = Resources.Load(prefabPath);
                GameObject controlObject = Instantiate(gameControlRef) as GameObject;

                if (control != null) {
                    control = controlObject?.GetComponent<GameControl>();

                    DontDestroyOnLoad(controlObject);
                }
            }
            return control;
        }   
    }

    private void Awake() {
        if (control != null) {
            Destroy(gameObject);
            return;
        }
        control = this;
        DontDestroyOnLoad(gameObject);
    }

    // Data to persist
    public float petCount = 0;
    public int currency = 0;
    public Item[] equipedWeapon = new Item[4];
    public bool cheatOneHitKill = false;

    // Currencies
    public void addCurrency(int amt) {
        currency += amt;
    }

    public bool isEnough(int amt) {
        return (currency >= amt);
    }

    public void minusCurrency(int amt) {
        currency -= amt;
    }

    public void addPet() {
        petCount ++;
    }
}
