using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundWhenPlaced : MonoBehaviour
{

    public AudioClip soundClip;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = soundClip;
        PlaySound();
    }

    void PlaySound()
    {
        if (audioSource.clip != null)
        {
            audioSource.Play();
        }
    }


}
