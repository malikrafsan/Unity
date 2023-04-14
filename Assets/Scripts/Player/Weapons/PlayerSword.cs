using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour, WeaponHandler
{
    private int damage = 20;
    private Animator animator;

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

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<EnemyHealth>(out var enemyHealth))
        {
            if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("SwordSwing"))
            {
                enemyHealth.TakeDamage(damage * level, enemyHealth.transform.position);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IncrementLevel()
    {
        level++;
    }
}
