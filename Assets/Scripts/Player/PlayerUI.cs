using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private GameObject powerupDurationGO;
    [SerializeField]
    private Slider powerupDurationSlider;
    [SerializeField]
    private TextMeshProUGUI powerupDurationTypeText;
    private Coroutine powerupDurationCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        Powerup.onPowerupConsumed += OnPowerupUsed;
        PlayerHealth.onShieldUsed += OnShieldUsed;
    }
    private void OnDestroy()
    {
        Powerup.onPowerupConsumed -= OnPowerupUsed;
        PlayerHealth.onShieldUsed -= OnShieldUsed;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnShieldUsed()
    {
        powerupDurationGO.SetActive(false);
    }
    private void OnPowerupUsed(GameObject caller, PowerupType powerupType, float powerupDuration)
    {
        if(!caller || caller != gameObject)
        {
            return;
        }
        powerupDurationGO.SetActive(true);
        if (powerupType == PowerupType.POWERUP_SPLITSHOT)
        {
            if (powerupDurationCoroutine != null)
            {
                StopCoroutine(powerupDurationCoroutine);
            }
            powerupDurationTypeText.SetText("Split Shot");
            powerupDurationSlider.maxValue = powerupDuration;
            powerupDurationSlider.value = powerupDuration;
            powerupDurationCoroutine = StartCoroutine(PowerupTimer(powerupDuration));
        }
        else if (powerupType == PowerupType.POWERUP_SHIELD)
        {
            if(powerupDurationCoroutine != null)
            {
                StopCoroutine (powerupDurationCoroutine);
            }
            powerupDurationTypeText.SetText("Shield");
            powerupDurationSlider.maxValue = powerupDuration;
            powerupDurationSlider.value = powerupDuration;
            powerupDurationCoroutine = StartCoroutine(PowerupTimer(powerupDuration));
        }
    }
    private IEnumerator PowerupTimer(float duration)
    {
        float timer = duration;
        while (timer > 0.0f)
        {
            timer -= Time.deltaTime;
            powerupDurationSlider.value = timer;
            yield return null;
        }
        powerupDurationTypeText.SetText("");
        powerupDurationGO.SetActive(false);
    }
}
