using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private List<AudioClip> ClipList;
    private AudioSource audioSource;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {   
            Instance = this;            
            audioSource = GetComponent<AudioSource>();
        }        
    }

    public void PlayClip(string clipName)
    {
        audioSource.clip = ClipList.Find(x => x.name == clipName);
        audioSource.PlayOneShot(audioSource.clip);
    }
}
