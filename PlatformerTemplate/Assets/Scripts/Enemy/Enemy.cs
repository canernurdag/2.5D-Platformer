using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public virtual void EnemyMove(bool _IsFaceRight, Rigidbody _MyRigidbody, float _EnemySpeed)
    {
        if (!_IsFaceRight)
        {
            _MyRigidbody.velocity = new Vector3(-_EnemySpeed, _MyRigidbody.velocity.y, _MyRigidbody.velocity.z);
        }

        else if (_IsFaceRight)
        {
            _MyRigidbody.velocity = new Vector3(_EnemySpeed, _MyRigidbody.velocity.y, _MyRigidbody.velocity.z);
        }
    }
    public virtual void EnemyJump(bool _CanJump, Rigidbody _MyRigidBody, float _EnemyJumpSpeed)
    {
        if (_CanJump)
        {
            _MyRigidBody.AddForce(new Vector3(0, _EnemyJumpSpeed, 0), ForceMode.Impulse);
        }
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("HoleLayer"))
        {
            Destroy(this.gameObject);
        }
 
    }

    public IEnumerator DeathSequence()
    {
        yield return null;
        Destroy(this.gameObject, 1f);
    }

}
