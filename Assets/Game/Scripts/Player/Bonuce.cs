using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonuce : MonoBehaviour
{
   [SerializeField] private float _duration;
   private Coroutine _bonuceTimer;
   public bool enabled { get; private set;}

   public void GetBonuce()
   {
      enabled = true;
      StopCoroutine(_bonuceTimer);
      _bonuceTimer = StartCoroutine(BonuceTime());
   }
   private IEnumerator BonuceTime()
   {
      yield return new WaitForSeconds(_duration);
      enabled = false;
   }
}
