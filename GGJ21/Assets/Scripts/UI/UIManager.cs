using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenu;
    public AudioMixer audioMixer;
    public GameObject settingsMenu;

    private void Start()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPaused)
            {
                Resume();
            }
            else
            {
               Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void LoadMenu()
    {
        Debug.Log("Loading menu...");
        SceneManager.LoadScene("Main Menu");
    }

    public void SettingsScreen()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void Back()
    {
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
