using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float damage;
    private Health enemyHealth;

    private void Awake()
    {
        enemyHealth = GetComponent<Health>();
        if (enemyHealth == null) Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.Player))
        {
            var healthScript = collision.gameObject.GetComponent<Health>();
            if (healthScript == null) healthScript = collision.gameObject.GetComponentInParent<Health>();
            if (healthScript == null) healthScript = collision.gameObject.GetComponentInChildren<Health>();
            if (healthScript == null) {
                Debug.LogError("HealthScript on Object not Found");
                return;
            }

            healthScript.TakeDamage(damage);
        }
    }
}
