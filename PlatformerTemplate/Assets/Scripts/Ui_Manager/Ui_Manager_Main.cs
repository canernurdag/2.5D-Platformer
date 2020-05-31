using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class Ui_Manager_Main : MonoBehaviour
{
    public RectTransform _canvas1;
    
    private void Start()
    {
        DOTween.Init();
        _canvas1.DOAnchorPos(Vector2.zero, 1, false);
    }

    public void PlayButtonFunction() // On Click
    {
        SceneManager.LoadScene(2); // To "Levels Scene"
    }
}
