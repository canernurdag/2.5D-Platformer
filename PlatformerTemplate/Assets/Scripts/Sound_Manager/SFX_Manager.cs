using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//FOR SFX
public class SFX_Manager : MonoBehaviour
{
    public static SFX_Manager _Instance;
    public AudioClip[] _sfxArray;
    public AudioSource _myAudioSource;

    //SINGLETON PATERIN IS IN SOUND_MANAGER SCRIPT

    private void Start()
    {
        _myAudioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic(AudioClip _tempAudioClip)
    {
        _myAudioSource.clip = _tempAudioClip;
        _myAudioSource.Play();
    }

    public void StopMusic(AudioClip _tempAudioClip)
    {
        _myAudioSource.Stop();
    }
}
