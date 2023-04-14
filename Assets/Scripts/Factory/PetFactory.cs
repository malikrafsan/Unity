using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetFactory : MonoBehaviour, IFactory
{
    [SerializeField]
    GameObject[] petPrefab;

    [SerializeField]
    Transform[] spawnPoints;

    public GameObject FactoryMethod(int tag)
    {
        return Instantiate(petPrefab[tag], spawnPoints[tag].position, spawnPoints[tag].rotation);
    }
}
