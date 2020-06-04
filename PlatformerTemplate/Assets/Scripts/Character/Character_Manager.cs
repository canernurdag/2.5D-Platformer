using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum Character_State
{
    normal,
    pilled
}


public class Character_Manager : MonoBehaviour
{
    public static Character_Manager _Instance;

    public Character_State _currentCharacterState;

    public Vector3 _normalCharacterSize;
    public Vector3 _pilledCharacterSize;

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
        DOTween.Init();
        _currentCharacterState = Character_State.normal;

        _normalCharacterSize = new Vector3(1, 1, 1);
        _pilledCharacterSize = new Vector3(1.4f, 1.4f, 1.4f);

        Game_Events._Instance._onCharacterDieSecond += CharacterDeath;
        Game_Events._Instance._onLevelCompletedFirst += CharacterTurn90Degrees;
        Game_Events._Instance._onCharacterGetPill += CharacterGetPilled;
        Game_Events._Instance._onEnemyHitToPilledCharacter += CharacterGetUnpilled;
    }

    public void CharacterTurn90Degrees(GameObject _this)
    {
        FindObjectOfType<Character_Movement>().transform.DORotate(new Vector3(0, 180, 0), 0.75f);
    }

    public void CharacterDeath(GameObject _this)
    {
        Destroy(FindObjectOfType<Character_Movement>());
    }

    public void CharacterGetPilled(GameObject _this)
    {
        _currentCharacterState = Character_State.pilled;
        FindObjectOfType<Character_Movement>().transform.localScale = _pilledCharacterSize;
        
    }

    public void CharacterGetUnpilled(GameObject _this)
    {
        _currentCharacterState = Character_State.normal;
        FindObjectOfType<Character_Movement>().transform.localScale = _normalCharacterSize;
    }
   

    public void OnDisable()
    {
        Game_Events._Instance._onCharacterDieSecond -= CharacterDeath;
        Game_Events._Instance._onLevelCompletedFirst -= CharacterTurn90Degrees;
        Game_Events._Instance._onCharacterGetPill -= CharacterGetPilled;
        Game_Events._Instance._onEnemyHitToPilledCharacter -= CharacterGetUnpilled;
    }

}
