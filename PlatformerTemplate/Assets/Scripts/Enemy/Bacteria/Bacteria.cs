using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacteria : Enemy, IEnemy
{
    public float _EnemySpeed { get; set; }
    public bool _IsFaceRight { get; set; }
    public bool _CanJump { get; set; }
    public float _EnemyJumpSpeed { get; set; }
    public bool _CanDie { get; set; }

    public Rigidbody _myRigidbody;
    public Character_Manager _myCharacterManager;


    private void Start()
    {
        _myRigidbody = GetComponent<Rigidbody>();
        _myCharacterManager = GameObject.FindObjectOfType<Character_Manager>();

        _IsFaceRight = false;

        _EnemySpeed = 5;
        _EnemyJumpSpeed = 0; //No jump

        InvokeRepeating("JumpMovement", 1f, 3f);
    }


    private void JumpMovement()
    {
        _CanJump = false; //No jump
    }

    private void FixedUpdate()
    {
        EnemyMove();
        EnemyJump();
    }

    public void EnemyMove()
    {
        base.EnemyMove(_IsFaceRight, _myRigidbody, _EnemySpeed);
    }

    public void EnemyJump()
    {
        base.EnemyJump(_CanJump, _myRigidbody, _EnemyJumpSpeed);
        _CanJump = false;
    }

    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (collision.gameObject.layer != LayerMask.NameToLayer("GroundLayer") && collision.gameObject.layer != LayerMask.NameToLayer("HoleLayer"))
        {
            if (_IsFaceRight)
            {
                _IsFaceRight = false;
            }
            else if (!_IsFaceRight)
            {
                _IsFaceRight = true;
            }
        }
    }

    public override void EnemyDeath(GameObject _this)
    {
        base.EnemyDeath(_this);
    }


}
