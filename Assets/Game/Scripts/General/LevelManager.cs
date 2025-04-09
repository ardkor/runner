using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Player _player;

    [SerializeField] private List<GameObject> _levelParts;
    [SerializeField] private GameObject _emptyLevelPart;
    [SerializeField] private Transform _level;
    [SerializeField] private Transform _levelPartsParent;
    [SerializeField] private Transform _buildPoint;
    private Vector3 _buildPosition;

    [SerializeField] private float _levelSpeed = 0.2f;

    private List<GameObject> _currentParts;
    private System.Random _random;
    private bool _levelMoving;
    private int _prevIndex = 0;
    private float _speedMultiplier = 1;

    private void OnEnable()
    {
        _gameManager.gameStarted += DestroyPrevLevel;
        _gameManager.gameStarted += StartLevelBuilding;
        _gameManager.gameStarted += EnableMoving;
        _player.died += DisableMoving;
        _player.died += StopAnimations;
        _player.died += ResetBuildPoint;
    }

    private void OnDisable()
    {
        _gameManager.gameStarted -= DestroyPrevLevel;
        _gameManager.gameStarted -= StartLevelBuilding;
        _gameManager.gameStarted -= EnableMoving;
        _player.died -= DisableMoving;
        _player.died -= StopAnimations;
        _player.died -= ResetBuildPoint;
    }

    private void StopAnimations()
    {
        foreach (var part in _currentParts)
        {
            part.GetComponent<LevelPart>().StopAnimations();
            part.GetComponent<LevelPart>().StopCollectables();
        }
    }
    private void Update()
    {
        if (_levelMoving)
            MoveLevel();
    }

    private void Start()
    {
        _buildPosition = _buildPoint.position;
    }

    public void UpdateSpeed(float speed)
    {
        _speedMultiplier = speed;
    }

    private void ResetBuildPoint()
    {
        _buildPoint.position = _buildPosition;
    }
    private void DestroyPrevLevel()
    {
        int partsCount = _levelPartsParent.transform.childCount;
        for (int i = 0; i < partsCount; i++)
        {
            Transform part = _levelPartsParent.transform.GetChild(i);
            Destroy(part.gameObject);
        }
    }
    public void BuildLevelPart()
    {
        int levelIndex = _random.Next(0, _levelParts.Count);
        if (_levelParts.Count > 1)
        {
            while (_prevIndex == levelIndex)
                levelIndex = _random.Next(0, _levelParts.Count);
            _prevIndex = levelIndex;
        }
        LevelPart levelPart = _levelParts[levelIndex].GetComponent<LevelPart>();
        _buildPoint.position -= new Vector3(0, 0, levelPart.length);
        GameObject part = Instantiate(_levelParts[levelIndex], _levelPartsParent);
        part.transform.position = _buildPoint.position;
        _currentParts.Add(part);
    }
    public void BuildEmptyLevelPart()
    {
        LevelPart levelPart = _emptyLevelPart.GetComponent<LevelPart>();
        _buildPoint.position -= new Vector3(0, 0, levelPart.length);
        GameObject part = Instantiate(_emptyLevelPart, _levelPartsParent);
        part.transform.position = _buildPoint.position;
        _currentParts.Add(part);
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
        BuildEmptyLevelPart();
        for (int i = 0; i < 7; i++)
        {
            BuildLevelPart();
        }
    }

    private void MoveLevel()
    {
        _level.Translate(new Vector3(0, 0, _levelSpeed * _speedMultiplier * Time.deltaTime));
    }
}