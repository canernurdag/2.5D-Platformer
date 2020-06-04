using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Ui_Manager_Game : MonoBehaviour
{
    public RectTransform _gameOverPanel;
    public Text _gameOverText;

    public Text _highScoreText;

    private void Start()
    {
        DOTween.Init();
        Game_Events._Instance._onCharacterDieFirst += GameOverUiAnimation;
        Game_Events._Instance._onCharacterDieFirst += GameOverUiHighScoreShow;
        Game_Events._Instance._onGameFinished += GameFinishUiSequence;

    }
    public void GameOverUiAnimation(GameObject _null)
    {
        _gameOverPanel.DOAnchorPos(Vector2.zero,2);
    }

    public void GameOverUiHighScoreShow(GameObject _gameObject)
    {
        _highScoreText.text = User_Manager._Instance.LoadUserHighScore().ToString();
    }

    public void GameFinishUiSequence(GameObject _gameObject)
    {
        _gameOverText.text = "Game Finished";
        _gameOverPanel.DOAnchorPos(Vector2.zero, 2);
    }

    private void OnDisable()
    {
        Game_Events._Instance._onCharacterDieFirst -= GameOverUiAnimation;
        Game_Events._Instance._onCharacterDieFirst -= GameOverUiHighScoreShow;
        Game_Events._Instance._onGameFinished -= GameFinishUiSequence;
    }
}
