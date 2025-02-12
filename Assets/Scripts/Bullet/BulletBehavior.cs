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
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage();
            Destroy(gameObject);
        }
        else if (numBounces == 0)
        {
            Destroy(gameObject);
        }
        else
        {
            --numBounces;
            if (!collision.gameObject.CompareTag("InnerWall"))
            {
                Vector2 normal = collision.contacts[0].normal;
                UpdateDirection(Vector2.Reflect(direction.normalized, normal).normalized);
            }
        }
    }

    public void IncreaseSpeed()
    {
        bulletSpeed += 1.0f;
        rb.velocity = this.direction * bulletSpeed;
    }
