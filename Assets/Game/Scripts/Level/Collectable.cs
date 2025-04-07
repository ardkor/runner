using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private float _disappearanceDuration = 0.2f;

    private Vector3 _size;

    private void Start()
    {
        _size = transform.localScale;
    }
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Disappearance());
    }

    private IEnumerator Disappearance()
    {
        float timer = 0;
        while (_disappearanceDuration >= timer)
        {
            timer += Time.deltaTime;
            transform.localScale = new Vector3(Mathf.Lerp(_size.x, 0, timer / _disappearanceDuration),
                Mathf.Lerp(_size.y, 0, timer / _disappearanceDuration),
                Mathf.Lerp(_size.z, 0, timer / _disappearanceDuration));
            yield return new WaitForSeconds(Time.deltaTime);
        }

        Destroy(gameObject);
    }
}