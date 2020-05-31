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

    public Character_State _currentCharacterState;

    private void Start()
    {
        DOTween.Init();
        _currentCharacterState = Character_State.normal;

        Game_Events._Instance._onCharacterDieSecond += CharacterDeath;
        Game_Events._Instance._onLevelCompletedFirst += CharacterTurn90Degrees;
    }

    public void CharacterTurn90Degrees(GameObject _this)
    {
        _this.transform.DORotate(new Vector3(0, 180, 0), 0.75f);
    }

    public void CharacterDeath(GameObject _this)
    {
        _this = this.gameObject;
        Destroy(_this);
    }
   

    public void OnDisable()
    {
        Game_Events._Instance._onCharacterDieSecond -= CharacterDeath;
        Game_Events._Instance._onLevelCompletedFirst -= CharacterTurn90Degrees;
    }

}
