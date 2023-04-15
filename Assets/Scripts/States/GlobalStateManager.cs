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
    private CheatManager cheatManager;

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
        cheatManager = FindObjectOfType<CheatManager>();
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

    private PetManager _petManager;
    public PetManager petManager
    {
        get
        {
            if (_petManager == null)
            {
                _petManager = GameObject.Find("PetManager").GetComponent<PetManager>();
            }

            return _petManager;
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

    public bool[] Cheats
    {
        get
        {
            /*return CheatManager.Ims*/
            return cheatManager.SaveCheat();
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
        var playerStateSave = new PlayerStateSave(PlayerName, Money, Health, IdxQuest, playerWeapons);
        var petStateSave = new PetStateSave(PetHealth, (int)GameControl.control.petIdx);
        var globalStateSave = new GlobalStateSave(TimePlayed, Cheats);

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
        GameControl.control.petIdx = state.petStateSave.idxCurrentPet;
        var pet = petManager.Spawn(state.petStateSave.idxCurrentPet);
        if (pet != null)
        {
            pet.GetComponent<PetHealth>().currentHealth = state.petStateSave.health;
        }

        // TODO: global state save
        GlobalManager.Instance.TimePlayed = state.globalStateSave.timePlayed;
        cheatManager.loadCheat(state.globalStateSave.cheats);
    }
}
