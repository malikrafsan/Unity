using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetMovement : MonoBehaviour
{
    public PetType petType;
    public float timeBetweenBirdAudio = 15f;

    GameObject player;
    Transform playerPosition;
    PlayerHealth playerHealth;
    GameObject enemy;
    Transform enemyPosition;
    EnemyHealth enemyHealth;
    PetHealth petHealth;
    AudioSource birdMoveAudio;
    UnityEngine.AI.NavMeshAgent nav;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerPosition = player.transform;
        playerHealth = player.GetComponent <PlayerHealth> ();
        petHealth = GetComponent <PetHealth> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();

        if (petType == PetType.Bird)
        {
            birdMoveAudio = GetComponent <AudioSource> ();
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        if (enemy != null)
        {
            enemyPosition = enemy.transform;
            enemyHealth = enemy.GetComponent <EnemyHealth> ();
        }
        
        if (petHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            if (petType == PetType.Angler && enemy != null)
            {
                if (enemyHealth.currentHealth > 0)
                {
                    nav.SetDestination (enemyPosition.position);
                }
            }
            else
            {
                nav.SetDestination (playerPosition.position);
                if (petType == PetType.Bird && timer >= timeBetweenBirdAudio)
                {
                    timer = 0f;
                    birdMoveAudio.Play ();
                }
            }
        }
        else
        {
            nav.enabled = false;
        }
    }
}