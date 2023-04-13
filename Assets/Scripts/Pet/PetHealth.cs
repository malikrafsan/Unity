using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    //public AudioClip deathClip;
    //public EnemyType enemyType;


    Animator anim;
    //AudioSource enemyAudio;
    CapsuleCollider capsuleCollider;
    bool isDeath;
    bool isSinking;


    void Awake ()
    {   
        anim = GetComponent <Animator> ();
        //enemyAudio = GetComponent <AudioSource> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();

        currentHealth = startingHealth;
    }


    void Update ()
    {
        if (isSinking)
        {
            //Debug.Log("SINKING");
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage (int amount)
    {
        if (isDeath)
            return;
        
        // if (GameControl.control.cheatOneHitKill)
        // {
        //     currentHealth = 0;
        // }


        //enemyAudio.Play ();

        currentHealth -= amount;
        print(currentHealth);

        if (currentHealth <= 0)
        {
            Death ();
        }
    }


    public void Death ()
    {
        currentHealth = 0;
        isDeath = true;
        print("PET MATIIIIIIIIIII");

        capsuleCollider.isTrigger = true;

        anim.SetBool("IsDeath", isDeath);

        //enemyAudio.clip = deathClip;
        //enemyAudio.Play ();
    }


    public void StartSinking ()
    {
        GetComponent<UnityEngine.AI.NavMeshAgent> ().enabled = false;
        GetComponent<Rigidbody> ().isKinematic = true;
        isSinking = true;
        Destroy (gameObject, 2f);
    }
}
