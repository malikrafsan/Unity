using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    public Weapon[] Weapons
    {
        get;
    } = new Weapon[4];
    int idxWeapon = 0;

    // Start is called before the first frame update
    void Awake()
    {
        var gun = new GameObject[2];
        gun[0] = GameObject.Find("GunBarrelEnd");
        gun[1] = GameObject.Find("Gun");
        Weapons[0] = new Weapon(gun, WeaponType.SimpleGun);

        var shotgun = new GameObject[2];
        shotgun[0] = GameObject.Find("ShotGunBarrelEnd");
        shotgun[1] = GameObject.Find("ShotGun");
        Weapons[1] = new Weapon(shotgun, WeaponType.ShotGun);

        var sword = new GameObject[1];
        sword[0] = GameObject.Find("Sword");
        Weapons[2] = new Weapon(sword, WeaponType.Sword);

        var bow = new GameObject[1];
        bow[0] = GameObject.Find("Bow");
        Weapons[3] = new Weapon(bow, WeaponType.Bow);

        UnlockWeapon(WeaponType.SimpleGun);
        /*UnlockWeapon(WeaponType.Sword);
        UnlockWeapon(WeaponType.ShotGun);
        UnlockWeapon(WeaponType.Bow);*/
        SelectWeapon(idxWeapon);

    }

    // Update is called once per frame
    void Update()
    {
        for (var i = 0; i < Weapons.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                idxWeapon = i;
                SelectWeapon(idxWeapon);
                break;
            }
        }

        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            IncrementIdxWeapon();
            SelectWeapon(idxWeapon);
        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            DecrementIdxWeapon();
            SelectWeapon(idxWeapon);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            IncrementIdxWeapon();
            SelectWeapon(idxWeapon);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            DecrementIdxWeapon();
            SelectWeapon(idxWeapon);
        }
    }

    private int GetIdxWeapon(WeaponType weaponType)
    {
        return weaponType switch
        {
            WeaponType.SimpleGun => 0,
            WeaponType.ShotGun => 1,
            WeaponType.Sword => 2,
            WeaponType.Bow => 3,
            _ => -1,
        };
    }

    public bool LevelUp(WeaponType weaponType)
    {
        int idx = GetIdxWeapon(weaponType);
        if (idx == -1) return false;

        Weapons[idx].handler.IncrementLevel();
        Debug.Log("Current Level is" + Weapons[idx].handler.Level);
        return true;
    }

    public bool UnlockWeapon(WeaponType weaponType)
    {
        int idx = GetIdxWeapon(weaponType);
        if (idx == -1) return false;

        var ws = Weapons;
        foreach (var w in ws)
        {
            Debug.Log("Weapon: " + w);
            Debug.Log("Weapon len: " + Weapons.Length);
            Debug.Log("Weapons[idx]: " + w);
        }

        Weapons[idx].IsUnlocked = true;
        return true;
    }

    public bool SetLevel(WeaponType weaponType, int level)
    {
        int idx = GetIdxWeapon(weaponType);
        if (idx == -1) return false;

        Weapons[idx].Level = level;
        return true;
    }

    private void IncrementIdxWeapon()
    {
        idxWeapon++;
        idxWeapon %= Weapons.Length;
    }

    private void DecrementIdxWeapon()
    {
        idxWeapon--;
        idxWeapon = (idxWeapon + Weapons.Length) % Weapons.Length;
    }

    private void SelectWeapon(int idx)
    {
        Debug.Log("SelectWeapon: " + idx);
        DisableAllWeapons();
        var success = EnableWeapon(idx);
        if (!success)
        {
            ToastManager.Instance.ShowToast("Selecting locked weapon", 1);
        }
    }

    private bool EnableWeapon(int idx)
    {
        return Weapons[idx].Enable();
    }

    private void DisableAllWeapons()
    {
        for (var i = 0; i < Weapons.Length; i++)
        {
            Weapons[i].Disable();
        }
    }
}
