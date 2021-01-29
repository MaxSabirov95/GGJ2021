using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public GameObject settingsMenu;
    public GameObject mainMenu;
    public GameObject creditMenu;
    public void SetMusicVolume (float music)
    {
        audioMixer.SetFloat("Music", music);
    }
    public void SetSFXVolume(float sfx)
    {
        audioMixer.SetFloat("SFX", sfx);
    }

    private void Start()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        creditMenu.SetActive(false);
    }

    public void OpenSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
        creditMenu.SetActive(false);
    }
    public void OpenCredits()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        creditMenu.SetActive(true);
    }
    public void Back()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
        creditMenu.SetActive(false);
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
