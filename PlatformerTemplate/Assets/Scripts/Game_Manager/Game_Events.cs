using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Game_Events : MonoBehaviour
{
    public static Game_Events _Instance;

    public delegate void CharacterActions(GameObject _char);
    public event CharacterActions _onCharacterDie;
    public event CharacterActions _onCharacterGetPill;

    public delegate void EnemyDie(GameObject _enemy);
    public event EnemyDie _onEnemyDie;

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

    public IEnumerator CharacterDieSequence(GameObject _char)
    {
        if(_onCharacterDie != null)
        {
            _onCharacterDie(_char);
        }
        yield return new WaitForSeconds(1f);
        Destroy(_char);
 
    }

    public IEnumerator EnemyDieSequence(GameObject _enemy)
    {
        if(_onEnemyDie != null)
        {
            _onEnemyDie(_enemy);
        }
        yield return new WaitForSeconds(1f);
        Destroy(_enemy);
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

}
