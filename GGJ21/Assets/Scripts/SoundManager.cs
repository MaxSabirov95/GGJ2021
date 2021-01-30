using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] Sounds;
    public AudioClip[] UISounds;
    public AudioClip[] Musics;
    public AudioSource BackgroundSFX;
    public AudioSource voicesSFX;
    public AudioSource BackgroundMusic;

    private static AudioSource AudioSrc;//--Main Audio source

    void Start()
    {
        BlackBoard.soundsManager = this;
        BackgroundSFX.Play();
        BackgroundMusic.Play();
        AudioSrc = GetComponent<AudioSource>();
        voicesSFX = GetComponent<AudioSource>();
    }


    public void SoundsList(int SoundNumber)
    {
        AudioSrc.PlayOneShot(Sounds[SoundNumber]);
    }//--Game sounds list
    public void UISoundsList(int SoundNumber)
    {
        AudioSrc.PlayOneShot(UISounds[SoundNumber]);
    }//--Game sounds list

    public void MusicList(int MusicNumber)
    {
        BackgroundMusic.PlayOneShot(Musics[MusicNumber]);
    }//--Game sounds list

    public void TimeOutWhispers(int SoundNumber, float num)
    {
        voicesSFX.PlayOneShot(Sounds[SoundNumber]);
        voicesSFX.volume = (100 - num)/100;
    }//--Game sounds list
}
