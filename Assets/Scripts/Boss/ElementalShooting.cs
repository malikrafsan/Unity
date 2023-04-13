using UnityEngine;
using System.Collections;


public class ElementalShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;

    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;
    float minDist = 7;

    GameObject player;

    PlayerHealth playerHealth;

    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        float dist = Vector3.Distance(player.transform.position, transform.position);

        // If far away, shoot normally
        ShootAbility(dist);

        // If close attack normally

    }

    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

    void Shoot()
    {
        timer = 0f;

        gunAudio.Play();

        gunLight.enabled = true;

        gunParticles.Stop();
        gunParticles.Play();

        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        shootRay.origin = transform.position;

        // randomize x, z the same but y is the player's y for shootray direction
        float randomX = Random.Range(player.transform.position.x - 1.5f, player.transform.position.x + 1.5f);
        Vector3 playerPos = new Vector3(randomX, player.transform.position.y + 0.3f, player.transform.position.z);
        Vector3 currentPos = transform.position;

        shootRay.direction = Vector3.Normalize(playerPos - transform.position);

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            PlayerHealth playerHealth = shootHit.collider.gameObject.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damagePerShot);
            }
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }


    void ShootAbility(float curr_dist)
    {
        if (playerHealth.currentHealth <= 0)
        {
            return;
        }

        if (curr_dist > minDist && timer >= timeBetweenBullets)
        {
            Shoot();
        }

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }
}