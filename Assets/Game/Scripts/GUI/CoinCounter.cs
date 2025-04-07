using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class CoinCounter : MonoBehaviour
{
    [SerializeField] private Player _player;
    
    [SerializeField] private TMP_Text _score;
    
    private int _coins;

    public int Coins => _coins;
    private void OnEnable()
    {
        _player.coinRaised += UpdateScore;
    }
    private void OnDisable()
    {
        _player.coinRaised -= UpdateScore;
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
    private void UpdateScore(int coins)
    {
        _coins = coins;
        _score.text = _coins.ToString();
    }
}
