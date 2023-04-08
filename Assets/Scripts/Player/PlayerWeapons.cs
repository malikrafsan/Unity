using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    GameObject[][] weapons = new GameObject[4][];
    int idxWeapon = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (var i = 0; i < weapons.Length; i++)
        {
            weapons[i] = new GameObject[0];
        }

        weapons[0] = new GameObject[2];
        weapons[0][0] = GameObject.Find("Gun");
        weapons[0][1] = GameObject.Find("GunBarrelEnd");
        
        weapons[1] = new GameObject[1];
        weapons[1][0] = GameObject.Find("Sword");

        weapons[2] = new GameObject[2];
        weapons[2][0] = GameObject.Find("ShotGun");
        weapons[2][1] = GameObject.Find("ShotGunBarrelEnd");

        weapons[3] = new GameObject[1];
        weapons[3][0] = GameObject.Find("Bow");

        SelectWeapon(idxWeapon);
    }

    // Update is called once per frame
    void Update()
    {
        for (var i = 0; i < weapons.Length; i++)
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

    private void IncrementIdxWeapon()
    {
        idxWeapon++;
        idxWeapon %= weapons.Length;
    }

    private void DecrementIdxWeapon()
    {
        idxWeapon--;
        idxWeapon = (idxWeapon + weapons.Length) % weapons.Length;
    }

    private void SelectWeapon(int idx)
    {
        Debug.Log("SelectWeapon: " + idx);
        DisableAllWeapons();
        EnableWeapon(idx);
    }

    private void EnableWeapon(int idx)
    {
        int idxUsed = idx % weapons.Length;
        for (var i = 0; i < weapons[idxUsed].Length; i++)
        {
            weapons[idxUsed][i].SetActive(true);
        }
    }

    private void DisableAllWeapons()
    {
        for (var i = 0; i < weapons.Length; i++)
        {
            for (var j = 0; j < weapons[i].Length; j++)
            {
                weapons[i][j].SetActive(false);
            }
        }
    }
}
