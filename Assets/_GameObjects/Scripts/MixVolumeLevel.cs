using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixVolumeLevel : MonoBehaviour
{
    public AudioMixer mastermixer;




    public void SetFXVolume(float fxVolume)
    {
        mastermixer.SetFloat("FX", fxVolume);
    }
    public void SetMusicVolume(float musicVolume)
    {
        mastermixer.SetFloat("Volumen", musicVolume);
    }
    
}
