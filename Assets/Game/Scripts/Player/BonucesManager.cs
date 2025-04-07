using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonucesManager : MonoBehaviour
{
    [SerializeField] private Bonuce _doubleCoins;
    [SerializeField] private Bonuce _invincibility;

    public bool DoubleCoinsEnabled => _doubleCoins.enabled;
    public bool InvincibilityEnabled => _doubleCoins.enabled;

    public void GetDoubleCoinsBonuce()
    {
        _doubleCoins.GetBonuce();
    }
    public void GetInvincibilityBonuce()
    {
        _invincibility.GetBonuce();
    }
}
