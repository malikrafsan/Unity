using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public float spawnTime = 5f;

    [SerializeField]
    MonoBehaviour factory;
    IFactory Factory { get { return factory as IFactory; } }

    void Start()
    {
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    public void Spawn(int tag)
    {
        if (playerHealth.currentHealth <= 0f)
        {
            return;
        }
        int spawnPet = tag;
        Factory.FactoryMethod(spawnPet);
    }

}
