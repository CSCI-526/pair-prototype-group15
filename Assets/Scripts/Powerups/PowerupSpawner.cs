using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject powerupPrefab;
    [SerializeField]
    private float respawnTime;
    private bool bShouldNaturallyRespawnPowerup = true;
    private bool bHasSpawnedPowerupBeenConsumed = true;
    private GameObject childPowerup = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(bShouldNaturallyRespawnPowerup && bHasSpawnedPowerupBeenConsumed)
        {
            childPowerup = Instantiate(powerupPrefab, gameObject.transform);
            Powerup child = childPowerup.GetComponent<Powerup>();
            if(child)
            {
                child.SetSpawnerParent(gameObject);
            }
            bHasSpawnedPowerupBeenConsumed = false;
            bShouldNaturallyRespawnPowerup = false;
        }
    }

    public void AllowRespawn()
    {
        bHasSpawnedPowerupBeenConsumed = true;
        StartCoroutine(PowerupCooldown());
    }
    private IEnumerator PowerupCooldown()
    {
        float timer = 0.0f;
        while (timer < respawnTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        bShouldNaturallyRespawnPowerup = true;
    }

}
