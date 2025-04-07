using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action died;
    public event Action<int> coinRaised;

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
            _coinCounter.AddCoin();
        }
        else if (other.GetComponent<Obstacle>())
        {
            Die();
        }
        
    }
    

    private void Die()
    {
        died?.Invoke();
    }
}
