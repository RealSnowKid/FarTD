using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSoundEffects : MonoBehaviour
{
    public AudioSource audioSource;
    private AudioSource birdFlap;
    private AudioSource birdAttack;

    private void Awake()
    {
        
        birdFlap = audioSource.GetComponents<AudioSource>()[0];
        birdAttack = audioSource.GetComponents<AudioSource>()[1];
    }
    public void FlapEnable()
    {
        birdFlap.Play();
    }

    public void FlapDisable()
    {
        birdFlap.Stop();
    }

    public void AttackEnable()
    {
        birdAttack.Play();
    }

    public void AttackDisable()
    {
        birdAttack.Stop();
    }
}
