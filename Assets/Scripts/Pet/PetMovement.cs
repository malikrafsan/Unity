using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetMovement : MonoBehaviour
{
    GameObject player;
    Transform playerPosition;
    PlayerHealth playerHealth;
    PetHealth petHealth;
    UnityEngine.AI.NavMeshAgent nav;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerPosition = player.transform;
        playerHealth = player.GetComponent <PlayerHealth> ();
        petHealth = GetComponent <PetHealth> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
    }

    // Update is called once per frame
    void Update()
    {
        if (petHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            nav.SetDestination (playerPosition.position);
        }
        else
        {
            nav.enabled = false;
        }
    }
}
