using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public event Action died;

    [SerializeField] private BonucesManager _bonucesManager;
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private CoinCounter _coinCounter;

    private bool isDead;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!isDead)
        {
            if (other.GetComponent<Coin>())
            {
                if (_bonucesManager.DoubleCoinsEnabled)
                    _coinCounter.AddCoin();
                _coinCounter.AddCoin();
            }
            else if (other.GetComponent<Obstacle>())
            {
                if (!_bonucesManager.InvincibilityEnabled)
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
            else if (other.GetComponent<LevelPartTrigger>())
            {
                _levelManager.PassPart();
            }
        }
    }

    public void ComeToLfe()
    {
        StartCoroutine(ComingToLife());
    }

    private IEnumerator ComingToLife()
    {
        yield return new WaitForSeconds(1);
        isDead = false;
    }

    private void Die()
    {
        isDead = true;
        died?.Invoke();
    }
}