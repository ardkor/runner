using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    private static readonly int JumpKey = Animator.StringToHash("Jump");
    private static readonly int JumpEndKey = Animator.StringToHash("JumpEnd");
    private static readonly int RunKey = Animator.StringToHash("Run");

    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Player _player;
    [SerializeField] private float _firstRawX;
    [SerializeField] private float _spaceBetweenRaws;

    [SerializeField] private float _sideSpeed = 3;

    [SerializeField] private Animator _animator;

    private Vector3 _position;
    private float _movePosition;
    private float _targetPosition;

    private float _speedMultiplier = 1;

    private int _rawsCount = 3;

    private int _currentRaw = 2;

    //private float _jumpTimer;
    private float _jumpTime = 1.5f;
    private float _targetHeight = 3f;

    private float _sideMovingTimer;

    private bool _jumping;
    private bool _sidMoving;
    private bool _active;

    public void UpdateSpeed(float speed)
    {
        _speedMultiplier = speed;
    }

    private void OnEnable()
    {
        _gameManager.gameStarted += Enable;
        _gameManager.gameStarted += ResetAnimator;
        _gameManager.gameStarted += MoveToStart;
        _player.died += Disable;
    }

    private void OnDisable()
    {
        _gameManager.gameStarted -= Enable;
        _gameManager.gameStarted -= ResetAnimator;
        _gameManager.gameStarted -= MoveToStart;
        _player.died -= Disable;
    }

    private void Start()
    {
        _position = transform.position;
    }

    private void Enable()
    {
        _animator.enabled = true;
        _active = true;
    }

    private void Disable()
    {
        _animator.enabled = false;
        _active = false;
    }

    private void ResetAnimator()
    {
        _animator.Play("run");
    }

    private void MoveToStart()
    {
        transform.position = new Vector3(_firstRawX - _spaceBetweenRaws * (_rawsCount - 2), _position.y, _position.z);
    }

    private void Update()
    {
        if (_active)
        {
            if (Input.GetKeyDown(KeyCode.A) && _currentRaw != 1)
            {
                _movePosition = transform.position.x;
                _targetPosition = _firstRawX - _spaceBetweenRaws * (_currentRaw - 2);
                _currentRaw -= 1;
                _sideMovingTimer = 0;
                _sidMoving = true;
            }
            else if (Input.GetKeyDown(KeyCode.D) && _currentRaw != _rawsCount)
            {
                _movePosition = transform.position.x;
                _targetPosition = _firstRawX - _spaceBetweenRaws * _currentRaw;
                _currentRaw += 1;
                _sideMovingTimer = 0;
                _sidMoving = true;
            }

            if (Input.GetKeyDown(KeyCode.Space) && !_jumping)
            {
                Jump();
            }

            if (_sidMoving)
            {
                _sideMovingTimer += Time.deltaTime;
                float t = _sideMovingTimer / (1 / _sideSpeed * _speedMultiplier);
                transform.position =
                    new Vector3(Mathf.Lerp(_movePosition, _targetPosition, t), transform.position.y, 0);
                if (t >= 1)
                    _sidMoving = false;
            }
        }
    }

    private void Jump()
    {
        _animator.SetTrigger(JumpKey);
        StartCoroutine(JumpTime());
    }

    private IEnumerator JumpTime()
    {
        _jumping = true;
        float posY = transform.position.y;
        float jumpTimer = 0;
        while (jumpTimer < _jumpTime / _speedMultiplier)
        {
            float t = jumpTimer / _jumpTime * _speedMultiplier;
            if (jumpTimer < _jumpTime / _speedMultiplier / 2)
                transform.position = new Vector3(transform.position.x, Mathf.Lerp(posY, _targetHeight, t * 2), 0);
            else
                transform.position = new Vector3(transform.position.x, Mathf.Lerp(_targetHeight, posY, t * 2 - 1), 0);
            jumpTimer += Time.deltaTime;
            yield return null;
        }

        transform.position = new Vector3(transform.position.x, posY, 0);
        _jumping = false;
    }
}