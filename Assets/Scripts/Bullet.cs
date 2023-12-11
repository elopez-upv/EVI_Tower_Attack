using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject explosionPrefab;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;

    
    private Transform target;

    public void SetTarget(Transform target) {
        this.target = target;
    }
    
    private void FixedUpdate() {
        if (!target) return;

        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        other.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);
        StartExplosionAnimation();
        Destroy(gameObject);
    }

    private void StartExplosionAnimation() {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }
}
