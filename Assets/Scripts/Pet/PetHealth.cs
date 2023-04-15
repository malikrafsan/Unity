using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetHealth : MonoBehaviour
{
    public int startingHealth = 100;
    private int _currentHealth = -1;
    public int currentHealth
    {
        get => _currentHealth;
        set
        {
            _currentHealth = value;
        }
    }
    public float sinkSpeed = 2.5f;
    public AudioClip deathClip;
    public PetType petType;

    Animator anim;
    AudioSource petDeathAudio;
    CapsuleCollider capsuleCollider;
    bool isDeath;
    bool isSinking;


    void Awake()
    {
        anim = GetComponent<Animator>();
        petDeathAudio = GetComponent<AudioSource>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        if (currentHealth == -1)
        {
            currentHealth = startingHealth;
        }
    }


    void Update()
    {
        if (isSinking)
        {
            //Debug.Log("SINKING");
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
        if (GameControl.control.killPet)
        {
            Death();
            GameControl.control.killPet = false;
        }
    }


    public void TakeDamage(int amount)
    {
        if (isDeath)
            return;
        if (GameControl.control.fullHPPet)
            return;


        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Death();
        }
    }


    public void Death()
    {
        currentHealth = 0;
        isDeath = true;

        capsuleCollider.isTrigger = true;
        GameControl.control.petIdx = -1;

        anim.SetBool("IsDeath", isDeath);
        petDeathAudio.clip = deathClip;
        petDeathAudio.Play();
    }


    public void StartSinking()
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        Destroy(gameObject, 2f);
    }

    public PetType GetPetType()
    {
        return petType;
    }
}
