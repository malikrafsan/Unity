using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    public HUD Hud;
    private string input;

    private void Awake() {
        Hud = GameObject.Find("HUDCanvas").GetComponent<HUD>();
    }

    private void FixedUpdate() {
        if (Input.GetKeyDown(KeyCode.C)) {
            Hud.ShowTextInput();
        }
    }

    public void ReadStringInput(string s) {
        input =  s;
        Debug.Log(input);
    }
}
