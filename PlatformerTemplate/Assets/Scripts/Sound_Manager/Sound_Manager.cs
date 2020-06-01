using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//FOR MUSICS
public class Sound_Manager : MonoBehaviour 
{
    public static Sound_Manager _Instance;
    public AudioClip[] _musicArray;
    public AudioSource _myAudioSource;
    public AudioClip _tempAudioClip;

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

        PlayMusic(_musicArray[0]); //Menu Music Play
    }

    public void SelectMusicAndPlay(Scene _current, Scene _next)
    {
        if (Scene_Manager._Instance._currentSceneIndex < 3)
        {
            _tempAudioClip = _musicArray[0];
        }
        else
            _tempAudioClip = _musicArray[1];

        PlayMusic(_tempAudioClip);
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
