using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed = 10.0f;
    [SerializeField]
    private int numBounces = 3;
    private Vector2 direction = Vector2.zero;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void UpdateDirection(Vector2 newDirection)
    {
        this.direction = newDirection;
        rb.velocity = this.direction * bulletSpeed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (numBounces == 0 || collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            // tesgt
        }
        --numBounces;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 normal = collision.contacts[0].normal;
            UpdateDirection(Vector2.Reflect(direction.normalized, normal).normalized);
        }
    }
}
