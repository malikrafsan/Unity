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

    void Start() {
        GenerateItemShopUI();
    }

    void GenerateItemShopUI() {
        // Remove First Element
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
                uiItem.OnItemSelect(i, OnItemSelectedWeapon);
            } else {
                uiItem.SetPrice (item.price);
                uiItem.OnItemPurchase(i, OnItemPurchase);
            }
        }
    }

    void OnItemSelectedWeapon(int index) {

        Item itemSelect = itemDB.GetItem(index);
        
        // Inequip Weapon
        int position = GameControl.control.slotWeapon(itemSelect);
        Item previousItem = GameControl.control.equipedWeapon[position];
        if (previousItem.characterName != "") {
            ItemUI previousUI = GetItemUI(previousItem.characterName);
            previousUI.UnequipItem();
        }
        
        // Equip Weapon
        GameControl.control.equipWeapon(itemSelect);
        ItemUI itemSelectUI = GetItemUI(index);
        itemSelectUI.EquipItem();
    }

    void OnItemPurchase (int index) {

        Item itemPurchasing = itemDB.GetItem(index);

        // Check if enough money
        if ( GameControl.control.isEnough(itemPurchasing.price) ) {

            //purchasing
            if (itemPurchasing.isWeapon) {
                OnWeaponPurchase(index);
            } else {
                GameControl.control.minusCurrency(itemPurchasing.price);
                OnPetPurchase(index);
            }

        } else {
            // TO DO: Keluar Animasi Not Enough Money
            messageError.text = "Money is not enough!!";
            StartCoroutine(executeAfter(3));
        }
    }

    void OnItemSelected(int index) {
        Debug.Log("Selected = " + index);
    }

    // purchase pets
    void OnPetPurchase (int index) {
        // TODO: initiate prefab pets in scene
    }

    // purchase weapon
    void OnWeaponPurchase (int index) {
        // Getting item from DB and UI
        Item itemPurchasing = itemDB.GetItem(index);
        ItemUI itemBeingPurchased = GetItemUI(index);

        // Set if purchased
        if (checkCanPurchase(itemPurchasing)) {
            GameControl.control.minusCurrency(itemPurchasing.price);
            itemDB.PurchasedItem(index);

            // Equipping Weapon
            GameControl.control.addWeaponToInventory(itemPurchasing);
            GameControl.control.equipWeapon(itemPurchasing);

            // Change UI if purchased
            itemBeingPurchased.SetItemAsPurchased();
            itemBeingPurchased.OnItemSelect(index, OnItemSelectedWeapon);
        } else {
            int slot = GameControl.control.slotWeapon(itemPurchasing);
            messageError.text = "Purchase Weapon Level " + GameControl.control.emptyLevel(slot) + " First!";
            StartCoroutine(executeAfter(3));
        }
        // TO DO: integrate with weapon
    }

    bool checkCanPurchase(Item selected) {
        if (selected.level == 1) {
            return true;
        }
        int slot = GameControl.control.slotWeapon(selected);
        Item temp = GameControl.control.weapons[slot, selected.level-2];

        if (temp.characterName == null){
            return false;
        }
        return true;
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
}
