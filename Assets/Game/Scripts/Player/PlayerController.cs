using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    private static readonly int JumpKey = Animator.StringToHash("Jump");
    private static readonly int RunKey = Animator.StringToHash("Run");

    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Player _player;
    [SerializeField] private float _firstRawX;
    [SerializeField] private float _spaceBetweenRaws;
    
    private Animator _animator;

    private Vector3 _currentDirection;

    private float _sideSpeed = 3;
    private float _speedMultiplier;

    private int _rawsCount = 3;
    private int _currentRaw = 2;
    private float tolerance = 0.04f;

    private bool _movingSide;
    private bool _active;

    public void UpdateSpeed(float speed)
    {
        _speedMultiplier = speed;
    }

    private void OnEnable()
    {
        _gameManager.gameStarted += Enable;
        _player.died += Disable;
    }

    private void OnDisable()
    {
        _gameManager.gameStarted -= Enable;
        _player.died -= Disable;
    }

    private void Enable()
    {
        _active = true;
    }

    private void Disable()
    {
        _active = true;
    }

    private void Update()
    {
        if (_active)
        {
            if (Input.GetKeyDown(KeyCode.A) && _currentRaw != 1)
            {
                _currentRaw -= 1;
                _currentDirection = Vector3.left;
                _movingSide = true;
            }
            else if (Input.GetKeyDown(KeyCode.D) && _currentRaw != _rawsCount)
            {
                _currentRaw += 1;
                _currentDirection = Vector3.right;
                _movingSide = true;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            if (_movingSide)
            {
                transform.Translate(transform.position +
                                    _currentDirection * (_sideSpeed * _speedMultiplier * Time.deltaTime));
                _movingSide = !TryReachTarget();
            }
        }
    }

    private void Jump()
    {
        _animator.SetTrigger(JumpKey);
    }

    private bool TryReachTarget()
    {
        if (_currentRaw == 1)
        {
            if (transform.position.x >= _firstRawX - tolerance && transform.position.x <= _firstRawX + tolerance)
            {
                return true;
            }
        }
        else if (_currentRaw == 2)
        {
            if (transform.position.x >= _firstRawX + _spaceBetweenRaws - tolerance &&
                transform.position.x <= _firstRawX + _spaceBetweenRaws + tolerance)
            {
                return true;
            }
        }
        else if (_currentRaw == 3)
        {
            if (transform.position.x >= _firstRawX + _spaceBetweenRaws * 2 - tolerance &&
                transform.position.x <= _firstRawX + _spaceBetweenRaws * 2 + tolerance)
            {
                return true;
            }
        }

        return false;
    }
}