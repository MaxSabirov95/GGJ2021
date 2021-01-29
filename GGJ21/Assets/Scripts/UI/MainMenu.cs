using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public GameObject settingsMenu;
    public GameObject mainMenu;
    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void OpenSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }
    public void Back()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void LoadLevel()
    {
        SceneManager.LoadScene("Main Level Scene");
        Time.timeScale = 1f;        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
