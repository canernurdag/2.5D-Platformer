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
        Game_Events._Instance._onCharacterDieFirst += UpdateHighScoreIfNecessery;
        Game_Events._Instance._onLevelCompletedFirst += UpdateHighScoreIfNecessery;
        Game_Events._Instance._onCharacterDieFirst += ResetUserScoreAfterGameFinish;
    }

    public void AddScore(GameObject _gameObject)
    {
        _userScore += _coinScore;
    }

    public void UpdateHighScoreIfNecessery(GameObject _gameObject)
    {
        if(User_Manager._Instance.LoadUserHighScore() < _userScore)
        {
            User_Manager._Instance._userHighScore = _userScore;
            User_Manager._Instance.SaveUserLocal(_gameObject);
        }

    }

    public void ResetUserScoreAfterGameFinish(GameObject _gameObject)
    {
        _Instance._userScore = 0;
    }

    private void OnDisable()
    {
        Game_Events._Instance._onCoinCollected -= AddScore;
        Game_Events._Instance._onCharacterDieFirst -= UpdateHighScoreIfNecessery;
        Game_Events._Instance._onLevelCompletedFirst -= UpdateHighScoreIfNecessery;
        Game_Events._Instance._onCharacterDieFirst -= ResetUserScoreAfterGameFinish;
    }

}
