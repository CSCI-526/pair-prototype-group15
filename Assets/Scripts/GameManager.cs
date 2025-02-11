using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton instance for easy access from other scripts
    public static GameManager Instance;

    // References to the two players; assign these in the Inspector
    public PlayerHealth player1;
    public PlayerHealth player2;

    private void Awake()
    {
        // Initialize the singleton instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Called when a player's lives have reached zero.
    /// </summary>
    /// <param name="defeatedPlayer">The player who was defeated.</param>
    public void PlayerDefeated(PlayerHealth defeatedPlayer)
    {
        // Determine the winner based on which player was defeated
        PlayerHealth winner = (defeatedPlayer == player1) ? player2 : player1;
        Debug.Log("Game Over! " + winner.playerName + " wins!");

        // Additional game over logic can be added here, such as pausing the game,
        // displaying a victory screen, or restarting the game.
    }
}

