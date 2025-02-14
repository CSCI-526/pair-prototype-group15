using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int lives = 3;
    public string playerName = "Player";

    private void Start()
    {
        GameManager.Instance.UpdateHealthUI();
    }

    public void TakeDamage(int damage = 1)
    {
        lives -= damage;
        GameManager.Instance.UpdateHealthUI();
        if (lives <= 0)
        {
            lives = 0;
            Debug.Log(playerName + " has run out of lives!");
            GameManager.Instance.PlayerDefeated(this);
        }
        else
        {
            Debug.Log(playerName + " lost a life, " + lives + " lives remaining.");
        }
    }
}
