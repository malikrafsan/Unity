using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class EnemyMovement : MonoBehaviour
{
    GameObject player;
    Transform playerPosition;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;


    private void Awake ()
    {
        //Find Object with Player Tag
        player = GameObject.FindGameObjectWithTag("Player");

        playerPosition = player.transform;

        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();

        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
    }


    void Update ()
    {
        if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            nav.SetDestination(playerPosition.position);
        }
        else
        {
            nav.enabled = false;
        }
    }
}
