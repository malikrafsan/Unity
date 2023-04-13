using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogHeal : MonoBehaviour
{
    public float timeBetweenHeals = 10f;
    public int healAmount = 10;
    public AudioClip healClip;

    Animator anim;
    AudioSource healAudio;
    GameObject player;
    PlayerHealth playerHealth;
    float timer;

    void Awake ()
    {
        anim = GetComponent <Animator> ();
        healAudio = GetComponent <AudioSource> ();
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenHeals && playerHealth.currentHealth < 100 && playerHealth.currentHealth > 0)
        {
            Heal ();
        }
        // if (playerHealth.currentHealth <= 0)
        // {
        //     anim.SetTrigger ("PlayerDead");
        // }

        if (playerHealth.currentHealth <= 0)
        {
            anim.SetBool("PlayerDeath", true);
        }
    }

    void Heal ()
    {
        timer = 0f;
        int playerHealthLost = 100 - playerHealth.currentHealth;
        if (playerHealthLost < healAmount){
            healAmount = playerHealthLost;
            print(healAmount);
        }
        //print("masuk healing");
        healAudio.Play ();
        playerHealth.HealDamage(healAmount);
        healAmount = 10;
    }
}
