using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Spawner : MonoBehaviour
{
    public GameObject _characterPrefab;
    public Transform _spawnPoint;

    public GameObject _levelCharacter;


    private void Start()
    {
        _levelCharacter = Instantiate(_characterPrefab, _spawnPoint.position, Quaternion.Euler(0, 90, 0));
        _levelCharacter.transform.SetParent(transform);

 

        Invoke("CharacterSizeChange", 0.05f); //Due to script execution order
    }

    

    public void CharacterSizeChange()
    {
  

        if (Character_Manager._Instance._currentCharacterState == Character_State.pilled)
        {
            _levelCharacter.transform.localScale = Character_Manager._Instance._pilledCharacterSize;
        }
        else if (Character_Manager._Instance._currentCharacterState == Character_State.normal)
        {
            return;
        }
    }
}
