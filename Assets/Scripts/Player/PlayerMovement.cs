using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private bool bIsPlayerOne = true;
    [SerializeField]
    private float movementSpeed = 1.0f;
    [SerializeField]
    private float rotationSpeed = 1.0f;
    [SerializeField]
    private float reloadTime = 3.0f;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform firePoint;
    private Rigidbody2D playerRigidbody2D;
    private KeyCode rotateLeft;
    private KeyCode rotateRight;
    private KeyCode moveUp;
    private KeyCode moveDown;
    private KeyCode shootKey;
    private bool bCanShoot = true;
    private bool bIsSplitShot;
    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        playerRigidbody2D.freezeRotation = true;
        rotateLeft = bIsPlayerOne ? KeyCode.A : KeyCode.LeftArrow;
        rotateRight = bIsPlayerOne ? KeyCode.D : KeyCode.RightArrow;
        moveUp = bIsPlayerOne ? KeyCode.W : KeyCode.UpArrow;
        moveDown = bIsPlayerOne ? KeyCode.S : KeyCode.DownArrow;
        shootKey = bIsPlayerOne ? KeyCode.V : KeyCode.Slash;
        Powerup.powerupConsumed += OnPowerupUsed;
    }
    private void OnDisable()
    {
        Powerup.powerupConsumed -= OnPowerupUsed;
    }
    private void OnDestroy()
    {
        Powerup.powerupConsumed -= OnPowerupUsed;
    }
    // Update is called once per frame
    void Update()
    {
        if (bCanShoot && Input.GetKey(shootKey))
        {
            bCanShoot = false;
            Shoot();
        }
    }
    private void FixedUpdate()
    {
        if (Input.GetKey(rotateLeft))
        {
            transform.Rotate(0, 0, rotationSpeed * Time.fixedDeltaTime);
        }
        if (Input.GetKey(rotateRight))
        {
            transform.Rotate(0, 0, -rotationSpeed * Time.fixedDeltaTime);
        }
        if (Input.GetKey(moveUp))
        {
            transform.Translate(0, movementSpeed * Time.fixedDeltaTime, 0);
        }
        if (Input.GetKey(moveDown))
        {
            transform.Translate(0, -movementSpeed * Time.fixedDeltaTime, 0);
        }
    }
    private void OnPowerupUsed(PowerupType powerupType, float powerupDuration) 
    {
        if(powerupType == PowerupType.POWERUP_SPLITSHOT)
        {
            bIsSplitShot = true;
            StartCoroutine(PowerupTimer(powerupDuration));
        }
    }

    private void Shoot()
    {
        if (bulletPrefab == null)
        {
            Debug.LogError("bulletPrefab is not assigned!");
            return;
        }

        if (firePoint == null)
        {
            Debug.LogError("firePoint is not assigned!");
            return;
        }

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        BulletBehavior bulletBehavior = bullet.GetComponent<BulletBehavior>();
        if (bulletBehavior != null)
        {
            bulletBehavior.UpdateDirection(firePoint.up.normalized);
        }
        else
        {
            Debug.LogError("BulletBehavior script is missing on bulletPrefab!");
        }

        Debug.Log("Shooting bullet...");
        bCanShoot = false;

        if (bIsSplitShot)
        {
            Quaternion leftRotation = Quaternion.Euler(0, 0, -30f);  // Modify Z-axis rotation
            Quaternion rightRotation = Quaternion.Euler(0, 0, 30f);  // Modify Z-axis rotation

            GameObject bulletOne = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * leftRotation);
            bulletOne.GetComponent<BulletBehavior>().UpdateDirection(bulletOne.transform.up.normalized);

            GameObject bulletTwo = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * rightRotation);
            bulletTwo.GetComponent<BulletBehavior>().UpdateDirection(bulletTwo.transform.up.normalized);
        }

        StartCoroutine(ShootingCooldown());
    }


    private IEnumerator ShootingCooldown()
    {
        float timer = 0.0f;
        while (timer < reloadTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        bCanShoot = true;
    }

    private IEnumerator PowerupTimer(float duration)
    {
        float timer = 0.0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        bIsSplitShot = false;
    }
}
