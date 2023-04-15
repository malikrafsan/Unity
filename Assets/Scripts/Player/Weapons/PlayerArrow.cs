using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrow : MonoBehaviour
{
    [SerializeField]
    private float torque;

    public Rigidbody rb;
    public Collider collider;

    private bool didHit;

    private float damageMultiplier = 1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        rb.isKinematic = true;
        collider.enabled = false;
    }

    public void Shoot(Vector3 force)
    {
        rb.isKinematic = false;
        rb.AddRelativeForce(force);
        transform.parent = null;
        collider.enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (didHit) return;

        didHit = true;

        IEnemyHealthHandler enemyHealth = other.gameObject.GetComponent<ElementalHealth>();
        enemyHealth ??= other.gameObject.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            var energy = 0.5 * rb.mass * rb.velocity.magnitude * rb.velocity.magnitude;
            var damage = energy * damageMultiplier;
            Debug.Log("damage: "+ damage);
            enemyHealth.TakeDamage((int)damage, enemyHealth.transform.position);
        }
        Destroy(gameObject);
    }
}
