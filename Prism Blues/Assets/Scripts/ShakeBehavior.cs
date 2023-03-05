using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeBehavior : MonoBehaviour
{
    private Transform transform;

    private float shakeDuration = 0f;

    private float shakeMagnitude = 0.7f;

    private float dampingSpeed = 1.0f;

    Vector3 initialPosition;


    private void Awake()
    {
        if (transform == null)
        {
            transform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    private void OnEnable()
    {
        initialPosition = transform.localPosition;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }
    public void TriggerShake(float duration, float magnitude) 
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }
}
