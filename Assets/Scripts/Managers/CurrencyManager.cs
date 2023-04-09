using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CurrencyManager : MonoBehaviour
{
    Text text;

    void Awake ()
    {
        text = GetComponent <Text> ();
    }

    void Update ()
    {
        text.text = "" + GameControl.control.currency;
    }
}
