using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Fungsi kelas ini untuk event shop buka dan tutup
public class EventShop : MonoBehaviour
{
    [SerializeField] GameObject shopUI;

    public void CloseShop() {
        shopUI.SetActive(false);
    }

    public void OpenShop() {
        shopUI.SetActive(true);
    }
}
