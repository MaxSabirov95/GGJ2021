using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenu;
    public AudioMixer audioMixer;
    public GameObject settingsMenu;
    public Slider musicSlider;
    public Slider sfxSlider;

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
        StartCoroutine(PlayPauseMenuSounds(1));
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause()
    {
        StartCoroutine(PlayPauseMenuSounds(2));
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void LoadMenu()
    {
        StartCoroutine(PlayPauseMenuSounds(1));
        SceneManager.LoadScene("Main Menu");
    }

    public void SettingsScreen()
    {
        StartCoroutine(PlayPauseMenuSounds(1));
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void Back()
    {
        StartCoroutine(PlayPauseMenuSounds(0));
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void SetMusicVolume(float music)
    {
        audioMixer.SetFloat("Music", music);
        MainMenu.musicValue = music;
    }
    public void SetSFXVolume(float sfx)
    {
        audioMixer.SetFloat("SFX", sfx);
        MainMenu.sfxValue = sfx;
    }

    public void QuitGame()
    {
        StartCoroutine(PlayPauseMenuSounds(1));
        Debug.Log("Quitting game...");
        Application.Quit();
    }

<<<<<<< HEAD
    private void OnEnable()
    {
        musicSlider.value = MainMenu.musicValue;
        sfxSlider.value = MainMenu.sfxValue;
    }

    IEnumerator PlayPauseMenuSounds (int num)
    {
        BlackBoard.soundsManager.UISoundsList(num);
        yield return new WaitForSeconds(0.05f);
    }
=======
    //private void OnEnable()
    //{
    //    musicSlider.value = MainMenu.musicValue;
    //    sfxSlider.value = MainMenu.sfxValue;
    //}
>>>>>>> parent of 21ac223... Revert "Level 2"
}
