using UnityEngine;
using System.Collections;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject playButton;
    public GameObject pauseButton;
    private bool gameIsPaused = false;

    public void PauseGame()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
        playButton.SetActive(true);
        pauseButton.SetActive(false);
        gameIsPaused = true;
    }

    public void ResumeGame()
    { 
        Time.timeScale = 1;
        gameObject.SetActive(false);
        playButton.SetActive(false);
        pauseButton.SetActive(true);
        gameIsPaused = false;
    }

    public bool IsGamePaused()
    {
        return gameIsPaused;
    }
}
