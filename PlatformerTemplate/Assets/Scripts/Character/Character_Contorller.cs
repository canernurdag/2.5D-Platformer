using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace Character_Controller
{ 
    public class Character_Contorller : MonoBehaviour
    {
        public Rigidbody _myRigidbody;

        [Header("Boolens")]
        public bool _IsReadyToStop; // To avoid chracter dragging on surface
        public bool _IsFaceRight;
        public bool _IsJumpButtonDown; // Full jump
        public bool _IsJumpButtonUp; // To create small jump
        public bool _IsGrounded;

        [Header("Inputs")]
        public float _moveHorizontal;
        public float _runSpeed;
        public float _jumpSpeed;
        public float _currentHorizontalVelocity; // Temp variable to calculate
        public float _fallFasterFactor;
        public float _characterSlowFactor;

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
        public float _groundCheckRadius;
        public LayerMask _groundLayerMask;
        public Transform _groundCheckTransform;

        private void Start()
        {
            DOTween.Init();
            _myRigidbody = GetComponent<Rigidbody>();

            _IsFaceRight = true;

            _runSpeed = 4;
            _jumpSpeed = 20;
            _groundCheckRadius = 0.2f;
            _fallFasterFactor = 15;
            _characterSlowFactor = 0.9f;


        }

        private void Update() //All inputs in Update function
        {
            _moveHorizontal = Input.GetAxis("Horizontal");
            
            if(Input.GetButtonDown("Jump"))
            {
                _IsJumpButtonDown = true;
            }
            if(Input.GetButtonUp("Jump"))
            {
                _IsJumpButtonUp = true;
            }

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

        private void FixedUpdate() // All physics in Fixed Update function
        {
            HorizontalMovementFunction();
            StopInXAxisFunction();
            IsGroundedFunction();
            JumpFunction();
            SmallJumpFunction();
            CharacterFallFasterFunction();
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
            if (_groundCollisionArray.Length > 0)
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
}
