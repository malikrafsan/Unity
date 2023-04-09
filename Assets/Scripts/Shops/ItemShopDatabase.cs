using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// FUngsi kelas ini adalah database dari shop item
[CreateAssetMenu (fileName = "ItemShopDatabase", menuName = "Shopping/Items shop database")]
public class ItemShopDatabase : ScriptableObject
{   
    public Item[] items;

    public int ItemsCount {
        get { return items.Length; }
    }

    public Item GetItem (int index){
        return items[index];
    }

    public void LevelUpItem (int index) {
        Item temp = items[index];

        if ( temp.isWeapon ) {
            items[index].level++;
        }
    }

    public void SetItemAsPurchased (int index) {
        Item temp = items[index];
        items[index].isPurchased = true;
    }

    public void SetDescription (int index, string newDescription) {
        Item temp = items[index];

        if ( temp.isWeapon ) {
            items[index].description = newDescription;
        }
    }

    public void IncreasePrice (int index) {
        Item temp = items[index];

        if ( temp.isWeapon ) {
            items[index].price = items[index].price * (items[index].level + 1);
        }
    }

    public void SetCharacterName (int index, string newName) {
        Item temp = items[index];

        if ( temp.isWeapon ) {
            items[index].characterName = newName;
        }
    }

    public void SetItem(int index, Item newItem) {
        items[index].characterName = newItem.characterName;
        items[index].description = newItem.description;
        items[index].level = newItem.level;
        items[index].price = newItem.price;
        items[index].isPurchased = newItem.isPurchased;
    }

    public void SetPurchase(int index, bool purchase) {
        items[index].isPurchased = purchase;
    }
}
