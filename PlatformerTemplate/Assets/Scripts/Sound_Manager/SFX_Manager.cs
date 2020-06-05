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

        Game_Events._Instance._onCoinCollected += CoinSFX;
        Game_Events._Instance._onEnemyDie += ExplodeSFX;
        Game_Events._Instance._onLevelCompletedFirst += VictorySFX;
        Game_Events._Instance._onObjectBreak += BreakSFX;
        Game_Events._Instance._onCharacterGetPill += PowerUpSFX;
    }

    public void CoinSFX(GameObject _gameObject)
    {
        PlayMusic(_sfxArray[0]);
    }

    public void ExplodeSFX(GameObject _gameObject)
    {
        PlayMusic(_sfxArray[1]);
    }

    public void PowerUpSFX(GameObject _gameObject)
    {
        PlayMusic(_sfxArray[2]);
    }

    public void VictorySFX(GameObject _gameObject)
    {
        PlayMusic(_sfxArray[3]);
    }

    public void BreakSFX(GameObject _gameObject)
    {
        PlayMusic(_sfxArray[4]);
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

    private void OnDisable()
    {
        Game_Events._Instance._onCoinCollected -= CoinSFX;
        Game_Events._Instance._onEnemyDie -= ExplodeSFX;
        Game_Events._Instance._onLevelCompletedFirst -= VictorySFX;
        Game_Events._Instance._onObjectBreak -= BreakSFX;
        Game_Events._Instance._onCharacterGetPill -= PowerUpSFX;
    }
}
