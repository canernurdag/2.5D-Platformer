using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class UserData
{
    public List<bool> _userLevelsListData;


    public UserData()
    {
        _userLevelsListData = User_Manager._Instance._userLevelsList;
    }
}
