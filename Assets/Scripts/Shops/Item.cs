using UnityEngine;

[System.Serializable]
// Fungsi ini adalah atribut dari kelas item
public struct Item
{
    public Sprite image;
    public string description;
    public int price;
    public string characterName;

    public bool isPurchased;

    public bool isWeapon;
    public WeaponType weaponType;
    public int level;

    public PetType petType;
}
