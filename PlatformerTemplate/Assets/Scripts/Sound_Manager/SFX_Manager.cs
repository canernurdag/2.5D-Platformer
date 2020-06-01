using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//FOR SFX
public class SFX_Manager : MonoBehaviour
{
    public static SFX_Manager _Instance;
    public AudioClip[] _sfxArray;
    public AudioSource _myAudioSource;

    #region SINGLETON
    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

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
