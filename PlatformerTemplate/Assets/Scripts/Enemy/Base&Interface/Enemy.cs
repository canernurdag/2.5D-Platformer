using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    protected float _EnemySpeed { get; set; }
    protected bool _IsFaceRight { get; set; }
    protected bool _CanJump { get; set; }
    protected float _EnemyJumpSpeed { get; set; }
    protected bool _CanDie { get; set; }

    protected Rigidbody _myRigidbody;
    protected Character_Manager _myCharacterManager;

    public virtual void Start()
    {
        _myRigidbody = GetComponent<Rigidbody>();
        _myCharacterManager = GameObject.FindObjectOfType<Character_Manager>();

        _IsFaceRight = false;

        _EnemySpeed = 5;
        _EnemyJumpSpeed = 20;

        InvokeRepeating("JumpMovement", 1f, 3f);

    }

    public virtual void JumpMovement()
    {
        _CanJump = true;
    }

    public virtual void FixedUpdate()
    {
        EnemyMove();
        EnemyJump();
    }

    public virtual void EnemyMove()
    {
        if (!_IsFaceRight)
        {
            _myRigidbody.velocity = new Vector3(-_EnemySpeed, _myRigidbody.velocity.y, _myRigidbody.velocity.z);
        }

        else if (_IsFaceRight)
        {
            _myRigidbody.velocity = new Vector3(_EnemySpeed, _myRigidbody.velocity.y, _myRigidbody.velocity.z);
        }
    }
    public virtual void EnemyJump()
    {
        if (_CanJump)
        {
            _myRigidbody.AddForce(new Vector3(0, _EnemyJumpSpeed, 0), ForceMode.Impulse);
        }
        _CanJump = false;
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("HoleLayer"))
        {
            Destroy(this.gameObject);
        }

       else if (collision.gameObject.layer != LayerMask.NameToLayer("GroundLayer") && collision.gameObject.layer != LayerMask.NameToLayer("HoleLayer"))
        {
            if (_IsFaceRight)
            {
                _IsFaceRight = false;
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (!_IsFaceRight)
            {
                _IsFaceRight = true;
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }

    }

    public virtual void EnemyDeath(GameObject _this)
    {
        _this = this.gameObject;
        Destroy(_this);
    }

}
