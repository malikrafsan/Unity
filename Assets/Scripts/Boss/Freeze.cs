using UnityEngine;
using System.Collections;


public class Freeze : MonoBehaviour
{

    ElementalAttack attack;

    void Awake()
    {
        attack = transform.root.gameObject.transform.Find("DetectPlayer").GetComponent<ElementalAttack>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            attack.Slower();
            attack.Attack(5);
        }
    }
}