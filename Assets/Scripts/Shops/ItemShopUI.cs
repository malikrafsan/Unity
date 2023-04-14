using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Fungsi kelas ini untuk shop UI
public class ItemShopUI : MonoBehaviour
{
    ItemShopDatabase itemDB;
    [Space(20f)]
    [SerializeField] Transform ShopItemContainer;
    [SerializeField] GameObject itemPrefab;

    [Space(20f)]
    [SerializeField] Text messageError;
    PlayerWeapons playerWeapons;
    private bool cheatCurrency = false;

    void Start()
    {
        itemDB = GetComponent<ItemShopDatabase>();
        resetDatabase();
        GameObject.Find("HUDCanvas").GetComponent<HUD>();
        playerWeapons = GameObject.Find("Player").GetComponent<PlayerWeapons>();
        GenerateItemShopUI();
    }

    void Updating(int index)
    {
        ItemUI uiItem = GetItemUI(index);
        Item item = itemDB.GetItem(index);

        uiItem.SetCharacterName(item.characterName);
        uiItem.SetDescription(item.description);
        uiItem.SetPrice(item.price);
    }

    void GenerateItemShopUI()
    {
        // Clearing items
        Destroy(ShopItemContainer.GetChild(0).gameObject);
        ShopItemContainer.DetachChildren();

        if (this.name == "Weapon")
        {
            LoadWeapon();
        }
        if (this.name == "Pet")
        {
            LoadPet();
        }
        // Generating items
        for (int i = 0; i < itemDB.ItemsCount; i++)
        {
            Item item = itemDB.GetItem(i);
            ItemUI uiItem = Instantiate(itemPrefab, ShopItemContainer).GetComponent<ItemUI>();

            // Item name in hierarchy
            uiItem.gameObject.name = "Item " + item.characterName;

            // Add information
            uiItem.SetCharacterName(item.characterName);
            uiItem.SetDescription(item.description);
            uiItem.SetItemImage(item.image);
            uiItem.SetPrice(item.price);

            // Pertama kali load jika weapon atau tidak,
            // FIX: do not use isPurchased
            if (item.isPurchased)
            {
                uiItem.SetItemAsPurchased();
                uiItem.OnItemSelect(i, OnItemSelectedPet);
            }
            else
            {
                if ((item.isWeapon && item.level != 1) || item.weaponType == WeaponType.SimpleGun)
                {
                    uiItem.OnItemPurchase(i, OnItemSelectedWeapon);
                }
                else
                {
                    uiItem.OnItemPurchase(i, OnItemPurchase);
                }
                uiItem.SetPrice(item.price);
            }
        }
    }

    void OnItemSelectedPet(int index)
    {
        Debug.Log("already bought man");
    }

    void OnItemSelectedWeapon(int index)
    {
        Item itemPurchasing = itemDB.GetItem(index);
        if (GameControl.control.isEnough(itemPurchasing.price))
        {
            //purchasing
            if (!cheatCurrency)
            {
                GameControl.control.minusCurrency(itemPurchasing.price);
            }

            // level up weapon
            Item itemSelected = itemDB.GetItem(index);
            playerWeapons.LevelUp(itemSelected.weaponType);

            // update Data and UI for leveling up
            Debug.Log("Leveling up weapon before: " + itemSelected.level);
            itemDB.LevelUpItem(index);
            Debug.Log("Leveling up weapon after: " + itemSelected.level);

            string newName = "" + itemSelected.weaponType + " Lvl " + (itemSelected.level + 1);
            itemDB.SetCharacterName(index, newName);
            itemDB.IncreasePrice(index);
            string newDescription = "Leveling your weapon to level " + (itemSelected.level + 1);
            itemDB.SetDescription(index, newDescription);
            Updating(index);
        }
        else
        {
            // Keluar Text Not Enough Money
            messageError.text = "Money is not enough!!";
            StartCoroutine(executeAfter(3));
        }
    }

    void OnItemPurchase(int index)
    {

        Item itemPurchasing = itemDB.GetItem(index);

        // Check if enough money
        if (GameControl.control.isEnough(itemPurchasing.price))
        {

            //purchasing
            if (itemPurchasing.isWeapon)
            {
                if (!cheatCurrency)
                {
                    GameControl.control.minusCurrency(itemPurchasing.price);
                }
                if (itemPurchasing.level == 1)
                {
                    OnWeaponPurchase(index);
                    return;
                }
                OnItemSelectedWeapon(index);
            }
            else
            {
                OnPetPurchase(index);
            }
        }
        else
        {
            // TO DO: Keluar Animasi Not Enough Money
            messageError.text = "Money is not enough!!";
            StartCoroutine(executeAfter(3));
        }
    }

    // purchase pets
    void OnPetPurchase(int index)
    {
        // TODO: initiate prefab pets in scene and only one pet can be active
        if (GameControl.control.petCount == 1)
        {
            messageError.text = "You can only have 1 pet!";
            StartCoroutine(executeAfter(3));
            return;
        }
        Item itemPurchasing = itemDB.GetItem(index);
        ItemUI itemBeingPurchased = GetItemUI(index);
        if (!cheatCurrency)
        {
            GameControl.control.minusCurrency(itemPurchasing.price);
        }
        // Set if purchased
        itemDB.SetPurchase(index, true);
        itemBeingPurchased.SetItemAsPurchased();
        itemBeingPurchased.OnItemSelect(index, OnItemSelectedPet);
        GameControl.control.addPet();
    }

    // Equip Purchase Weapon
    void OnWeaponPurchase(int index)
    {
        // Getting item from DB and UI
        ItemUI itemBeingPurchased = GetItemUI(index);

        // Set if purchased
        Item itemPurchasing = itemDB.GetItem(index);
        itemDB.IncreasePrice(index);
        itemDB.LevelUpItem(index);
        itemDB.SetDescription(index, "Leveling your weapon to level " + (itemPurchasing.level + 1));

        string newName = " " + itemPurchasing.weaponType + " Lvl " + (itemPurchasing.level + 1);
        itemDB.SetCharacterName(index, newName);

        // Unlock Weapon
        playerWeapons.UnlockWeapon(itemPurchasing.weaponType);

        // Change UI if purchased
        itemBeingPurchased.OnItemPurchase(index, OnItemSelectedWeapon);
        // render UI item
        Updating(index);
    }

    private IEnumerator executeAfter(int secs)
    {
        yield return new WaitForSeconds(secs);
        messageError.text = "";
    }

    // getters
    ItemUI GetItemUI(int index)
    {
        return ShopItemContainer.GetChild(index).GetComponent<ItemUI>();
    }

    ItemUI GetItemUI(string name)
    {
        string temp = "Item " + name;
        Transform test = ShopItemContainer.Find(temp);
        return test.GetComponent<ItemUI>();
    }

    // reset Database
    void resetDatabase()
    {
        if (this.name == "Pet")
        {
            foreach (PetType pet in System.Enum.GetValues(typeof(PetType)))
            {
                // set item
                string name = "Pet " + pet;
                string description = "Purchase to Equip!";
                int price = 30;
                Sprite image = Resources.Load<Sprite>("Pet/" + pet.ToString());
                bool isPurchased = false;
                bool isWeapon = false;
                WeaponType weaponType = WeaponType.SimpleGun;
                int level = 0;
                itemDB.SetItem((int)pet, image, description, price, name, isPurchased, isWeapon, weaponType, level);
            }
        }
        if (this.name == "Weapon")
        {
            foreach (WeaponType weapon in System.Enum.GetValues(typeof(WeaponType)))
            {
                // set item
                string name = weapon + " Lvl 1";
                string description = "Purchase to Unlock!";
                int price = 2;
                Sprite image = Resources.Load<Sprite>("Weapon/" + weapon.ToString());
                bool isPurchased = false;
                bool isWeapon = true;
                WeaponType weaponType = weapon;
                int level = 1;
                itemDB.SetItem((int)weapon, image, description, price, name, isPurchased, isWeapon, weaponType, level);
            }
        }
    }

    public void SetCheatCurrency(bool value)
    {
        cheatCurrency = value;
    }


    public void LoadWeapon()
    {
        var existentWeapon = playerWeapons.Weapons;
        if (existentWeapon == null)
        {
            Debug.Log("Weapon is null, state actually??");
            Debug.Log(state);
            return;
        }
        foreach (var weapon in existentWeapon)
        {
            Debug.Log("Weapon index " + (int)weapon.Type + " " + weapon.Type + " " + weapon.Level);

            string name = weapon.Type + " Lvl " + (weapon.Level);
            string description = "";
            int level = weapon.Level;
            if (weapon.Level == 1 && weapon.Type != WeaponType.SimpleGun)
                description = "Purchase to Unlock!";
            else
            {
                description = "Leveling your weapon to level " + (weapon.Level + 1);
                level += 1;
                name = weapon.Type + " Lvl " + (weapon.Level + 1);
            }
            int price = itemDB.GetIncreasePrice((int)weapon.Type);
            Sprite image = Resources.Load<Sprite>("Weapon/" + weapon.Type.ToString());
            bool isWeapon = true;
            WeaponType weaponType = weapon.Type;

            itemDB.SetItem((int)weapon.Type, image, description, price, name, false, isWeapon, weaponType, level);
        }
    }

    public void LoadPet()
    {
        foreach (PetType pet in System.Enum.GetValues(typeof(PetType)))
        {
            Debug.Log("masuk ke pet yang " + pet + "dengan index yang " + (int)pet);
            // set item
            string name = "Pet " + pet;
            string description = "Purchase to Equip!";
            int price = 30;
            Sprite image = Resources.Load<Sprite>("Pet/" + pet.ToString());
            bool isPurchased = false;
            bool isWeapon = false;
            WeaponType weaponType = WeaponType.SimpleGun;
            int level = 0;
            itemDB.SetItem((int)pet, image, description, price, name, isPurchased, isWeapon, weaponType, level);
        }
    }
}
