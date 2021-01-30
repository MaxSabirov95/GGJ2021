using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public GameObject settingsMenu;
    public GameObject mainMenu;
    public GameObject creditMenu;
    public static float musicValue;
    public static float sfxValue;
    public Slider musicSlider;
    public Slider sfxSlider;

    public void SetMusicVolume (float music)
    {
        audioMixer.SetFloat("Music", music);
        musicValue = music;
    }
    public void SetSFXVolume(float sfx)
    {
        audioMixer.SetFloat("SFX", sfx);
        sfxValue = sfx;
    }

    private void Start()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        creditMenu.SetActive(false);
        musicSlider.value = 0f;
        sfxSlider.value = 0f;
    }

    public void OpenSettings()
    {
        StartCoroutine(PlayUISound(2));
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
        creditMenu.SetActive(false);
    }
    public void OpenCredits()
    {
        StartCoroutine(PlayUISound(2));
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        creditMenu.SetActive(true);
    }
    public void Back()
    {
        StartCoroutine(PlayUISound(1));
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
        creditMenu.SetActive(false);
    }
    public void LoadLevel()
    {
        StartCoroutine(PlayStartSound(0)); 
    }

    public void QuitGame()
    {
        StartCoroutine(PlayUISound(2));
        Application.Quit();
    }

    private void OnEnable()
    {
        SetMusicVolume(musicValue);
        musicSlider.value = musicValue;
        SetSFXVolume(sfxValue);
        sfxSlider.value = sfxValue;
    }

    IEnumerator PlayUISound (int num)
    {
        BlackBoard.soundsManager.UISoundsList(num);
        yield return new WaitForSeconds(0.05f);
    }

    IEnumerator PlayStartSound (int num)
    {
        BlackBoard.soundsManager.UISoundsList(num);
        yield return new WaitForSeconds(1.6f);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Level Scene");
    }
}
