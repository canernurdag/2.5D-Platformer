using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Collision : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint _tempContactPoint = collision.GetContact(0);

        if(collision.gameObject.layer == LayerMask.NameToLayer("EnemyLayer") && _tempContactPoint.normal.y > 0.5f) 
        {
            StartCoroutine(Game_Events._Instance.EnemyDieSequence(collision.gameObject));
        }

        else if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyLayer") && _tempContactPoint.normal.y <= 0.5f)
        {
            StartCoroutine(Game_Events._Instance.CharacterDieSequence(this.gameObject));
        }

        else if (collision.gameObject.layer == LayerMask.NameToLayer("HoleLayer"))
        {
            StartCoroutine(Game_Events._Instance.CharacterDieSequence(this.gameObject));
        }

        else if (collision.gameObject.layer == LayerMask.NameToLayer("PillLayer"))
        {
            StartCoroutine(Game_Events._Instance.CharacterGetPillSequence(this.gameObject));
        }
    }
}
