using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    // Static Reference
    private static string prefabPath = "Assets/Prefabs/GameControl.prefab";

    public static GameControl control;

    // Do not know how this works :(
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
    public float health = 0;
    public int currency = 0;
    public Item[,] weapons = new Item[4,3];
    public Item[] equipedWeapon = new Item[4];

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

    // Weapons
    public void addWeaponToInventory(Item item) {
        int slot = slotWeapon(item);
        weapons[slot, (item.level-1)] = item;
    }

    public void equipWeapon(Item item) {
        if (item.type == "gun") {
            equipedWeapon[0] = item;
        }
        if (item.type == "shotgun") {
            equipedWeapon[1] = item;
        }
        if (item.type == "bow") {
            equipedWeapon[2] = item;
        }
        if (item.type == "sword") {
            equipedWeapon[3] = item;
        }
    }

    public int slotWeapon(Item item) {
        if (item.type == "gun") return 0;
        if (item.type == "shotgun") return 1;
        if (item.type == "bow") return 2;
        if (item.type == "sword") return 3;
        else return -1;
    }

    public int emptyLevel(int slot) {
        for (int y = 0; y < weapons.GetLength(1); y += 1) {
            if (weapons[slot, y].characterName == null) return (y+1);
        }
        return -1;
    }

}
