using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrow : MonoBehaviour
{
    [SerializeField]
    private int damage;

    [SerializeField]
    private float torque;

    private Rigidbody rb;

    private bool didHit;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        Destroy(gameObject, 10);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hitting " + other);

        if (didHit) return;

        didHit = true;
        if (other.gameObject.TryGetComponent<EnemyHealth>(out var enemyHealth))
        {
            enemyHealth.TakeDamage(damage, enemyHealth.transform.position);
        }

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
    }
}
