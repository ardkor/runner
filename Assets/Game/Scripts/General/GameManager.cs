using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event Action gameStarted;

    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private Player _player;

    private Coroutine _speedUpCoroutine;
    private float _speedChangeTime = 10;
    private float _speedChange = 0.2f;
    private float _gameSpeed = 1;

    private void OnEnable()
    {
        _player.died += EndGame;
    }

    private void OnDisable()
    {
        _player.died -= EndGame;
    }
    
    public void StartGame()
    {
        gameStarted?.Invoke();
        _speedUpCoroutine = StartCoroutine(SpeedUpTimer());
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void BuildPart()
    {
        _levelManager.BuildLevelPart();
    }

    private void EndGame()
    {
        StopCoroutine(_speedUpCoroutine);
    }

    private IEnumerator SpeedUpTimer()
    {
        while (true)
        {
            _gameSpeed += _speedChange;
            AddGameSpeed();
            yield return new WaitForSeconds(_speedChangeTime);
        }
    }

    private void AddGameSpeed()
    {
        _levelManager.UpdateSpeed(_gameSpeed);
    }
}