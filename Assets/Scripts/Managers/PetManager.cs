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
        InvokeRepeating("Spawn", spawnTime, spawnTime);
        print("masuk invoking spawn pet");
    }

    void Spawn()
    {
        if (playerHealth.currentHealth <= 0f)
        {
            return;
        }

        //int spawnPointIndex = Random.Range (0, spawnPoints.Length);
        int spawnPet = Random.Range (0,3);
        Factory.FactoryMethod(spawnPet);
    }
}
