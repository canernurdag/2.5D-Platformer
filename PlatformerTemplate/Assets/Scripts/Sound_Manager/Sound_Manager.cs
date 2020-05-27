using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//FOR MUSICS
public class Sound_Manager : MonoBehaviour 
{
    public static Sound_Manager _Instance;
    public AudioClip[] _musicArray;
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
