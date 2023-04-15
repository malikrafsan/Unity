using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    HUD Hud;
    PlayerHealth playerHealth;
    PlayerMovement playerMovement;
    PlayerWeapons playerWeapon;
    public GameObject petShop;
    public GameObject weaponShop;
    ItemShopUI petShopUI;
    ItemShopUI weaponShopUI;
    private string input;
    private int prevCurrency;

    // Activated Cheats
    bool[] cheats = new bool[5];

    private void Awake()
    {
        Hud = GameObject.Find("HUDCanvas").GetComponent<HUD>();
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerWeapon = GameObject.Find("Player").GetComponent<PlayerWeapons>();
        petShopUI = petShop.GetComponent<ItemShopUI>();
        weaponShopUI = weaponShop.GetComponent<ItemShopUI>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Hud.ShowTextInput();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hud.CloseTextInput();
        }
    }

    public void ReadStringInput(string s)
    {
        input = s;
        compareString();
    }

    public void compareString()
    {
        if (input == "NODAMAGE")
        {
            CheatNoDamage();
            return;
        }
        if (input == "ONEHITKILL")
        {
            CheatOneHitKill();
            return;
        }
        if (input == "MOTHERLODE")
        {
            CheatMotherLode();
            return;
        }
        if (input == "TWOTIMESPEED")
        {
            CheatTwoTimeSpeed();
            return;
        }
        if (input == "FULLHPPET")
        {
            CheatFullHPPet();
            return;
        }
        if (input == "KILLPET")
        {
            CheatKillPet();
            return;
        }
        if (input == "RESET")
        {
            CheatReset();
            return;
        }
        Hud.OpenMessagePanel("Invalid Cheat!");
    }

    private void CheatNoDamage()
    {
        playerHealth.SetCheatNoDamage(true);
        Hud.OpenMessagePanel("Cheat No Damage Activated!");
        cheats[(int)CheatType.NODAMAGE] = true;
    }

    private void CheatOneHitKill()
    {
        GameControl.control.cheatOneHitKill = true;
        Hud.OpenMessagePanel("Cheat One Hit Kill Activated!");
        cheats[(int)CheatType.ONEHITKILL] = true;
    }

    private void CheatMotherLode()
    {
        this.prevCurrency = GameControl.control.currency;
        GameControl.control.currency = 999999;
        petShopUI.SetCheatCurrency(true);
        weaponShopUI.SetCheatCurrency(true);
        cheats[(int)CheatType.MOTHERLODE] = true;
        GameControl.control.motherLoadOn = true;
        Hud.OpenMessagePanel("Cheat Mother Lode Activated!");
    }

    private void CheatTwoTimeSpeed()
    {
        playerMovement.SetCheatTwoTimesSpeed();
        Hud.OpenMessagePanel("Cheat Two Time Speed Activated!");
        cheats[(int)CheatType.TWOTIMESPEED] = true;
    }

    private void CheatFullHPPet()
    {
        Hud.OpenMessagePanel("Cheat Full HP Pet Activated!");
        GameControl.control.fullHPPet = true;
        cheats[(int)CheatType.FULLHPPET] = true;
    }

    private void CheatKillPet()
    {
        Hud.OpenMessagePanel("Cheat Kill Pet Activated!");
        GameControl.control.killPet = true;
    }

    private void CheatReset()
    {
        playerHealth.SetCheatNoDamage(false);
        playerMovement.ResetSpeed();
        GameControl.control.cheatOneHitKill = false;
        if (cheats[(int)CheatType.MOTHERLODE])
        {
            GameControl.control.currency = this.prevCurrency;
            petShopUI.SetCheatCurrency(false);
            weaponShopUI.SetCheatCurrency(false);
            GameControl.control.motherLoadOn = false;
        }
        GameControl.control.fullHPPet = false;
        Hud.OpenMessagePanel("Cheat Reseted!");
    }

    public void loadCheat(bool[] gatheredCheats)
    {
        if (gatheredCheats[(int)CheatType.NODAMAGE])
        {
            CheatNoDamage();
        }
        if (gatheredCheats[(int)CheatType.ONEHITKILL])
        {
            CheatOneHitKill();
        }
        if (gatheredCheats[(int)CheatType.MOTHERLODE])
        {
            CheatMotherLode();
        }
        if (gatheredCheats[(int)CheatType.TWOTIMESPEED])
        {
            CheatTwoTimeSpeed();
        }
        if (gatheredCheats[(int)CheatType.FULLHPPET])
        {
            CheatFullHPPet();
        }
    }

    public bool[] SaveCheat()
    {
        return cheats;
    }

    public void ResetCheat()
    {
        cheats = new bool[5];
        foreach (CheatType cheat in System.Enum.GetValues(typeof(CheatType)))
        {
            cheats[(int)cheat] = false;
        }
    }

    public int GetPrevCurrency()
    {
        return this.prevCurrency;
    }
}
