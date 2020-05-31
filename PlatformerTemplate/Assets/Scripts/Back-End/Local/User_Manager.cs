using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class User_Manager : MonoBehaviour
{
    //Static Instance
    public static User_Manager _Instance;

    #region VARIABLES
    public int _userHighScoreLocal;

    public List<bool> _userLevelsList;
    public List<bool> _tempLevelList; //For internal calculate
    public int _currentLevel;

    GameObject _null;
    #endregion

    #region FUNCTIONS
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
        _userLevelsList = LoadUserLevelLocal();
        _tempLevelList = _userLevelsList;

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
        _userLevelsList[_currentLevel] = true;
        _tempLevelList = _userLevelsList;
    }

    public int LoadUserHighScoreLocal()
    {
        UserData _myUserData = UserSave.LoadUser();
        _Instance._userHighScoreLocal = _myUserData._userHighScoreLocalData;

        return _Instance._userHighScoreLocal;
    }

    public List<bool> LoadUserLevelLocal()
    {
        UserData _myUserData = UserSave.LoadUser();
        _Instance._userLevelsList = _myUserData._userLevelsListData;

        return _Instance._userLevelsList;
    }

    #endregion
}
