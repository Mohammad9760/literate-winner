using UnityEngine;
using UnityEngine.Audio; // Make sure to include this namespace
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    // Assign these in the Inspector
    public Slider sfxVolumeSlider;
    public Slider musicVolumeSlider;
    public AudioMixer audioMixer; // Reference to your Audio Mixer

    void Start()
    {
        // Load the saved volume settings
        sfxVolumeSlider.value = Settings.SFXVolume;
        musicVolumeSlider.value = Settings.MusicVolume;

        // Add listeners to the sliders
        sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
    }

    void OnSFXVolumeChanged(float value)
    {
        Settings.SFXVolume = value;
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20); // Convert to decibels
    }

    void OnMusicVolumeChanged(float value)
    {
        Settings.MusicVolume = value;
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20); // Convert to decibels
    }
}

public static class Settings
{
    private const string SFXVolumeKey = "SFXVolume";
    private const string MusicVolumeKey = "MusicVolume";

    public static float SFXVolume
    {
        get
        {
            return PlayerPrefs.GetFloat(SFXVolumeKey, 1.0f); // Default to 1.0f if not set
        }
        set
        {
            PlayerPrefs.SetFloat(SFXVolumeKey, value);
            PlayerPrefs.Save();
        }
    }

    public static float MusicVolume
    {
        get
        {
            return PlayerPrefs.GetFloat(MusicVolumeKey, 1.0f); // Default to 1.0f if not set
        }
        set
        {
            PlayerPrefs.SetFloat(MusicVolumeKey, value);
            PlayerPrefs.Save();
        }
    }
}
