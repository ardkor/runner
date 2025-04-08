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

    [SerializeField] private float _levelSpeed = 0.2f;
    
    private List<GameObject> _currentParts;
    private System.Random _random;
    private bool _levelMoving;
    private int _prevIndex = 0;
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
        int levelIndex = _random.Next(0, _levelParts.Count);
        while (_prevIndex == levelIndex)
            levelIndex = _random.Next(0, _levelParts.Count);
        _buildPoint.position -= new Vector3(0, 0, _levelParts[levelIndex].length);
        GameObject levelPart = Instantiate(_levelParts[levelIndex].LevelPartPrfab, _levelParent);
        levelPart.transform.position = _buildPoint.position;
        _currentParts.Add(levelPart);
    }

    public void PassPart()
    {
        RemovePart();
        BuildLevelPart();
    }
    public void RemovePart()
    {
        GameObject part = _currentParts[0];
        _currentParts.Remove(_currentParts[0]);
        Destroy(part);
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
        _currentParts = new List<GameObject>();
        _random = new System.Random();
        for (int i = 0; i < 5; i++)
        {
            BuildLevelPart();
        }
    }


    private void MoveLevel()
    {
        _levelParent.Translate(new Vector3(0, 0, _levelSpeed * _speedMultiplier * Time.deltaTime));
        _levelBack.transform.Translate(new Vector3(0, 0, -_levelSpeed * _speedMultiplier * Time.deltaTime));
    }
}