using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotGunning : MonoBehaviour, WeaponHandler
{
    [SerializeField]
    private float damagePerShot = 30f;
    [SerializeField]
    private float timeBetweenBullets = 1f;
    [SerializeField]
    private float range = 100f;
    public GameObject prefabEffect;

    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    readonly float effectsDisplayTime = 0.2f;

    private readonly float maxDist = 10f;

    int numBullet = 3;
    List<GameObject> effects = new List<GameObject>();

    private int level = 1;
    public int Level
    {
        get
        {
            return level;
        }
        set
        {
            var diff = value - level;
            for (int i = 0; i < diff; i++)
            {
                AddBullet();
                numBullet++;
            }

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

        for (int i = 0; i < numBullet; i++)
        {
            AddBullet();
        }
    }

    private void AddBullet()
    {
        var instance = Instantiate(prefabEffect, transform);
        instance.transform.SetParent(transform, false);
        instance.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        effects.Add(instance);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && !GameControl.control.cantShoot)
        {
            Shoot();
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

        foreach (var effect in effects)
        {
            effect.GetComponent<Light>().enabled = false;
            effect.GetComponent<LineRenderer>().enabled = false;
        }
    }

    void Shoot()
    {
        timer = 0f;

        gunAudio.Play();

        gunParticles.Stop();
        gunParticles.Play();

        var rangeAngle = 30;

        for (int i = 0; i < numBullet; i++)
        {
            var gunLight = effects[i].GetComponent<Light>();
            var gunLine = effects[i].GetComponent<LineRenderer>();

            gunLight.enabled = true;
            gunLine.enabled = true;

            gunLine.SetPosition(0, transform.position);
            shootRay.origin = transform.position;

            var customDir = UnityEngine.Random.Range(-rangeAngle, rangeAngle);
            shootRay.direction = Quaternion.Euler(0, customDir, 0) * transform.forward;

            if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
            {
                IEnemyHealthHandler enemyHealth = shootHit.collider.GetComponent<ElementalHealth>();
                enemyHealth ??= shootHit.collider.GetComponent< EnemyHealth>(); 

                if (enemyHealth != null)
                {
                    var dist = Vector3.Distance(shootHit.transform.position, transform.position);
                    if (dist <= maxDist)
                    {
                        Debug.Log("Enemy is On Distance");
                        int finalDamage = (int)(damagePerShot / Math.Sqrt(dist));
                        enemyHealth.TakeDamage(finalDamage, shootHit.point);
                    }
                    else
                    {
                        Debug.Log("Enemy out of distance");
                    }
                }

                gunLine.SetPosition(1, shootHit.point);
            }
            else
            {
                gunLine.SetPosition(1, shootRay.direction * range);
            }
        }
    }

    public void IncrementLevel()
    {
        level++;
        AddBullet();
        numBullet++;
    }
}