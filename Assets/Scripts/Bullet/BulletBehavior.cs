using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed = 10.0f;
    private Vector2 direction = Vector2.zero;
    private bool bCanDealDamage = false;
    [SerializeField]
    private Color activeColor;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public bool GetCanDealDamage() { return bCanDealDamage; }
    public void UpdateSpriteColor()
    {
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        if (sprite)
        {
            sprite.color = activeColor;
        }
        bCanDealDamage = true;
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
        if (collision.gameObject.CompareTag("Player") && bCanDealDamage)
        {
            
            bool bIsPlayerOneBullet = bulletLayer == GameManager.Instance.PLAYER_ONE_BULLET_LAYER;
            if ((bIsPlayerOneBullet && collision.gameObject.layer == GameManager.Instance.PLAYER_TWO_LAYER) ||
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

        if (!collision.gameObject.CompareTag("InnerWall"))
        {
            Destroy(gameObject);
        }
    }

    public void IncreaseSpeed()
    {
        bulletSpeed += 1.0f;
        rb.velocity = this.direction * bulletSpeed;
    }
}
