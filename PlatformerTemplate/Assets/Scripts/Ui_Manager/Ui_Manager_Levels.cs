using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Ui_Manager_Levels : MonoBehaviour
{
    public RectTransform _canvas1;

    public Text _highScoreText;

    private void Start()
    {
        DOTween.Init();
        _canvas1.DOAnchorPos(Vector2.zero, 1, false);

        _highScoreText.text = User_Manager._Instance.LoadUserHighScore().ToString() ;
    }
}
