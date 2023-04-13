using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public readonly int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public Image freezeImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    float freezeSpeed;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    public Color frozenColor = new Color(0f, 0f, 1f, 0.1f);

    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDeath;
    bool damaged;
    bool cheatNoDamage = false;
    bool frozen;

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();

        currentHealth = startingHealth;
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

        if (frozen)
        {
            freezeImage.color = frozenColor;
            StartCoroutine(UnFreeze(freezeSpeed));
        }

        damaged = false;
        frozen = false;
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

    public void TakeFreeze(float timeFreeze)
    {
        GameControl.control.cantShoot = true;
        freezeSpeed = timeFreeze;
        frozen = true;
    }

    IEnumerator UnFreeze(float frozenTime)
    {
        yield return new WaitForSeconds(frozenTime);
        freezeImage.color = Color.Lerp(freezeImage.color, Color.clear, 5f);
        GameControl.control.cantShoot = false;
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

    public void SetCheatNoDamage(bool value)
    {
        cheatNoDamage = value;
    }
}
