using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField]
    protected float rotationSpeedX, rotationSpeedY=0.5f, rotationSpeedZ;
    
    private float _disappearanceDuration = 0.2f;

    private Vector3 _size;

    public void StopRotating()
    {
        rotationSpeedY = 0f;
    }
    private void Start()
    {
        _size = transform.localScale;
    }
    protected void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>() != null)
            StartCoroutine(Disappearance());
    }


    protected virtual void Update()
    {
        transform.Rotate(rotationSpeedX, rotationSpeedY, rotationSpeedZ);
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