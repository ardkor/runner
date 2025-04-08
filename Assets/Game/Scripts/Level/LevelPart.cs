using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPart : MonoBehaviour
{
    [SerializeField] private float _length;
    public float length => _length;

    [SerializeField] private GameObject _levelPartPrfab;

    public GameObject LevelPartPrfab => _levelPartPrfab;
}
