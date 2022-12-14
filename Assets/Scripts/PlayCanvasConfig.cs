using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayCanvasConfig : MonoBehaviour
{
    public static bool gameIsPaused;
    public static bool gameOver = false; // Use true for debug purposes
    public static float money = 0f;
    [Header("References")]
    public GameObject pauseScreenUI;
    public GameObject gameOverUI;
    public TextMeshPro scoreText;
    public Button upgradeButton;

    private void HandlePause()
    {
        Time.timeScale = (gameIsPaused || gameOver) ? 0f : 1f;
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

    private void UpgradeButtonInteraction()
    {
        upgradeButton.interactable = !BallBehavior.locked;
    }

    /// <summary>
    /// Resume game on a delay (coroutine)
    /// </summary>
    /// <returns>null</returns>
    public IEnumerator ResumeGame()
    {
        if (gameIsPaused) gameIsPaused = false;
        Debug.Log("Resuming...");
        yield return new WaitForSeconds(1f);
    }
    public void Resume()
    {
        StartCoroutine(ResumeGame());
    }
    private void Update()
    {
        HandleScoreText();
        HandleGameOver();
        HandlePause();
        if (!gameOver)
            UpgradeButtonInteraction();
    }
    
}