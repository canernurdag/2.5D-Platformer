using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level_Manager : MonoBehaviour
{
    public GameObject[] _levelButtonsArray; // levels should be stored as 1,2,3,4... etc
    public List<bool> _tempLevelList;

    private void Start()
    {
        _tempLevelList = User_Manager._Instance._userLevelsList; // Temp bool array from local data
        ImplementAllLevelIcons();
    }

    private void ImplementAllLevelIcons()
    {
        for (int i = 0; i < _levelButtonsArray.Length; i++)
        {
            if(_tempLevelList[i]) //If true, means level is unlocked
            { 
                _levelButtonsArray[i].GetComponent<Button>().interactable = true;
            }

            else if(!_tempLevelList[i]) //If false, means level is locked
            {
                _levelButtonsArray[i].GetComponent<Button>().interactable = false;
            }
        }
    }

    //Button Functions

    public void PlayTheSelectedLevelFunction(int _LevelIndexInBuild) // On Click
    {
        User_Manager._Instance._currentLevel = _LevelIndexInBuild-2; //Substract the first 3 scene by "-2";
        SceneManager.LoadScene(_LevelIndexInBuild);
    }

}
