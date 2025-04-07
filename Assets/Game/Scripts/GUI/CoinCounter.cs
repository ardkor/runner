using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class CoinCounter : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    [SerializeField] private Player _player;
    
    [SerializeField] private TMP_Text _score;
    
    private int _coins;

    public int Coins => _coins;
    private void OnEnable()
    {
        _gameManager.gameStarted += ResetCoins;
    }

    private void OnDisable()
    {
        _gameManager.gameStarted -= ResetCoins;
    }

    private void ResetCoins()
    {
        _coins = 0;
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void AddCoin()
    {
        _coins += 1;
        UpdateScore();
    }
    private void UpdateScore()
    {
        _score.text = _coins.ToString();
    }
}
