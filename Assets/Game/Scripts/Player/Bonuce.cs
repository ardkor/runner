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
      if(_bonuceTimer != null)
         StopCoroutine(_bonuceTimer);
      enabled = true;
      _bonuceTimer = StartCoroutine(BonuceTime());
   }
   private IEnumerator BonuceTime()
   {
      yield return new WaitForSeconds(_duration);
      enabled = false;
   }
}
