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
    //[SerializeField] private float _jumpTime = 1.3f;

    [SerializeField] private float _sideSpeed = 3;
    
    [SerializeField] private Animator _animator;

    private Vector3 _currentDirection;
    private float _movePosition;
    private float _targetPosition;

    private float _speedMultiplier = 1;

    private int _rawsCount = 3;
    private int _currentRaw = 2;
    //private float tolerance = 0.3f;
    private float _movingTimer;

    private bool _jumping;
    private bool _movingSide;
    private bool _active;

    public void UpdateSpeed(float speed)
    {
        _speedMultiplier = speed;
    }

    private void OnEnable()
    {
        _gameManager.gameStarted += Enable;
        _gameManager.gameStarted += MoveToStart;
        _player.died += Disable;
    }

    private void OnDisable()
    {
        _gameManager.gameStarted -= Enable;
        _gameManager.gameStarted -= MoveToStart;
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

    private void MoveToStart()
    {
        _targetPosition = _firstRawX - _spaceBetweenRaws * (_currentRaw - 1);
    }
    private void Update()
    {
        if (_active)
        {
            if (Input.GetKeyDown(KeyCode.A) && _currentRaw != 1)
            {
                //_currentDirection = Vector3.right;
                _movingSide = true;
                _movePosition = transform.position.x;
                _targetPosition = _firstRawX - _spaceBetweenRaws * (_currentRaw - 2);
                _currentRaw -= 1;
                _movingTimer = 0;
            }
            else if (Input.GetKeyDown(KeyCode.D) && _currentRaw != _rawsCount)
            {
                //_currentDirection = Vector3.left;
                //_movingSide = true;
                _movePosition = transform.position.x;
                _targetPosition = _firstRawX - _spaceBetweenRaws * _currentRaw;
                _currentRaw += 1;
                _movingTimer = 0;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                //if(!_jumping)
                Jump();
            }
            
                _movingTimer += Time.deltaTime;
                float t = _movingTimer / (1 / _sideSpeed * _speedMultiplier); //_movePosition / _targetPosition 
                transform.position = new Vector3(Mathf.Lerp(_movePosition, _targetPosition, t), 0, 0);

                //transform.Translate(_currentDirection * (_sideSpeed * _speedMultiplier * Time.deltaTime));
        }
    }

    private void Jump()
    {
        //_jumping = true;
        _animator.SetTrigger(JumpKey);
        //StartCoroutine(JumpAnimation());
    }

    /*private IEnumerator JumpAnimation()
    {
        yield return new WaitForSeconds(_jumpTime);
        _animator.SetTrigger(JumpEndKey);
        _jumping = false;
    }*/

    /*private bool TryReachTarget()
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
            if (transform.position.x >= _firstRawX - _spaceBetweenRaws - tolerance &&
                transform.position.x <= _firstRawX - _spaceBetweenRaws + tolerance)
            {
                return true;
            }
        }
        else if (_currentRaw == 3)
        {
            if (transform.position.x >= _firstRawX - _spaceBetweenRaws * 2 - tolerance &&
                transform.position.x <= _firstRawX - _spaceBetweenRaws * 2 + tolerance)
            {
                return true;
            }
        }

        return false;
    }*/
}