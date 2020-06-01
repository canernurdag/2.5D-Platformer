using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Game_Events : MonoBehaviour
{
    //Static Instance (Data Structures)
    public static Game_Events _Instance;
    
    //All Events For Functions To Subscribe
    public event Action<GameObject> _onCharacterDieFirst;
    public event Action<GameObject> _onCharacterDieSecond;
    public event Action<GameObject> _onCharacterGetPill;
    public event Action<GameObject> _onEnemyDie;
    public event Action<GameObject> _onLevelCompletedFirst;
    public event Action<GameObject> _onLevelCompletedSecond;
    public event Action<GameObject> _onCoinCollected;

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


    public IEnumerator CharacterDieSequence(GameObject _char)
    {
        if(_onCharacterDieFirst != null)
        {
            _onCharacterDieFirst(_char);
        }
        yield return new WaitForSeconds(2f);
        if(_onCharacterDieSecond != null)
        {
            _onCharacterDieSecond(_char);
        }
 
    }

    public IEnumerator EnemyDieSequence(GameObject _enemy)
    {
        if(_onEnemyDie != null)
        {
            _onEnemyDie(_enemy);
        }
        yield return new WaitForSeconds(0.1f);
        if(_enemy != null) // To avoid to call a function of the destroyed GameObject
        { 
            _enemy.GetComponent<Enemy>().EnemyDeath(_enemy); //To be able to select all inherited classes
        }
    }

    public IEnumerator CharacterGetPillSequence(GameObject _char)
    {
        if(_onCharacterGetPill != null)
        {
            _onCharacterGetPill(_char);
        }
        
        yield return new WaitForSeconds(1f);
        //FUNCTION
    }

    public IEnumerator LevelCompletedSequence(GameObject _char)
    {
        if(_onLevelCompletedFirst != null)
        {
            _onLevelCompletedFirst(_char);
        }
        yield return new WaitForSeconds(4);
        
        if(_onLevelCompletedSecond != null)
        {
            _onLevelCompletedSecond(_char);
        }
    }

    public void CoinCollectSequnce(GameObject _gameObject)
    {
        if(_onCoinCollected != null)
        {
            _onCoinCollected(_gameObject);
        }
    }



}
