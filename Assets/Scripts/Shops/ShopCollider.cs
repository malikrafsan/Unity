using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCollider : MonoBehaviour
{
    HUD Hud;
    [SerializeField] GameObject shop;
    private bool stall_state = false;

    private void Awake() {
        Hud = GameObject.Find("HUDCanvas").GetComponent<HUD>();
    }

    private void FixedUpdate() {
        if (stall_state && Input.GetKeyDown(KeyCode.F)) {
            if (shop) {
                shop.SetActive(true);
            }
        }
        if (!stall_state && Input.GetKeyDown(KeyCode.F)) {
            Hud.OpenMessagePanel("Go To Shop First!");
            StartCoroutine(executeAfter(3));
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.name.Equals("Player"))
        {
            stall_state = true;
            Hud.OpenMessagePanel("- Press F to Shop - ");
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

    private IEnumerator executeAfter(int secs) {
        yield return new WaitForSeconds(secs);
        Hud.CloseMessagePanel();
    }
}
