using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElementalAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 5f;

    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    ElementalHealth elementalHealth;
    ElementalMovement elementalMovement;
    bool playerInRange;
    float timer;

    public float timeFreezeEffect = 5f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        elementalHealth = transform.root.gameObject.GetComponent<ElementalHealth>();
        elementalMovement = transform.root.gameObject.GetComponent<ElementalMovement>();
        anim = transform.root.gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetBool("PlayerDeath", true);
        }

        StopMoving();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
            anim.SetBool("PlayerInRange", playerInRange);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
            anim.SetBool("PlayerInRange", playerInRange);
        }
    }

    public void Attack(int attackDamage)
    {
        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }

    public void Slower()
    {
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        if (playerMovement.speed <= 3f)
        {
            return;
        }
        playerMovement.speed -= 3f;
        // Debug.Log("Slower " + playerMovement.speed);
        StartCoroutine(DissapearFrostBite(timeFreezeEffect));
        playerHealth.TakeFreeze(timeFreezeEffect);
    }

    public void StopMoving()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Elemental@UnarmedAttack01"))
        {
            elementalMovement.nav.enabled = false;
            return;
        }
        elementalMovement.nav.enabled = true;
    }

    IEnumerator DissapearFrostBite(float time)
    {
        yield return new WaitForSeconds(time);
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.ResetSpeed();
        Debug.Log("Reset Speed " + playerMovement.speed);
    }
}
