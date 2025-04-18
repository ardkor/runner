using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event Action gameStarted;

    [SerializeField] private EndGameMenu _endGameMenu;
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private Player _player;
    [SerializeField] private PlayerController _playerController;
    
    private Coroutine _speedUpCoroutine;
    private float _speedChangeTime = 10;
    private float _speedChange = 0.5f;
    private float _gameSpeed = 1;

    private void OnEnable()
    {
        _player.died += StopAccelerating;
        _player.died += StopMusic;
        _player.died += OpenEndMenu;
    }

    private void OnDisable()
    {
        _player.died -= StopAccelerating;
        _player.died -= StopMusic;
        _player.died -= OpenEndMenu;
    }
    
    public void StartGame()
    {
        SoundManager.Instance.StartMusic(SoundManager.actionMusic);
        SoundInstance.musicVolume = 0.4f;
        gameStarted?.Invoke();
        _player.ComeToLfe();
        _gameSpeed = 1;
        ChangeGameSpeed();
        _speedUpCoroutine = StartCoroutine(SpeedUpTimer());
    }

    private void StopMusic()
    {
        SoundManager.Instance.PauseMusic();
    }
    public void Exit()
    {
        Application.Quit();
    }

    private void StopAccelerating()
    {
        StopCoroutine(_speedUpCoroutine);
    }

    private void OpenEndMenu()
    {
        _endGameMenu.Activate();
    }

    private IEnumerator SpeedUpTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(_speedChangeTime);
            _gameSpeed += _speedChange;
            ChangeGameSpeed();
        }
    }

    private void ChangeGameSpeed()
    {
        _levelManager.UpdateSpeed(_gameSpeed);
        _playerController.UpdateSpeed(_gameSpeed);
    }
}