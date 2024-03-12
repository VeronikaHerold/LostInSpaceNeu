using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider _musicSlider, _sfxSlider;

    private void Start()
    {
        // Überprüfe, ob die PlayerPrefs vorhanden sind
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            float musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            _musicSlider.value = musicVolume;
            AudioManagerNew.Instance.MusicVolume(musicVolume);
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            float sfxVolume = PlayerPrefs.GetFloat("SFXVolume");
            _sfxSlider.value = sfxVolume;
            AudioManagerNew.Instance.SFXVolume(sfxVolume);
        }
    }

    public void ToggleMusic()
    {
        AudioManagerNew.Instance.ToggleMusic();
    }

    public void ToggleSFX()
    {
        AudioManagerNew.Instance.ToggleSFX();
    }

    public void MusicVolume()
    {
        float volume = _musicSlider.value;
        PlayerPrefs.SetFloat("MusicVolume", volume);
        AudioManagerNew.Instance.MusicVolume(volume);
    }

    public void SFXVolume()
    {
        float volume = _sfxSlider.value;
        PlayerPrefs.SetFloat("SFXVolume", volume);
        AudioManagerNew.Instance.SFXVolume(volume);
    }
}