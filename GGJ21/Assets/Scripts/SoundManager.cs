using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] Sounds;
    public AudioSource BackgroundMusic;
    public AudioSource voicesMusic;

    private static AudioSource AudioSrc;//--Main Audio source

    void Start()
    {
        BlackBoard.soundsManager = this;
        BackgroundMusic.Play();
        AudioSrc = GetComponent<AudioSource>();
        voicesMusic = GetComponent<AudioSource>();
    }


    public void SoundsList(int SoundNumber)
    {
        AudioSrc.PlayOneShot(Sounds[SoundNumber]);
    }//--Game sounds list


    public void TimeOutWhispers(int SoundNumber, float num)
    {
        voicesMusic.PlayOneShot(Sounds[SoundNumber]);
        voicesMusic.volume = (100 - num)/100;
    }//--Game sounds list
}
