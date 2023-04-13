using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;


    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    GameObject pet;
    PetHealth petHealth;
    EnemyHealth enemyHealth;
    bool playerInRange;
    bool petInRange;
    float timer;


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
        pet = GameObject.FindGameObjectWithTag ("Pet");
        petHealth = pet.GetComponent <PetHealth> ();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
    }

    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject == player && other.isTrigger == false)
        {
            playerInRange = true;
        }
        if (other.gameObject == pet && other.isTrigger == false)
        {
            petInRange = true;
        }
    }

    void OnTriggerExit (Collider other)
    {
        if(other.gameObject == player && other.isTrigger == false)
        {
            playerInRange = false;
        }
        if(other.gameObject == pet && other.isTrigger == false)
        {
            petInRange = false;
        }
    }


    void Update ()
    {
        timer += Time.deltaTime;

        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack ();
        }

        if(timer >= timeBetweenAttacks && petInRange && enemyHealth.currentHealth > 0)
        {
            AttackPet ();
        }

        if (playerHealth.CurrentHealth <= 0)
        {
            anim.SetBool("PlayerDeath", true);
        }
    }


    void Attack ()
    {
        timer = 0f;

        if (playerHealth.CurrentHealth > 0)
        {
            playerHealth.TakeDamage (attackDamage);
        }
    }

    void AttackPet ()
    {
        timer = 0f;

        if (petHealth.currentHealth > 0)
        {
            petHealth.TakeDamage (attackDamage);
        }
    }
}
