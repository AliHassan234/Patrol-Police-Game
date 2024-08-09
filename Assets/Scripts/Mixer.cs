using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class Mixer : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Sprite soundOnImage;
    public Sprite soundOffImage;
    public Sprite musicOnImage;
    public Sprite musicOffImage;
    public Image soundButtonImage;
    public Image musicButtonImage;

    private bool isSFXOn = true;
    private bool isMusicOn = true;

    private const string SFX_KEY = "SFXOn";
    private const string MUSIC_KEY = "MusicOn";

    private void Start()
    {
        // Load saved settings
        isSFXOn = PlayerPrefs.GetInt(SFX_KEY, 1) == 1;
        isMusicOn = PlayerPrefs.GetInt(MUSIC_KEY, 1) == 1;

        // Apply loaded settings
        UpdateSFXUI();
        UpdateMusicUI();
    }

    // Adjust the volume of sound effects
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
    }

    // Adjust the volume of background music
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }

    // Toggle sound effects on/off
    public void ToggleSFX()
    {
        isSFXOn = !isSFXOn;
        PlayerPrefs.SetInt(SFX_KEY, isSFXOn ? 1 : 0); // Save setting
        UpdateSFXUI();
    }

    // Toggle background music on/off
    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        PlayerPrefs.SetInt(MUSIC_KEY, isMusicOn ? 1 : 0); // Save setting
        UpdateMusicUI();
    }

    private void UpdateSFXUI()
    {
        if (isSFXOn)
        {
            audioMixer.SetFloat("SFXVolume", 0f);
            if (soundButtonImage != null)
                soundButtonImage.sprite = soundOnImage;
        }
        else
        {
            audioMixer.SetFloat("SFXVolume", -80f); // Mute
            if(soundButtonImage!=null)
                soundButtonImage.sprite = soundOffImage;
        }
    }

    private void UpdateMusicUI()
    {
        if (isMusicOn)
        {
            audioMixer.SetFloat("MusicVolume", 0f);
            if (musicButtonImage != null)
                musicButtonImage.sprite = musicOnImage;
        }
        else
        {
            audioMixer.SetFloat("MusicVolume", -80f); // Mute
            if (musicButtonImage != null)
                musicButtonImage.sprite = musicOffImage;
        }
    }
}
