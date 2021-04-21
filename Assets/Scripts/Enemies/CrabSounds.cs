using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabSounds : MonoBehaviour
{
    public AudioSource audioSource;
    private AudioSource crabWalk;
    private AudioSource crabAttack;

    private void Awake()
    {

        crabWalk = audioSource.GetComponents<AudioSource>()[0];
        crabAttack = audioSource.GetComponents<AudioSource>()[1];
    }
    public void WalkEnable()
    {
        crabWalk.Play();
    }

    public void WalkDisable()
    {
        crabWalk.Stop();
    }

    public void AttackEnable()
    {
        crabAttack.Play();
    }

    public void AttackDisable()
    {
        crabAttack.Stop();
    }
}
