using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Collision : MonoBehaviour
{
    public delegate IEnumerator EnemyDeathSequence();
    public event EnemyDeathSequence OnHitToEnemy;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("EnemyLayer"))
        {
            
        }
    }
}
