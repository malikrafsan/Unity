using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Fungsi kelas ini untuk shop UI
public class ItemShopUI : MonoBehaviour
{

    [Space(20f)]
    [SerializeField] Transform ShopItemContainer;
    [SerializeField] GameObject itemPrefab;

    [Space(20f)]
    [SerializeField] ItemShopDatabase itemDB;
    [SerializeField] Text messageError;
    PlayerWeapons playerWeapons;

    void Start() {
        resetDatabase();
        GenerateItemShopUI();
        GameObject.Find("HUDCanvas").GetComponent<HUD>();
        playerWeapons = GameObject.Find("Player").GetComponent<PlayerWeapons>();
        if (this.name == "Weapon") OnWeaponPurchase(0);
    }

    void Updating(int index){
        ItemUI uiItem = GetItemUI(index);
        Item item = itemDB.GetItem(index);

        uiItem.SetCharacterName (item.characterName);
        uiItem.SetDescription (item.description);
        uiItem.SetPrice (item.price);
    }

    void GenerateItemShopUI() {
        // Clearing items
        Destroy (ShopItemContainer.GetChild(0).gameObject);
        ShopItemContainer.DetachChildren();

        // Generating items
        for (int i = 0; i < itemDB.ItemsCount; i++) {
            Item item = itemDB.GetItem(i);
            ItemUI uiItem = Instantiate (itemPrefab, ShopItemContainer).GetComponent<ItemUI>();

            // Item name in hierarchy
            uiItem.gameObject.name = "Item " + item.characterName;

            // Add information
            uiItem.SetCharacterName (item.characterName);
            uiItem.SetDescription (item.description);
            uiItem.SetItemImage (item.image);
            uiItem.SetPrice (item.price);

            // Pertama kali load jika weapon atau tidak,
            // FIX: do not use isPurchased
            if (item.isPurchased) {
                uiItem.SetItemAsPurchased();
                uiItem.OnItemSelect(i, OnItemSelectedPet);
            } else {
                uiItem.SetPrice (item.price);
                uiItem.OnItemPurchase(i, OnItemPurchase);
            }
        }
    }

    void OnItemSelectedPet(int index) {
        Debug.Log("already bought man");
    }

    void OnItemSelectedWeapon(int index) {
        Item itemPurchasing = itemDB.GetItem(index);
        if ( GameControl.control.isEnough(itemPurchasing.price) ) {
            //purchasing
            GameControl.control.minusCurrency(itemPurchasing.price);

            // level up weapon
            Item itemSelected = itemDB.GetItem(index);
            playerWeapons.LevelUp(itemSelected.weaponType);

            // update Data and UI for leveling up
            itemDB.LevelUpItem(index);
            string newName = " " + itemSelected.weaponType + " Level " + itemSelected.level;
            itemDB.SetCharacterName(index, newName);
            itemDB.IncreasePrice(index);
            Updating(index);
        } else {
            // Keluar Text Not Enough Money
            messageError.text = "Money is not enough!!";
            StartCoroutine(executeAfter(3));
        }
    }

    void OnItemPurchase (int index) {

        Item itemPurchasing = itemDB.GetItem(index);

        // Check if enough money
        if ( GameControl.control.isEnough(itemPurchasing.price) ) {

            //purchasing
            if (itemPurchasing.isWeapon) {
                GameControl.control.minusCurrency(itemPurchasing.price);
                if (itemPurchasing.level == 1) {
                    OnWeaponPurchase(index);
                    return;
                }
                OnItemSelectedWeapon(index);
            } else {
                OnPetPurchase(index);
            }
        } else {
            // TO DO: Keluar Animasi Not Enough Money
            messageError.text = "Money is not enough!!";
            StartCoroutine(executeAfter(3));
        }
    }

    // purchase pets
    void OnPetPurchase (int index) {
        // TODO: initiate prefab pets in scene and only one pet can be active
        if (GameControl.control.petCount == 1) {
            messageError.text = "You can only have 1 pet!";
            StartCoroutine(executeAfter(3));
            return;
        }
        Item itemPurchasing = itemDB.GetItem(index);
        ItemUI itemBeingPurchased = GetItemUI(index);
        GameControl.control.minusCurrency(itemPurchasing.price);
        // Set if purchased
        itemDB.SetPurchase(index, true);
        itemBeingPurchased.SetItemAsPurchased();
        itemBeingPurchased.OnItemSelect(index, OnItemSelectedPet);
        GameControl.control.addPet();
    }

    // Equip Purchase Weapon
    void OnWeaponPurchase (int index) {
        // Getting item from DB and UI
        ItemUI itemBeingPurchased = GetItemUI(index);

        // Set if purchased
        itemDB.SetDescription(index, "Leveling Up");
        itemDB.IncreasePrice(index);
        itemDB.LevelUpItem(index);
        Item itemPurchasing = itemDB.GetItem(index);
        string newName = " " + itemPurchasing.weaponType + " Lvl " + itemPurchasing.level;
        itemDB.SetCharacterName(index, newName);

        // Unlock Weapon
        playerWeapons.UnlockWeapon(itemPurchasing.weaponType);

        // Change UI if purchased
        itemBeingPurchased.OnItemPurchase(index, OnItemSelectedWeapon);
        // render UI item
        Updating(index);
    }

    private IEnumerator executeAfter(int secs) {
        yield return new WaitForSeconds(secs);
        messageError.text = "";
    }

    // getters
    ItemUI GetItemUI(int index) {
        return ShopItemContainer.GetChild(index).GetComponent<ItemUI>();
    }

    ItemUI GetItemUI(string name) {
        string temp = "Item " + name;
        Transform test =  ShopItemContainer.Find(temp);
        return test.GetComponent<ItemUI>();
    }

    // reset Database
    void resetDatabase(){
        int counter = 0;
        if (this.name == "Pet") {
            // Set For Later
        }
        if (this.name == "Weapon") {
            foreach (WeaponType weapon in System.Enum.GetValues(typeof(WeaponType))) {
                // set item
                string name = "Weapon " + weapon;
                string description = "Purchase to Unlock!";
                int price = 2;
                // TODO: FInd Images for each weapon
                Sprite image = Resources.Load<Sprite>("Weapon/gold_coin-removebg-preview");
                bool isPurchased = false;
                bool isWeapon = true;
                WeaponType weaponType = weapon;
                int level = 1;

                itemDB.SetItem (counter, image, description, price, name, isPurchased, isWeapon, weaponType, level);
                counter++; 
            }
        }
    }
}
