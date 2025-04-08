using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectable
{
    private void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.GetComponent<Player>() != null)
        {
            SoundManager.Instance.PlaySound(SoundManager.coinSound, transform.position);
        }
    }
}