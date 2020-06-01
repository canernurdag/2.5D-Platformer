using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score_Manager : MonoBehaviour
{
    public static Score_Manager _Instance;

    public int _userHighScore;
    public int _userScore;

    public int _coinScore;


    #region SINGLETON Pattern
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
        Game_Events._Instance._onCoinCollected += AddScore;
    }

    public void AddScore(GameObject _gameObject)
    {
        _userScore += _coinScore;
    }

    private void OnDisable()
    {
        Game_Events._Instance._onCoinCollected -= AddScore;
    }

}
