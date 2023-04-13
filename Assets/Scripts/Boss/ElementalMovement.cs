using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class ElementalMovement : MonoBehaviour
{
    GameObject player;
    Transform playerPosition;
    PlayerHealth playerHealth;
    ElementalHealth enemyHealth;
    public UnityEngine.AI.NavMeshAgent nav;
    // Start is called before the first frame update

    private void Awake()
    {
        //Find Object with Player Tag
        player = GameObject.FindGameObjectWithTag("Player");

        playerPosition = player.transform;

        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<ElementalHealth>();

        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }


    void Update()
    {
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0 && nav.enabled)
        {
            nav.SetDestination(playerPosition.position);
        }
        else
        {
            // Debug.Log("Enemy or Player is dead");
            nav.enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
