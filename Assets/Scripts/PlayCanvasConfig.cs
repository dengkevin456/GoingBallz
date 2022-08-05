using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class PlayCanvasConfig : MonoBehaviour
{
    public static bool gameIsPaused;
    public static bool gameOver;
    public static float money = 0f;
    [Header("References")]
    public GameObject pauseScreenUI;
    public GameObject gameOverUI;
    public TextMeshPro scoreText;

    private void HandlePause()
    {
        Time.timeScale = gameIsPaused ? 0f : 1f;
        pauseScreenUI.SetActive(gameIsPaused);
    }

    private void HandleGameOver()
    {
        gameOverUI.SetActive(gameOver);
    }

    private void HandleScoreText()
    {
        scoreText.text = $"{money}$";
    }
    /// <summary>
    /// Pause the play screen as well as freezing timescale
    /// </summary>
    public void Pause()
    {
        if (!gameIsPaused && !gameOver) gameIsPaused = true;
    }

    public void Resume()
    {
        if (gameIsPaused) gameIsPaused = false;
    }
    private void Update()
    {
        HandleScoreText();
        HandleGameOver();
        HandlePause();
    }
    
}