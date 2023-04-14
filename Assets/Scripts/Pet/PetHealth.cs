using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public AudioClip deathClip;
    public PetType petType;

    Animator anim;
    AudioSource petDeathAudio;
    CapsuleCollider capsuleCollider;
    bool isDeath;
    bool isSinking;


    void Awake ()
    {   
        anim = GetComponent <Animator> ();
        petDeathAudio = GetComponent <AudioSource> ();
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

        capsuleCollider.isTrigger = true;

        anim.SetBool("IsDeath", isDeath);
        petDeathAudio.clip = deathClip;
        petDeathAudio.Play ();
    }


    public void StartSinking ()
    {
        GetComponent<UnityEngine.AI.NavMeshAgent> ().enabled = false;
        GetComponent<Rigidbody> ().isKinematic = true;
        isSinking = true;
        Destroy (gameObject, 2f);
    }
}
