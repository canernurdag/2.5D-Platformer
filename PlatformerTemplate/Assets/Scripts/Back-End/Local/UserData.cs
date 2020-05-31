using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class UserData
{
    public int _userHighScoreLocalData;
    public List<bool> _userLevelsListData;


    public UserData()
    {
        _userHighScoreLocalData = User_Manager._Instance._userHighScoreLocal;
        _userLevelsListData = User_Manager._Instance._userLevelsList;

    }
}
