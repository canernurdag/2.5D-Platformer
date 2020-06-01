using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Animator : MonoBehaviour
{
    public Animator _myAnimator;


    private void Start()
    {
        _myAnimator = GetComponent<Animator>();
        Game_Events._Instance._onCharacterDieFirst += AnimationDie;
        Game_Events._Instance._onLevelCompletedFirst += AnimationVictory;



    }
    public void AnimationIdle() 
    {
        _myAnimator.SetBool("Idle", true);
        _myAnimator.SetBool("Run", false);
        _myAnimator.SetBool("Climb", false);
        _myAnimator.SetBool("Jump1", false);
        _myAnimator.SetBool("Jump2", false);


    }

    public void AnimationRun()
    {
        _myAnimator.SetBool("Idle", false);
        _myAnimator.SetBool("Run", true);
        _myAnimator.SetBool("Climb", false);
        _myAnimator.SetBool("Jump1", false);
        _myAnimator.SetBool("Jump2", false);


    }

    public void AnimationJumpPhase1()
    {
        _myAnimator.SetBool("Idle", false);
        _myAnimator.SetBool("Run", false);
        _myAnimator.SetBool("Climb", false);
        _myAnimator.SetBool("Jump1", true);
        _myAnimator.SetBool("Jump2", false);

    }

    public void AnimationJumpPhase2()
    {
        _myAnimator.SetBool("Idle", false);
        _myAnimator.SetBool("Run", false);
        _myAnimator.SetBool("Climb", false);
        _myAnimator.SetBool("Jump1", false);
        _myAnimator.SetBool("Jump2", true);

    }

    public void AnimatorClimb()
    {
        _myAnimator.SetBool("Idle", false);
        _myAnimator.SetBool("Run", false);
        _myAnimator.SetBool("Climb", true);
        _myAnimator.SetBool("Jump1", false);
        _myAnimator.SetBool("Jump2", false);

    }


    public void AnimationDie(GameObject _thisGameObject)
    {
        _myAnimator.SetTrigger("Die");

        _myAnimator.SetBool("Idle", false);
        _myAnimator.SetBool("Run", false);
        _myAnimator.SetBool("Climb", false);
        _myAnimator.SetBool("Jump1", false);
        _myAnimator.SetBool("Jump2", false);
    }

    public void AnimationVictory(GameObject _thisGameObject)
    {
        _myAnimator.SetTrigger("Victory");

        _myAnimator.SetBool("Idle", false);
        _myAnimator.SetBool("Run", false);
        _myAnimator.SetBool("Climb", false);
        _myAnimator.SetBool("Jump1", false);
        _myAnimator.SetBool("Jump2", false);

    }

    private void OnDisable()
    {
        Game_Events._Instance._onCharacterDieFirst -= AnimationDie;
        Game_Events._Instance._onLevelCompletedFirst -= AnimationVictory;
    }
}
