using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Player _player;

    [SerializeField] private List<LevelPart> _levelParts;
    [SerializeField] private GameObject _levelBack;
    [SerializeField] private Transform _levelParent;
    [SerializeField] private Transform _buildPoint;

    private List<LevelPart> _currentParts;
    private System.Random _random;
    private bool _levelMoving = true;
    private int _prevIndex = 0;
    private float _levelSpeed = 3;
    private float _speedMultiplier = 1;

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
        _buildPoint.position += new Vector3(_levelParts[levelIndex].length, 0, 0);
        GameObject levelPart = Instantiate(_levelParts[levelIndex].gameObject, _levelParent);
        levelPart.transform.position = _buildPoint.position;
        _currentParts.Add(_levelParts[levelIndex]);
        
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
            part.transform.Translate(part.transform.position + new Vector3(0, 0, _levelSpeed * _speedMultiplier * Time.deltaTime));
        }
        _levelBack.transform.Translate(_levelBack.transform.position + new Vector3(0, 0, _levelSpeed * _speedMultiplier * Time.deltaTime));
    }
}