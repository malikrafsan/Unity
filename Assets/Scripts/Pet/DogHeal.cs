using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogHeal : MonoBehaviour
{
    public float timeBetweenHeals = 10f;
    public int healAmount = 10;

    GameObject player;
    PlayerHealth playerHealth;
    float timer;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenHeals && playerHealth.currentHealth < 100)
        {
            print("berhasil masuk mau heal");
            Heal ();
        }
        // if (playerHealth.currentHealth <= 0)
        // {
        //     anim.SetTrigger ("PlayerDead");
        // }
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
        playerHealth.HealDamage(healAmount);
        healAmount = 10;
    }
}
