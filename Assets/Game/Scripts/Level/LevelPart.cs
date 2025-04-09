using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPart : MonoBehaviour
{
    [SerializeField] private float _length;
    public float length => _length;

    [SerializeField] private GameObject _levelPartPrfab;

    //public GameObject LevelPartPrfab => _levelPartPrfab;
    
    public void StopAnimations()
    {
        Animator[] animators = GetComponentsInChildren<Animator>();
        foreach (var animator in animators)
        {
            animator.enabled = false;
        }
    }

    public void StopCollectables()
    {
        Collectable[] collectables = GetComponentsInChildren<Collectable>();
        foreach (var collectable in collectables)
        {
            collectable.StopRotating();
        }
    }
}
