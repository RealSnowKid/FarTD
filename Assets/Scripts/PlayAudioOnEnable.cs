using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayAudioOnEnable : MonoBehaviour
{
    public AudioSource audioData;

    public virtual void Awake()
    {
        audioData = GetComponent<AudioSource>();
    }
    public virtual void OnEnable()
    {
        audioData.Play();
    }

    public virtual void OnDisable()
    {
        audioData.Stop();
    }
}
