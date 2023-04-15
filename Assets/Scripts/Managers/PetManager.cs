using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetManager : MonoBehaviour
{
    private PlayerHealth _playerHealth;
    public PlayerHealth playerHealth
    {
        get
        {
            if (_playerHealth == null )
            {
                _playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
            }

            return _playerHealth;
        }
    }
    public float spawnTime = 5f;

    [SerializeField]
    MonoBehaviour factory;
    IFactory Factory { get { return factory as IFactory; } }

    public GameObject Spawn(int tag)
    {
        if (playerHealth.currentHealth <= 0f)
        {
            return null;
        }
        int spawnPet = tag;
        return Factory.FactoryMethod(spawnPet);
    }

}
