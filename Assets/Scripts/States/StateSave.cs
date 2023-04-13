using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class MetaStateSave
{
    public string name;
    public string createdAt;
    public string updatedAt;

    public MetaStateSave(string name)
    {
        this.name = name;
        this.createdAt = DateTime.Now.ToString(SaveLoadConfig.dateTimeFormat);
        this.updatedAt = DateTime.Now.ToString(SaveLoadConfig.dateTimeFormat);
    }

    public void Update()
    {
        this.updatedAt = DateTime.Now.ToString(SaveLoadConfig.dateTimeFormat);
    }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}

[Serializable]
public class PlayerStateSave
{
    public string name;
    public int money;
    public int health;
    public int idxQuest;

    public PlayerStateSave(string name, int money, int health, int idxQuest)
    {
        this.name=name;
        this.money = money;
        this.health = health;
        this.idxQuest = idxQuest;
    }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}

[Serializable]
public class PetStateSave
{
    public int health;
    public int idxCurrentPet;

    public PetStateSave(int health, int idxCurrentPet)
    {
        this.health = health;
        this.idxCurrentPet = idxCurrentPet;
    }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}

[Serializable]
public class GlobalStateSave
{
    public double timePlayed;

    public GlobalStateSave(double timePlayed)
    {
        this.timePlayed = timePlayed;
    }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}

[Serializable]
public class StateSave
{
    public MetaStateSave metaStateSave;
    public PlayerStateSave playerStateSave;
    public PetStateSave petStateSave;
    public GlobalStateSave globalStateSave;

    public StateSave(MetaStateSave metaStateSave, PlayerStateSave playerStateSave, PetStateSave petStateSave, GlobalStateSave globalStateSave) 
    {
        this.metaStateSave = metaStateSave;
        this.playerStateSave = playerStateSave;
        this.petStateSave = petStateSave;
        this.globalStateSave = globalStateSave;
    }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
