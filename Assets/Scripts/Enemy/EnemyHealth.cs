using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;
    public EnemyType enemyType;


    GameObject pet;
    PetHealth petHealth;
    PetType petType;
    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDeath;
    bool isSinking;
    Temple questTemple;


    void Awake()
    {
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = gameObject.transform.Find("HitParticles").GetComponent<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        currentHealth = startingHealth;
        questTemple = FindObjectOfType<Temple>();
        pet = GameObject.FindGameObjectWithTag ("Pet");
        petHealth = pet.GetComponent <PetHealth> ();
        petType = petHealth.GetPetType();
        print(petType);
    }


    void Update()
    {
        if (isSinking)
        {
            //Debug.Log("SINKING");
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDeath)
            return;

        if (GameControl.control.cheatOneHitKill)
        {
            currentHealth = 0;
        }

        enemyAudio.Play();

        currentHealth -= amount;
        //print(currentHealth);
        if (petType == PetType.Bird)
        {
            currentHealth -= 10;
        }
        //print(currentHealth);

        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

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

        anim.SetBool("IsDeath", isDeath);

        enemyAudio.clip = deathClip;
        enemyAudio.Play();
        questTemple.OnDeathEnemy(enemyType);
    }


    public void StartSinking()
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        GameControl.control.currency += 5;
        ScoreManager.score += scoreValue;
        Destroy(gameObject, 2f);
    }
}
