using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

// Fungsi kelas untuk 1 item pada shop
public class ItemUI : MonoBehaviour
{

    [SerializeField] Color itemNotSelectedColor;
    [SerializeField] Color itemSelectedColor;
    
    [Space (20f)]
    [SerializeField] Image oldImage;
    [SerializeField] Text characterName;
    [SerializeField] Text descriptionContent;
    [SerializeField] Text price;
    [SerializeField] Button itemPurchaseButton;
    [SerializeField] Button equipButton;

    [Space (20f)]
    [SerializeField] Button itemButton;
    [SerializeField] Image itemImage;

    public void SetItemImage (Sprite newImage) {
        oldImage.sprite = newImage;
    }

    public void SetCharacterName (string name) {
        characterName.text = name;
    }

    public void SetDescription (string des) {
        descriptionContent.text = des;
    }

    public void SetPrice (int newPrice) {
        price.text = newPrice.ToString();
    }

    public void SetItemAsPurchased () {
        // for pets
        itemPurchaseButton.gameObject.SetActive(false);
        itemButton.interactable = true;
        equipButton.gameObject.SetActive(true);
    }

    public void OnItemPurchase (int itemIndex, UnityAction<int> action) {
        itemPurchaseButton.onClick.RemoveAllListeners();
        itemPurchaseButton.onClick.AddListener( () => action.Invoke (itemIndex) );
    }

    public void OnItemSelect (int itemIndex, UnityAction<int> action) {
        itemButton.interactable = true;
        itemButton.onClick.RemoveAllListeners();
        itemButton.onClick.AddListener( ()=> action.Invoke (itemIndex) );
    }

    public void SelectItem() {
        itemImage.color = itemSelectedColor;
        itemButton.interactable = false;
    }

    public void DeselectItem() {
        itemImage.color = itemNotSelectedColor;
        itemButton.interactable = true;
    }

    public void EquipItem() {
        equipButton.gameObject.SetActive(true);
    }

    public void UnequipItem(){
        equipButton.gameObject.SetActive(false);
    }
}
