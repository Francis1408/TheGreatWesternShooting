using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    // Singleton instance of GameManager
    public static GameManager Instance { get; private set; }

    // Score and game state variables
    private int currentScore = 0;
    private bool isGameOver = false;
   
    // UI variables
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI playerLifeText;
    public TextMeshProUGUI gameOverText;

    private void Awake()
    {
        // Ensure there is only one instance of the GameManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object between scene loads
        }
        else
        {
            Destroy(gameObject);
        }
        gameOverText.gameObject.SetActive(false);
    }
    

    // Method to add points to the score when an enemy is killed
    public void AddPoints(int points)
    {
        if (!isGameOver)
        {
            currentScore += points;
            scoreText.text = currentScore.ToString();
            //update UI with the score
            scoreText.text = currentScore.ToString();
        }
    }

    // Method to call when the game ends (e.g., player dies, or all enemies are killed)
    private void EndGame()
    {
        isGameOver = true;
        gameOverText.gameObject.SetActive(true);
        Debug.Log("Game Over!");
        // You can add logic to handle the game over state here, like showing a Game Over screen or restarting the level.
    }

    // Method to reset the game or restart the level
    public void RestartGame()
    {
        isGameOver = false;
        currentScore = 0;
        // Optionally, reload the current scene or reset any game state
        Debug.Log("Game Restarted");
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Example to reload the current scene
    }

    // Optional: Get the current score
    public int GetCurrentScore()
    {
        return currentScore;
    }

    public void PlayerOnDeath()
    {
        playerLifeText.text = Player.Instance.lifes.ToString();
       
        if (Player.Instance.lifes == 0)
        {
            EndGame();
        }
    }
}