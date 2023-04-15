using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GlobalStateManager : MonoBehaviour
{
    public static GlobalStateManager Instance { get; private set; }
    private PlayerWeapons playerWeapons;
    private PlayerHealth playerHealth;
    private Temple temple;
    private PetHealth petHealth;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            RefreshAttrs();
        }
    }

    private void RefreshAttrs()
    {
        playerWeapons = FindObjectOfType<PlayerWeapons>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        temple = FindObjectOfType<Temple>();
        petHealth = FindObjectOfType<PetHealth>();
    }

    public int Money
    {
        get
        {
            return GameControl.control.currency;
        }
    }

    public Weapon[] Weapons
    {
        get
        {
            return playerWeapons.Weapons;
        }
    }

    public int Health
    {
        get
        {
            return playerHealth.currentHealth;
        }
    }

    public int MaxHealth
    {
        get
        {
            return playerHealth.startingHealth;
        }
    }

    public string PlayerName
    {
        get
        {
            return GlobalManager.Instance.PlayerName;
        }
    }


    public string Stats()
    {
        var str = "";
        str += string.Format("Money: {0}\n", Money);
        str += string.Format("Health: {0}\n", Health);
        str += string.Format("IdxQuest: {0}\n", IdxQuest);
        foreach (var weapon in Weapons)
        {
            var type = Enum.GetName(typeof(WeaponType), weapon.Type);
            var level = weapon.Level;
            var isUnlocked = weapon.IsUnlocked;
            str += string.Format("Weapon {0} is {1}locked on level {2}\n", type, isUnlocked ? "un" : "",level);
        }
        // TODO: PET

        return str;
    }

    public int IdxQuest
    {
        get
        {
            return temple.IdxCurrentQuest;      
        }
    }

    public int PetHealth
    {
        get
        {
            if (petHealth == null) return 0;
            return petHealth.currentHealth;
        }
    }

    public double TimePlayed
    {
        get
        {
            return GlobalManager.Instance.TimePlayed;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            var stats = Stats();
            Debug.Log(stats);
            ToastManager.Instance.ShowToast(stats, 1);
        }
    }

    public StateSave GetState()
    {
        RefreshAttrs();

        var playerWeapons = new PlayerWeapon[4];
        for (int i = 0; i < Weapons.Length; i++)
        {
            var weapon = Weapons[i];
            var playerWeapon = new PlayerWeapon(weapon.Type, weapon.IsUnlocked, weapon.Level);
            playerWeapons[i] = playerWeapon;
        }

        // TODO: get states
        var metaStateSave = new MetaStateSave("name");
        var arg1 = PlayerName;
        var arg2 = Money;
        var arg3 = Health;
        var arg4 = IdxQuest;
        var arg5 = playerWeapons;

        Debug.Log("PlayerName: " + PlayerName);
        Debug.Log("Money: " + Money);
        Debug.Log("Health: " + Health);
        Debug.Log("IdxQuest: " + IdxQuest);
        foreach (var w in playerWeapons)
        {
            Debug.Log("playerWeapon[{{i}}]: " + w);
        }

        var playerStateSave = new PlayerStateSave(PlayerName, Money, Health, IdxQuest, playerWeapons);
        var petStateSave = new PetStateSave(PetHealth, -1);
        var globalStateSave = new GlobalStateSave(TimePlayed);

        var state = new StateSave(metaStateSave, playerStateSave, petStateSave, globalStateSave);
        return state;
    }

    public void SetState(StateSave state)
    {
        RefreshAttrs();

        // TODO: SET STATES

        // TODO: set meta state save
        GlobalManager.Instance.MetaStateSave = state.metaStateSave;

        // TODO: set player state save
        GlobalManager.Instance.PlayerName = state.playerStateSave.playerName;
        playerHealth.currentHealth = state.playerStateSave.health;
        GameControl.control.currency = state.playerStateSave.money;
        temple.IdxCurrentQuest = state.playerStateSave.idxQuest;
        /*        foreach (var weapon in state.playerStateSave.playerWeapons)
                {
                    var type = weapon.weaponType;
                    var isUnlocked = weapon.isUnlocked;
                    var level = weapon.level;

                    if (isUnlocked)
                    {
                        playerWeapons.UnlockWeapon(type);
                        playerWeapons.SetLevel(type, level);
                    }
                }*/
        var len = state.playerStateSave.playerWeapons.Length;
        for ( var i = 0; i<len; i++ )
        {
            var weapon = state.playerStateSave.playerWeapons[i];
            var type = weapon.weaponType;
            var isUnlocked = weapon.isUnlocked;
            var level = weapon.level;

            if (isUnlocked)
            {
                playerWeapons.UnlockWeapon(type);
                playerWeapons.SetLevel(type, level);
            }
        }

        // TODO: set pet state save


        // TODO: global state save
        GlobalManager.Instance.TimePlayed = state.globalStateSave.timePlayed;
    }
}
