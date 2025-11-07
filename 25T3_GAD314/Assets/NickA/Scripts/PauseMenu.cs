using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool gameIsPaused = false;

    [SerializeField] private GameObject pauseMenuUI;


    void Start()
    {
        
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (gameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false); // turn UI off
        Time.timeScale = 1.0f;  // time normal
        gameIsPaused = false; // change bool
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true); // turn UI on
        Time.timeScale = 0f; // time pause
        gameIsPaused = true; // change bool
    }

    public void Restart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {      
        Application.Quit();
    }

}
