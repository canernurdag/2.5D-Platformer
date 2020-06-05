using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Ui_Manager_Game : MonoBehaviour
{
    public RectTransform _gameOverPanel;
    public RectTransform _gameFinishedPanel;


    public Text _highScoreText;
    public Text _highScoreText2;

    private void Start()
    {
        DOTween.Init();
        Game_Events._Instance._onCharacterDieFirst += GameOverUiAnimation;
        Game_Events._Instance._onCharacterDieFirst += GameOverUiHighScoreShow;
        Game_Events._Instance._onGameFinishedFirst += GameFinishUiSequence;
        Game_Events._Instance._onGameFinishedFirst += GameOverUiHighScoreShow2;

    }
    public void GameOverUiAnimation(GameObject _null)
    {
        _gameOverPanel.DOAnchorPos(Vector2.zero,2);
    }

    public void GameOverUiHighScoreShow(GameObject _gameObject)
    {
        _highScoreText.text = User_Manager._Instance.LoadUserHighScore().ToString();
    }

    public void GameOverUiHighScoreShow2(GameObject _gameObject)
    {
        _highScoreText2.text = User_Manager._Instance.LoadUserHighScore().ToString();
    }

    public void GameFinishUiSequence(GameObject _null)
    {
        _gameFinishedPanel.DOAnchorPos(Vector2.zero, 2);
    }

    private void OnDisable()
    {
        Game_Events._Instance._onCharacterDieFirst -= GameOverUiAnimation;
        Game_Events._Instance._onCharacterDieFirst -= GameOverUiHighScoreShow;
        Game_Events._Instance._onGameFinishedFirst -= GameFinishUiSequence;
        Game_Events._Instance._onGameFinishedFirst -= GameOverUiHighScoreShow2;
    }
}
