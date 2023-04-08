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

    public void PurchasedItem (int index) {
        Item temp = items[index];

        if ( temp.isWeapon ) {
            items[index].isPurchased = true;
        }
    }
}
