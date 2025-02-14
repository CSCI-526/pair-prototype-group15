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
        Debug.Log($"Bullet Direction: {newDirection}, Speed: {bulletSpeed}");
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        int bulletLayer = gameObject.layer;
        if (collision.gameObject.CompareTag("Player"))
        {
            bool bIsPlayerOneBullet = bulletLayer == GameManager.Instance.PLAYER_ONE_BULLET_LAYER;
            if((bIsPlayerOneBullet && collision.gameObject.layer == GameManager.Instance.PLAYER_TWO_LAYER) ||
                (!bIsPlayerOneBullet && collision.gameObject.layer == GameManager.Instance.PLAYER_ONE_LAYER))
            {
                PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage();
                }
                Destroy(gameObject);
            }
        }
        if (numBounces == 0)
        {
            Destroy(gameObject);
        }

        --numBounces;
        if (!collision.gameObject.CompareTag("InnerWall"))
        {
            Vector2 normal = collision.contacts[0].normal;
            UpdateDirection(Vector2.Reflect(direction.normalized, normal).normalized);
        }
    }

    public void IncreaseSpeed()
    {
        bulletSpeed += 1.0f;
        rb.velocity = this.direction * bulletSpeed;
    }
}
