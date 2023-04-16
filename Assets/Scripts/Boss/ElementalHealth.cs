using UnityEngine;

public class ElementalHealth : MonoBehaviour, IEnemyHealthHandler
{
    public int startingHealth = 500;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 20;
    public AudioClip deathClip;
    public EnemyType enemyType;

    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDeath;
    Temple questTemple;


    void Awake()
    {

        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GameObject.Find("ElementalHit").GetComponent<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        currentHealth = startingHealth;
        questTemple = FindObjectOfType<Temple>();
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

        Debug.Log("Masuk sini setelah mati or not?");

        anim.SetBool("IsHurt", isDeath);

        enemyAudio.clip = deathClip;
        enemyAudio.Play();
        questTemple.OnDeathEnemy(enemyType);

        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        GameControl.control.currency += 50;
        ScoreManager.score += scoreValue;
        StartCoroutine(GameControl.control.StartFade(enemyAudio, 2f, 0f, gameObject));
    }
}
