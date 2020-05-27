using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corona : Enemy
{
    private float _enemySpeed;
    public  float EnemySpeed
    {
        get
        {
            return _enemySpeed;
        }
        set
        {
            _enemySpeed = value;
        }
    }

    private bool _canDie;
    public  bool CanDie
    {
        get
        {
            return _canDie;
        }
        set
        {
            _canDie = value;
        }
    }

    private bool _isFaceRight;
    public  bool IsFaceRight
    {
        get
        {
            return _isFaceRight;
        }
        set
        {
            _isFaceRight = value;
        }
    }

    private bool _canJump;
    public bool CanJump
    {
        get
        {
            return _canJump;
        }
        set
        {
            _canJump = value;
        }
    }

    private float _enemyJumpSpeed;
    public float EnemyJumpSpeed
    {
        get
        {
            return _enemyJumpSpeed;
        }
        set
        {
            _enemyJumpSpeed = value;
        }
    }


    public Rigidbody _myRigidbody;
    public Character_Manager _myCharacterManager;


    private void Start()
    {
        _myRigidbody = GetComponent<Rigidbody>();
        _myCharacterManager = GameObject.FindObjectOfType<Character_Manager>();

        IsFaceRight = false;

        EnemySpeed = 5;
        EnemyJumpSpeed = 10;

        InvokeRepeating("JumpMovement", 1f, 3f);
    }


    private void JumpMovement()
    {
        _canJump = true;
    }

    private void FixedUpdate()
    {
        EnemyMove();
        EnemyJump();
    }

    public void EnemyMove()
    {
        base.EnemyMove(_isFaceRight, _myRigidbody, EnemySpeed);
    }

    public void EnemyJump()
    {
        base.EnemyJump(_canJump, _myRigidbody, _enemyJumpSpeed);
        _canJump = false;
    }

    public void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }


}

