using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField]
    private AudioSource backgroundMusic;
    [SerializeField]
    private AudioSource FxSound;
    [SerializeField]
    private GameObject SettingUI;

    [SerializeField]
    private AudioMixer audioMixer;

    private void Awake()
    {
        if (instance == null)
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        backgroundMusic = Instantiate(backgroundMusic, this.transform);
        FxSound = Instantiate(FxSound, this.transform);

        StartSound();
    }

    public void volumeReset()
    {
        backgroundMusic.volume = 1f;
        FxSound.volume = 1f;
    }

    public void OnFxSound()
    {
        FxSound.PlayOneShot(FxSound.clip);
    }
    public void StartSound()
    {
        audioMixer.SetFloat("Master", Mathf.Log10(PlayerPrefs.GetFloat("MasterVolume")) * 20);
        audioMixer.SetFloat("BGM", Mathf.Log10(PlayerPrefs.GetFloat("BGMVolume")) * 20);
        audioMixer.SetFloat("FX", Mathf.Log10(PlayerPrefs.GetFloat("FXVolume")) * 20);
    }

}
