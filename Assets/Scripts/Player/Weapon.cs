using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Weapon
{
    public readonly GameObject[] gameObjects;
    public bool IsUnlocked
    {
        get; set;
    }

    public int Level
    {
        get
        {
            return handler.Level;
        }
        set
        {
            handler.Level = value;
        }
    }

    public readonly WeaponHandler handler;

    public WeaponType Type
    {
        get;
        private set;
    }

    public Weapon(GameObject[] gameObjects, WeaponType type)
    {
        this.gameObjects = gameObjects;
        this.handler = this.gameObjects[0].GetComponent<WeaponHandler>();
        this.IsUnlocked = false;
        Disable();
        Type = type;
    }

    public bool Enable()
    {
        if (!IsUnlocked) return false;

        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(true);
        }

        return true;
    }

    public void Disable()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(false);
        }
    }
}
