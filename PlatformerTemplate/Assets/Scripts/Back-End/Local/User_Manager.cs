using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class User_Manager : MonoBehaviour
{
    //Static Instance
    public static User_Manager _Instance;

    #region VARIABLES

    public List<bool> _userLevelsList;
    public List<bool> _tempLevelList; //For internal calculate
    public int _currentLevel;
    public int _userHighScore;


    GameObject _null;
    #endregion


    private void Awake()
    {
        SinglePatern();

        UserData _myUserData = UserSave.LoadUser();
        CreateANewUserIfNecessary(_myUserData);
    }
    
    private void SinglePatern()
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
    private void CreateANewUserIfNecessary(UserData _myUserData)
    {
        if (_myUserData == null)
        {
            //Give Defult Setting Here

            _userLevelsList = new List<bool>
            {
                true,
                false,
                false

            };

            SaveUserLocal(_null);
        }
    }

    public void Start()
    {
        _userLevelsList = LoadUserLevelListLocal();
        _tempLevelList = _userLevelsList;

        _userHighScore = LoadUserHighScore();

        //Due to script execution order, below functions are in Start Method instead of OnEnable
        Game_Events._Instance._onLevelCompletedFirst += RefreshLevelArrayWithSucceedCurrentLevel;
        Game_Events._Instance._onLevelCompletedFirst += SaveUserLocal;

    }

    public void SaveUserLocal(GameObject _null) //_null for Event type to satisfy
    {
        UserSave.SaveUser(_Instance);
    }

    public void RefreshLevelArrayWithSucceedCurrentLevel(GameObject _null)
    {
        _userLevelsList = new List<bool>();
        for (int i=0;i<_tempLevelList.Count; i++)
        {
            _userLevelsList.Add(_tempLevelList[i]);
        }
        if(_currentLevel < _tempLevelList.Count-1) // Check the level is not the last level.
        { 
            _userLevelsList[_currentLevel+1] = true;
        }
        else if(_currentLevel == _tempLevelList.Count -1) // If last level
        {
            // Game Finished Actions are here.
            Debug.Log("Game Completed Successfully");
        }
        _tempLevelList = _userLevelsList;
    }



    public List<bool> LoadUserLevelListLocal()
    {
        UserData _myUserData = UserSave.LoadUser();
        _Instance._userLevelsList = _myUserData._userLevelsListData;

        return _Instance._userLevelsList;
    }

    public int LoadUserHighScore()
    {
        UserData _myUserData = UserSave.LoadUser();
        _Instance._userHighScore = _myUserData._userHighScoreData;

        return _Instance._userHighScore;
    }

    private void OnDisable()
    {
        Game_Events._Instance._onLevelCompletedFirst -= RefreshLevelArrayWithSucceedCurrentLevel;
        Game_Events._Instance._onLevelCompletedFirst -= SaveUserLocal;
    }

}
