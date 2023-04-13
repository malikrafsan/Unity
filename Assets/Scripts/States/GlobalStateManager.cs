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

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            playerWeapons = FindObjectOfType<PlayerWeapons>();
            playerHealth = FindObjectOfType<PlayerHealth>();
            temple = FindObjectOfType<Temple>();
        }
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            var stats = Stats();
            Debug.Log(stats);
            ToastManager.Instance.ShowToast(stats, 3);
        }
    }

    public StateSave GetState()
    {
        // TODO: get states
        var metaStateSave = new MetaStateSave("name");
        var playerStateSave = new PlayerStateSave(Money, Health, IdxQuest);
        var petStateSave = new PetStateSave(100, -1);
        var globalStateSave = new GlobalStateSave(319.123);

        var state = new StateSave(metaStateSave, playerStateSave, petStateSave, globalStateSave);
        return state;
    }

    public void SetState(StateSave state)
    {
        Debug.Log("STATE: " + state);
        playerHealth.currentHealth = state.playerStateSave.health;
        GameControl.control.currency = state.playerStateSave.money;
    }
}
