using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public enum PowerupType
{
    POWERUP_SHIELD,
    POWERUP_SPLITSHOT,
    POWERUP_NONE
}
public class Powerup : MonoBehaviour
{
    [SerializeField]
    PowerupType powerupType = PowerupType.POWERUP_NONE;
    [SerializeField]
    private float powerupDuration = 2.0f;
    public static event Action<GameObject, PowerupType, float> onPowerupConsumed;
    protected GameObject spawnerParent;
    
    public void SetSpawnerParent(GameObject parent)
    {
        spawnerParent = parent;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Give player powerup / effect
            PowerupConsumed(collision.gameObject);
        }
    }
    protected void PowerupConsumed(GameObject caller)
    {
        onPowerupConsumed?.Invoke(caller, powerupType, powerupDuration);
        // tell parent that it's child has been consumed 
        PowerupSpawner powerupSpawner = spawnerParent.GetComponent<PowerupSpawner>();
        if (powerupSpawner)
        {
            powerupSpawner.AllowRespawn();
        }
        // destroy this
        Destroy(gameObject);
    }
}
