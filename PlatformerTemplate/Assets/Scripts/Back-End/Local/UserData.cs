using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class UserData
{
    public List<bool> _userLevelsListData;
    public int _userHighScoreData;


    public UserData()
    {
        _userLevelsListData = User_Manager._Instance._userLevelsList;
        _userHighScoreData = User_Manager._Instance._userHighScore;
    }
}
