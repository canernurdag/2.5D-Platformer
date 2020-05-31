using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    float _EnemySpeed { get; set; }
    bool _IsFaceRight { get; set; }
    bool _CanJump { get; set; }
    float _EnemyJumpSpeed { get; set; }
    bool _CanDie { get; set; }


}
