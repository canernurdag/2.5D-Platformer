using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;


public class Character_Movement : MonoBehaviour
{
    public Rigidbody _myRigidbody;
    public Character_Animator _myCharacterAnimator;

    [Header("Boolens")]
    public bool _CanMove;
    public bool _IsReadyToStop; // To avoid chracter dragging on surface
    public bool _IsFaceRight;
    public bool _IsJumpButtonDown; // Full jump
    public bool _IsJumpButtonUp; // To create small jump
    public bool _IsGrounded;
    public bool _IsWallSliding;
    public bool _IsDead;
    public bool _IsLevelFinished;

    [Header("Inputs")]
    public float _moveHorizontal;
    public float _runSpeed;
    public float _jumpSpeed;
    public float _currentHorizontalVelocity; // Temp variable to calculate
    public float _fallFasterFactor;
    public float _characterSlowFactor;
    public float _wallSlideSpeed;
    public float _wallJumpForceX;
    public float _wallJumpForceY;

    [Header("Movement Detail Settings")]
    [SerializeField][Range(0, 1)]
    float _HorizontalDampingBasic = 0.6f;
    [SerializeField][Range(0, 1)]
    float _HorizontalDampingWhenStopping = 0.1f;
    [SerializeField][Range(0, 1)]
    float _HorizontalDampingWhenTurning = 0.1f;
    [SerializeField][Range(0, 1)]
    float _jumpShortenFactor = 0.5f;

    [Header("Ground Check Settings")]
    public Collider[] _groundCollisionArray;
    public Collider[] _objectForJumpCollisionArray;
    public float _groundCheckRadius;
    public LayerMask _groundLayerMask;
    public LayerMask _objectForJumpLayerMask;
    public Transform _groundCheckTransform;

    [Header("Wall Check Settings")]
    public Collider[] _wallCollisionArray;
    public float _wallCheckRadius;
    public LayerMask _objectForJumpLayerMask2;
    public Transform _wallCheckTransform;

    private void Start()
    {
        DOTween.Init();
        _myRigidbody = GetComponent<Rigidbody>();
        _myCharacterAnimator = GetComponent<Character_Animator>();

        _CanMove = true;
        _IsFaceRight = true;
        _IsDead = false;
        _IsLevelFinished = false;

        _runSpeed = 4;
        _jumpSpeed = 25;
        _wallSlideSpeed = 1f;
        _wallJumpForceX = 2000;
        _wallJumpForceY = 1300;
        _groundCheckRadius = 0.2f;
        _wallCheckRadius = 0.5f;
        _fallFasterFactor = 10;
        _characterSlowFactor = 0.9f;


    }

    private void Update() //All inputs in Update function
    {

        if(_CanMove)
        { 
            _moveHorizontal = Input.GetAxisRaw("Horizontal");

            if (Input.GetButtonDown("Jump"))
            {
                _IsJumpButtonDown = true;
                Invoke("IsJumpButtonDownButtonClearFunction", 0.1f);
            }
            if (Input.GetButtonUp("Jump"))
            {
                _IsJumpButtonUp = true;
                Invoke("IsJumpButtonUpButtonClearFunction", 0.1f);
            }

            CharacterMoveAdjustFunctions();

            //Character Animation Changes
            if(!_IsLevelFinished && !_IsDead)
            { 
                CallAnimationIdle();
                CallAnimationRun();
                CallAnimationJumpPhase1();
                CallAnimationJumpPhase2();
                CallAnimationClimb();
            }
        }
    }

    private void IsJumpButtonDownButtonClearFunction()
    {
        _IsJumpButtonDown = false;
    }
    private void IsJumpButtonUpButtonClearFunction()
    {
        _IsJumpButtonUp = false;
    }
    private void CallAnimationClimb()
    {
        if(_IsWallSliding)
        {
            _myCharacterAnimator.AnimatorClimb();
        }
    }
    private void CharacterMoveAdjustFunctions()
    {
        if (_moveHorizontal == 0)
        {
            _IsReadyToStop = true;
        } // Stop in X axis
        if (_moveHorizontal < 0 && _IsFaceRight == true)
        {
            _IsFaceRight = false;
            transform.DORotate(new Vector3(0, -90, 0), 0.5f);

        } // Turn Left
        if (_moveHorizontal > 0 && _IsFaceRight == false)
        {
            _IsFaceRight = true;
            transform.DORotate(new Vector3(0, 90, 0), 0.5f);

        } // Turn Right
    }
    private void CallAnimationJumpPhase2()
    {
        if (!_IsGrounded && _myRigidbody.velocity.y < 0)
        {
            _myCharacterAnimator.AnimationJumpPhase2();
        }
    }
    private void CallAnimationJumpPhase1()
    {
        if (!_IsGrounded)
        {
            _myCharacterAnimator.AnimationJumpPhase1();
        }
    }
    private void CallAnimationRun()
    {
        if (Mathf.Abs(_moveHorizontal) > 0 && _IsGrounded)
        {
            _myCharacterAnimator.AnimationRun();
        }
                         
    }
    private void CallAnimationIdle()
    {
        if (_moveHorizontal == 0 && _IsGrounded)
        {
            _myCharacterAnimator.AnimationIdle();
        }
    }

    private void FixedUpdate() // All physics in Fixed Update function
    {
        if(_CanMove)
        { 
            HorizontalMovementFunction();
            StopInXAxisFunction();
            IsGroundedFunction();
            JumpFunction();
            SmallJumpFunction();
            CharacterFallFasterFunction();
            IsWallTouchedFunction();
            WallSlidingFunction();
            WallJumpFunction();
        }
    }

    private void WallSlidingFunction()
    {
        if (_IsWallSliding && _myRigidbody.velocity.y < -_wallSlideSpeed)
        {
            _myRigidbody.velocity = new Vector3(_myRigidbody.velocity.x, -_wallSlideSpeed, _myRigidbody.velocity.z);
        }
        else
            return;
    }
    private void WallJumpFunction()
    {
        if (_IsWallSliding == true && _IsFaceRight == true && _IsJumpButtonDown == true)
        {
            _IsJumpButtonDown = false;
            _IsFaceRight = false;
            transform.DORotate(new Vector3(0, -90, 0), 0.1f);
            _myRigidbody.velocity = Vector3.zero;
            Vector3 _force = new Vector3(-_wallJumpForceX, _wallJumpForceY, 0);
            _myRigidbody.AddForce(_force);

        }

        else if (_IsWallSliding == true && _IsFaceRight == false && _IsJumpButtonDown == true)
        {
            _IsJumpButtonDown = false;
            _IsFaceRight = true;
            transform.DORotate(new Vector3(0, 90, 0), 0.1f);
            _myRigidbody.velocity = Vector3.zero;
            Vector3 _force = new Vector3(_wallJumpForceX, _wallJumpForceY, 0);
            _myRigidbody.AddForce(_force);
        }
    }
    private void IsWallTouchedFunction()
    {
        _wallCollisionArray = Physics.OverlapSphere(_wallCheckTransform.position, _wallCheckRadius, _objectForJumpLayerMask2);
        if (_wallCollisionArray.Length > 0 && !_IsGrounded)
        {
            _IsWallSliding = true;
        }
        else
            _IsWallSliding = false;
    }
    private void StopInXAxisFunction()
    {
        if (_IsReadyToStop == true)
        {
            _myRigidbody.velocity = new Vector3(0, _myRigidbody.velocity.y, _myRigidbody.velocity.z);
            _IsReadyToStop = false;
        }
    }
    private void CharacterFallFasterFunction()
    {
        if(!_IsGrounded)
        {
            _myRigidbody.AddForce(Vector3.down * _fallFasterFactor);
        }
    }
    private void SmallJumpFunction()
    {
        if (_myRigidbody.velocity.y > 0 && _IsJumpButtonUp)
        {
            _myRigidbody.velocity = new Vector3(_myRigidbody.velocity.x, _myRigidbody.velocity.y * _jumpShortenFactor, _myRigidbody.velocity.z);
            _IsJumpButtonUp = false;
        }
    }
    private void JumpFunction()
    {
        if (_IsGrounded == true && _IsJumpButtonDown)
        {
            _myRigidbody.velocity = new Vector3(_myRigidbody.velocity.x, _jumpSpeed, _myRigidbody.velocity.z);
            _IsGrounded = false;
            _IsJumpButtonDown = false;
        }
    }
    private void IsGroundedFunction()
    {
        _groundCollisionArray = Physics.OverlapSphere(_groundCheckTransform.position, _groundCheckRadius, _groundLayerMask);
        _objectForJumpCollisionArray = Physics.OverlapSphere(_groundCheckTransform.position, _groundCheckRadius, _objectForJumpLayerMask);
        if (_groundCollisionArray.Length > 0 ||_objectForJumpCollisionArray.Length>0)
        {
            _IsGrounded = true;
        }
        else
            _IsGrounded = false;
    }
    private void HorizontalMovementFunction()
    {
        _currentHorizontalVelocity = _myRigidbody.velocity.x;
        _currentHorizontalVelocity += _moveHorizontal * _runSpeed;

        if (Mathf.Abs(_moveHorizontal) < 0.01f)
        {
            _currentHorizontalVelocity *= Mathf.Pow(1f - _HorizontalDampingWhenStopping, Time.deltaTime * 10f);
        }
        else if (Mathf.Sign(_moveHorizontal) != Mathf.Sign(_currentHorizontalVelocity))
        {
            _currentHorizontalVelocity *= Mathf.Pow(1f - _HorizontalDampingWhenTurning, Time.deltaTime * 10f);
        }
        else
        {
            _currentHorizontalVelocity *= Mathf.Pow(1f - _HorizontalDampingBasic, Time.deltaTime * 10f);
        }

        if(_IsGrounded)
        { 
                _myRigidbody.velocity = new Vector3(_currentHorizontalVelocity, _myRigidbody.velocity.y, _myRigidbody.velocity.z);
        }
        else if (!_IsGrounded)
        {
            _myRigidbody.velocity = new Vector3(_currentHorizontalVelocity*_characterSlowFactor, _myRigidbody.velocity.y, _myRigidbody.velocity.z);
        }
    }
}   

