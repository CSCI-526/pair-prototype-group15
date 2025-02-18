using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;  // index TextMeshPro

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerHealth player1;
    public PlayerHealth player2;
    public int PLAYER_ONE_LAYER = 9;
    public int PLAYER_TWO_LAYER = 10;
    public int PLAYER_ONE_BULLET_LAYER = 11;
    public int PLAYER_TWO_BULLET_LAYER = 12;


    public TextMeshProUGUI player1HealthText;  // health presentation
    public TextMeshProUGUI player2HealthText;
    public TextMeshProUGUI gameOverText;  // gameover presentation

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateHealthUI();
        gameOverText.gameObject.SetActive(false); // hide Game Over at beginning
    }

    public void PlayerDefeated(PlayerHealth defeatedPlayer)
    {
        if(SceneManager.GetActiveScene().name == "TutorialScene"){
            SceneManager.LoadScene("MainMenu");
        }
        else{
            PlayerHealth winner = (defeatedPlayer == player1) ? player2 : player1;
            Debug.Log("Game Over! " + winner.playerName + " wins!");
            ShowGameOver(winner.playerName);
        }
    }

    private void ShowGameOver(string winnerName)
    {
        gameOverText.text = "GAME OVER\n" + winnerName + " Wins!";
        gameOverText.gameObject.SetActive(true);
        Time.timeScale = 0f; // pause the game
    }

    public void UpdateHealthUI()
    {
        if (player1HealthText != null)
            player1HealthText.text = player1.playerName + " " + player1.lives + "/3";
        if (player2HealthText != null)
            player2HealthText.text = player2.playerName + " " + player2.lives + "/3";
    }
}

