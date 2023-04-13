using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public readonly int startingHealth = 100;
    private int currentHealth = -1;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    public int CurrentHealth
    {
        get => currentHealth;
        set
        {
            currentHealth = value;
            healthSlider.value = currentHealth;
        }
    }


    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDeath;                                                
    bool damaged;   
    bool cheatNoDamage = false;                                            


    void Awake()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();

        if (currentHealth == -1)
        {
            currentHealth = startingHealth;
        }

        Debug.Log("currentHealt: "+ currentHealth);
        healthSlider.value = currentHealth;
    }


    void Update()
    {
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        damaged = false;
    }


    public void TakeDamage(int amount)
    {
        if (cheatNoDamage) return;
        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        playerAudio.Play();

        if (currentHealth <= 0 && !isDeath)
        {
            Death();
        }
    }

    public void HealDamage(int amount)
    {
        currentHealth += amount;
        healthSlider.value = currentHealth;
    }


    void Death()
    {
        isDeath = true;

        playerShooting.DisableEffects();

        anim.SetBool("IsDeath", isDeath);

        playerAudio.clip = deathClip;
        playerAudio.Play();
            
        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }

    public void SetCheatNoDamage(bool value) {
        cheatNoDamage = value;
    }
}
