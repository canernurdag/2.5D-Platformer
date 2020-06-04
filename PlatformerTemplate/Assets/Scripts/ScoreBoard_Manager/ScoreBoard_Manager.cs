using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard_Manager : MonoBehaviour
{
    public Text _scoreText;

    private void Start()
    {
        Game_Events._Instance._onCoinCollected += UpdateScoreBoardText;

        _scoreText.text = Score_Manager._Instance._userScore.ToString();
        
    }

    public void UpdateScoreBoardText(GameObject _gameObject)
    {
        _scoreText.text = Score_Manager._Instance._userScore.ToString();
    }

    private void OnDisable()
    {
        Game_Events._Instance._onCoinCollected -= UpdateScoreBoardText;
    }
}
