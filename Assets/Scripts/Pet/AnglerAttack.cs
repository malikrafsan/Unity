using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnglerAttack : MonoBehaviour
{
    public int damagePerShot = 10;
    public float timeBetweenBullets = 0.5f;
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

    private int level = 1;
    public int Level
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
        }
    }

    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;
        if (timer >= timeBetweenBullets && !GameControl.control.cantShoot && Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            if (!shootHit.collider.CompareTag("Player"))
            {
                Shoot();
            }
        }

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
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
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
            ElementalHealth elementalHealth = shootHit.collider.gameObject.GetComponent<ElementalHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerShot * level, shootHit.point);
            }

            if (elementalHealth != null)
            {

                elementalHealth.TakeDamage(damagePerShot * level, shootHit.point);
            }

            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }

    public void IncrementLevel()
    {
        level++;
    }
}
