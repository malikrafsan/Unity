using UnityEngine;
using System.Collections;


public class Punch : MonoBehaviour
{

    ElementalAttack attack;

    void Awake()
    {
        attack = transform.root.gameObject.transform.Find("DetectPlayer").GetComponent<ElementalAttack>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            attack.Attack(20);
            attack.Slower();
        }
    }
}