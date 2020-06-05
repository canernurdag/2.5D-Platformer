using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Collision : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint _tempContactPoint = collision.GetContact(0);

        if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyLayer") && _tempContactPoint.normal.y > 0.5f)
        {
            StartCoroutine(Game_Events._Instance.EnemyDieSequence(collision.gameObject));
        }

        else if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyLayer") && _tempContactPoint.normal.y <= 0.5f)
        {
            if (Character_Manager._Instance._currentCharacterState == Character_State.normal)
            {
                StartCoroutine(Game_Events._Instance.CharacterDieSequence(this.gameObject));
                GetComponent<Character_Movement>()._IsDead = true;
            }
            else if (Character_Manager._Instance._currentCharacterState == Character_State.pilled)
            {
                Game_Events._Instance.EnemyHitToPilledCharacter(this.gameObject);
            }
        }

        else if (collision.gameObject.layer == LayerMask.NameToLayer("HoleLayer"))
        {
            StartCoroutine(Game_Events._Instance.CharacterDieSequence(this.gameObject));
            GetComponent<Character_Movement>()._IsDead = true;
        }

        else if (collision.gameObject.layer == LayerMask.NameToLayer("PillLayer"))
        {
            StartCoroutine(Game_Events._Instance.CharacterGetPillSequence(this.gameObject));
            Destroy(collision.gameObject); // Destroy the pill
        }

        else if (collision.gameObject.layer == LayerMask.NameToLayer("FinalLayer"))
        {
            StartCoroutine(Game_Events._Instance.LevelCompletedSequence(this.gameObject));
            GetComponent<Character_Movement>()._IsLevelFinished = true;
        }

        else if (collision.gameObject.layer == LayerMask.NameToLayer("GameFinish"))
        {
            StartCoroutine(Game_Events._Instance.GameFinished(collision.gameObject));
            GetComponent<Character_Movement>()._IsLevelFinished = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("CoinLayer"))
        {
            Game_Events._Instance.CoinCollectSequnce(other.gameObject);
        }
    }
}
