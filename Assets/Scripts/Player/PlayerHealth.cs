using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    // The initial number of lives (e.g., 3 lives)
    public int lives = 3;

    // The name of the player (can be set in the Inspector to distinguish between players)
    public string playerName = "Player";
    private Coroutine powerupCoroutine;
    private bool bHasShieldBuff = false;
    public static event Action onShieldUsed;
    [SerializeField]
    private GameObject shieldIcon;

    private void Start()
    {
        Powerup.onPowerupConsumed += OnPowerupUsed;
    }
    private void OnDestroy()
    {
        Powerup.onPowerupConsumed -= OnPowerupUsed;
    }
    /// <summary>
    /// Call this method when the player takes damage.
    /// By default, each hit decreases the lives by 1.
    /// </summary>
    /// <param name="damage">Damage value, typically 1</param>
    public void TakeDamage(int damage = 1)
    {
        // Decrease the number of lives
        if(!bHasShieldBuff)
        {
            lives -= damage;
            if (lives <= 0)
            {
                lives = 0;
                Debug.Log(playerName + " has run out of lives!");
                // Notify the GameManager that this player is defeated
                GameManager.Instance.PlayerDefeated(this);
            }
            else
            {
                Debug.Log(playerName + " lost a life, " + lives + " lives remaining.");
            }
        }
        else
        {
            // Shielded from damage 
            onShieldUsed?.Invoke();
            bHasShieldBuff = false;
            shieldIcon.SetActive(false);
        }
    }

    private void OnPowerupUsed(GameObject caller, PowerupType powerupType, float powerupDuration)
    {
        if (!caller || caller != gameObject)
        {
            return;
        }
        if (powerupType == PowerupType.POWERUP_SHIELD)
        {
            if (powerupCoroutine != null)
            {
                StopCoroutine(powerupCoroutine);
            }
            bHasShieldBuff = true;
            shieldIcon.SetActive(true);
            powerupCoroutine = StartCoroutine(PowerupTimer(powerupDuration));
        }
        else
        {
            if (powerupCoroutine != null)
            {
                StopCoroutine(powerupCoroutine);
                bHasShieldBuff = false;
                shieldIcon.SetActive(false);
            }
        }
    }

    private IEnumerator PowerupTimer(float duration)
    {
        float timer = 0.0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        bHasShieldBuff = false;
        shieldIcon.SetActive(false);
    }
}
