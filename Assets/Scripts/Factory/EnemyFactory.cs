using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour, IFactory
{
    [SerializeField]
    GameObject[] enemyPrefab;

    [SerializeField]
    Transform[] spawnPoints;

    public GameObject FactoryMethod(int tag)
    {
        return Instantiate(enemyPrefab[tag], spawnPoints[tag].position, spawnPoints[tag].rotation);
    }
}
