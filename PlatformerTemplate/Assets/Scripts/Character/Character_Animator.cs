using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Animator : MonoBehaviour
{
    public Animator _myAnimator;

    private void Start()
    {
        _myAnimator = GetComponent<Animator>();

        Game_Events._Instance._onCharacterDie += AnimationDie;
    }
    public void AnimationIdle() 
    {

    }

    public void AnimationRun()
    {

    }

    public void AnimationJump()
    {

    }

    public void AnimationDie(GameObject _thisGameObject)
    {
        _myAnimator.SetBool("Die", true);
    }
}
