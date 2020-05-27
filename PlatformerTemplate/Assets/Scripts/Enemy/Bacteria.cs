using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacteria : Enemy
{

    private float _enemySpeed;
    public float EnemySpeed
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
    public bool CanDie
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
    public bool IsFaceRight
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

    }



    private void FixedUpdate()
    {
        EnemyMove();
    }

    public void EnemyMove()
    {
        base.EnemyMove(_isFaceRight, _myRigidbody, EnemySpeed);
    }




}
