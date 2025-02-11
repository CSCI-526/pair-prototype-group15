using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    // The initial number of lives (e.g., 3 lives)
    public int lives = 3;

    // The name of the player (can be set in the Inspector to distinguish between players)
    public string playerName = "Player";

    /// <summary>
    /// Call this method when the player takes damage.
    /// By default, each hit decreases the lives by 1.
    /// </summary>
    /// <param name="damage">Damage value, typically 1</param>
    public void TakeDamage(int damage = 1)
    {
        // Decrease the number of lives
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
}
