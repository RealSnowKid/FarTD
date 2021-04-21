using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string Event;
    private WavesSpawn wavesSpawn;
    private static FMOD.Studio.EventInstance music;

    private void Start()
    {
        music = FMODUnity.RuntimeManager.CreateInstance(Event);
        music.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject, gameObject.GetComponent<Rigidbody>()));
        RuntimeManager.AttachInstanceToGameObject(music, transform, gameObject.GetComponent<Rigidbody>());
        music.start();
        float volume;
        music.getVolume(out volume);
        Debug.Log(volume);
        music.release();
        wavesSpawn = GameObject.Find("WavesSystem").GetComponent<WavesSpawn>();
    }

    private void Update()
    {
        if (wavesSpawn.IsWave())
        {
            Intensity(1);
        }
        else if (!wavesSpawn.IsWave())
        {
            Intensity(0);
        }
    }

    private void Intensity(float intensity)
    {
        music.setParameterByName("Intensity", intensity);
    }

    private void OnDisable()
    {
        music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
