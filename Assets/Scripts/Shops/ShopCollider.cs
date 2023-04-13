using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCollider : MonoBehaviour
{
    HUD Hud;
    [SerializeField] GameObject shop;
    private bool stall_state = false;
    Temple temple;

    private void Awake() {
        Hud = GameObject.Find("HUDCanvas").GetComponent<HUD>();
        temple = GameObject.Find("Environment").GetComponentInChildren<Temple>();
    }

    private void Update() {
        if (temple.OnQuest && Input.GetKeyDown(KeyCode.F)) {
            Hud.OpenMessagePanel("Shop Unavailable!");
            return;
        }
        if (stall_state && Input.GetKeyDown(KeyCode.F)) {
            if (shop) {
                shop.SetActive(true);
            }
        }
        if (!stall_state && Input.GetKeyDown(KeyCode.F)) {
            Hud.OpenMessagePanel("Go To Shop First!");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(!temple.OnQuest && other.gameObject.name.Equals("Player"))
        {
            stall_state = true;
            Hud.OpenPermanantMessage("- Press F to Shop - ");
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if(other.gameObject.name.Equals("Player"))
        {
            stall_state = false;
            Hud.CloseMessagePanel();
        }
    }
}
