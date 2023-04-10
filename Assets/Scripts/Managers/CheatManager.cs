using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    public HUD Hud;
    public PlayerHealth playerHealth;
    public PlayerMovement playerMovement;
    public PlayerWeapons playerWeapon;
    public GameObject petShop;
    public GameObject weaponShop;
    public ItemShopUI petShopUI;
    public ItemShopUI weaponShopUI;
    private string input;
    private int prevCurrency;
    private bool motherlodeOn = false;

    private void Awake() {
        Hud = GameObject.Find("HUDCanvas").GetComponent<HUD>();
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerWeapon = GameObject.Find("Player").GetComponent<PlayerWeapons>();
        petShopUI = petShop.GetComponent<ItemShopUI>();
        weaponShopUI = weaponShop.GetComponent<ItemShopUI>();
    }

    private void FixedUpdate() {
        if (Input.GetKeyDown(KeyCode.C)) {
            Hud.ShowTextInput();
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Hud.CloseTextInput();
        }
    }

    public void ReadStringInput(string s) {
        input =  s;
        compareString();
    }

    public void compareString() {
        if (input == "NODAMAGE") {
            playerHealth.SetCheatNoDamage(true);
            Hud.OpenMessagePanel("Cheat No Damage Activated!");
            return;
        }
        if (input == "ONEHITKILL") {
            // BETTER IF SET DAMAGE TO 9999999
            Hud.OpenMessagePanel("Cheat One Hit Kill Activated!");
            return;
        }
        if (input == "MOTHERLODE") {
            Hud.OpenMessagePanel("Cheat Motherlode Activated!");
            this.prevCurrency = GameControl.control.currency;
            this.motherlodeOn = true;
            // TODO: Handle overflow currency
            GameControl.control.currency = 9999999;
            petShopUI.SetCheatCurrency(true);
            weaponShopUI.SetCheatCurrency(true);
            return;
        }
        if (input == "TWOTIMESPEED") {
            // still buggy can move through obstacles
            Hud.OpenMessagePanel("Cheat 2x Speed Activated!");
            playerMovement.SetCheatTwoTimesSpeed();
            return;
        }
        if (input == "FULLHPPET") {
            // TODO: FULL HP PET
            Hud.OpenMessagePanel("Cheat Full HP Pet Activated!");
            return;
        }
        if (input == "KILLPET") {
            // TODO: KILL PET
            Hud.OpenMessagePanel("Cheat Kill Pet Activated!");
            return;
        }
        if (input == "RESET") {
            playerHealth.SetCheatNoDamage(false);
            playerMovement.ResetSpeed();
            if (motherlodeOn) {
                GameControl.control.currency = this.prevCurrency;
                petShopUI.SetCheatCurrency(false);
                weaponShopUI.SetCheatCurrency(false);
            }
            Hud.OpenMessagePanel("Cheat Reseted!");
            return;
        }
        Hud.OpenMessagePanel("Invalid Cheat!");
    }
}
