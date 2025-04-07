using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Player _player;

    [SerializeField] private List<LevelPart> _levelParts;
    [SerializeField] private GameObject _levelBack;
    
    private List<LevelPart> _currentParts;
    private Transform _buildPoint;
    private System.Random _random;
    private bool _levelMoving = true;
    private int _prevIndex = 0;
    private float _levelSpeed = 3;
    private float _speedMultiplier;

    private void OnEnable()
    {
        _gameManager.gameStarted += StartLevelBuilding;
        _gameManager.gameStarted += EnableMoving;
        _player.died += DisableMoving;
    }

    private void OnDisable()
    {
        _gameManager.gameStarted -= StartLevelBuilding;
        _gameManager.gameStarted -= EnableMoving;
        _player.died -= DisableMoving;
    }

    private void Update()
    {
        if (_levelMoving)
            MoveLevel();
    }

    public void UpdateSpeed(float speed)
    {
        _speedMultiplier = speed;
    }

    public void BuildLevelPart()
    {
        int levelIndex = _random.Next(0, _levelParts.Count + 1);
        while (_prevIndex == levelIndex)
            levelIndex = _random.Next(0, _levelParts.Count + 1);
        Instantiate(_levelParts[levelIndex].gameObject, _buildPoint);
        _currentParts.Add(_levelParts[levelIndex]);
        _buildPoint.position += new Vector3(_levelParts[levelIndex].length, 0, 0);
    }

    private void EnableMoving()
    {
        _levelMoving = true;
    }

    private void DisableMoving()
    {
        _levelMoving = false;
    }

    private void StartLevelBuilding()
    {
        _random = new System.Random();
        for (int i = 0; i < 5; i++)
        {
            BuildLevelPart();
        }
    }


    private void MoveLevel()
    {
        foreach (var part in _currentParts)
        {
            part.transform.Translate(part.transform.position + new Vector3(_levelSpeed * _speedMultiplier, 0, 0));
        }
        _levelBack.transform.Translate(_levelBack.transform.position + new Vector3(_levelSpeed * _speedMultiplier, 0, 0));
    }
}