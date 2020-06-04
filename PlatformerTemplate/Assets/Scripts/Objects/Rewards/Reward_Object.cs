using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward_Object : MonoBehaviour
{
    public RewardMaker_Object _rewardMaker;
    public Rigidbody _myRigidbody;

    public float _rewardObjectVelocity;
    public bool _IsFaceRight;
  

    public virtual void Start()
    {


        Invoke("Init", 0.1f);

        _IsFaceRight = true;
        _rewardObjectVelocity = 8f;

    }

    public virtual void Init()
    {
        _rewardMaker = transform.parent.gameObject.GetComponent<RewardMaker_Object>();
        _myRigidbody = GetComponent<Rigidbody>();
    }
 
    public virtual void FixedUpdate()
    {
        if(_rewardMaker != null)
        { 
            if (_rewardMaker._IsInstantiatedObjectReadyToMove) 
            {
                if(_IsFaceRight)
                {
                    _myRigidbody.velocity = new Vector3(_rewardObjectVelocity, _myRigidbody.velocity.y, _myRigidbody.velocity.z);
                }
                else if(!_IsFaceRight)
                {
                    _myRigidbody.velocity = new Vector3(-_rewardObjectVelocity, _myRigidbody.velocity.y, _myRigidbody.velocity.z);
                }
            
            }
        }
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        if(_IsFaceRight)
        {
            _IsFaceRight = false;
        }

        else if(!_IsFaceRight)
        {
            _IsFaceRight = true;
        }
    }

}
