using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action died;

    [SerializeField] private BonucesManager _bonucesManager;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private CoinCounter _coinCounter;

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<LevelPart>())
        {
            _gameManager.BuildPart();
        }
        else if (other.GetComponent<Coin>())
        {
            if(_bonucesManager.DoubleCoinsEnabled)
                _coinCounter.AddCoin();
            _coinCounter.AddCoin();
        }
        else if (other.GetComponent<Obstacle>())
        {
            if(!_bonucesManager.InvincibilityEnabled)
                Die();
        }
        else if (other.GetComponent<DoubleCoins>())
        {
            _bonucesManager.GetDoubleCoinsBonuce();
        }
        else if (other.GetComponent<Invincibility>())
        {
            _bonucesManager.GetInvincibilityBonuce();
        }
    }
    

    private void Die()
    {
        died?.Invoke();
    }
}
