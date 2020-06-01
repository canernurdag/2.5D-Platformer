using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ui_Manager_Game : MonoBehaviour
{
    public RectTransform _gameOverPanel;

    private void Start()
    {
        DOTween.Init();
        Game_Events._Instance._onCharacterDieFirst += GameOverUiAnimation;
    }
    public void GameOverUiAnimation(GameObject _null)
    {
        _gameOverPanel.DOAnchorPos(Vector2.zero,2);
    }

    private void OnDisable()
    {
        Game_Events._Instance._onCharacterDieFirst -= GameOverUiAnimation;
    }
}
