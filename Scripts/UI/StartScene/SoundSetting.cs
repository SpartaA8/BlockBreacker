using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour
{
    public static SoundSetting Instance;

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider FxSlider;
    [SerializeField] private Slider MasterSlider;

    private void Awake()
    {
        if (Instance == null)
        Instance = this;
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey("BGMVolume") || PlayerPrefs.HasKey("MasterVolume") || PlayerPrefs.HasKey("FXVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetFxVolume();
            SetMasterVolume();
        }
    }

    public void SetMasterVolume()
    {
        float volume = MasterSlider.value;
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("BGM", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    public void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("BGMVolume");
        MasterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        FxSlider.value = PlayerPrefs.GetFloat("FXVolume");
        SetMasterVolume();
        SetMusicVolume();
        SetFxVolume();
    }

    public void SetFxVolume()
    {
        float volume = FxSlider.value;
        audioMixer.SetFloat("FX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("FXVolume", volume);
    }

    
}
