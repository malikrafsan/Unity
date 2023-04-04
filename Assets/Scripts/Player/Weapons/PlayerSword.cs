using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    [SerializeField]
    private int damage = 200;
    private Animator animator;

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
                enemyHealth.TakeDamage(damage, enemyHealth.transform.position);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
