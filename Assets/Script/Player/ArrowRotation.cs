using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRotation : MonoBehaviour
{
    [SerializeField] private Transform _targetObject;
    [SerializeField] private float _maxDistance = 0.3f;
    [SerializeField] private float _rotationSpeed = 80f; 

    private float _maxRotationAngle = 60f;
    private float _currentRotationAngle = 0f; 
    
    private bool _rotatingRight = true;
    
    void Update()
    {
        Rotation();
    }

    private void Rotation()
    {
        float rotationDelta = _rotationSpeed * Time.deltaTime;
        if (_rotatingRight)
        {
            _currentRotationAngle += rotationDelta;
            if (_currentRotationAngle >= _maxRotationAngle)
            {
                _currentRotationAngle = _maxRotationAngle;
                _rotatingRight = false;
            }
        }
        else
        {
            _currentRotationAngle -= rotationDelta;
            if (_currentRotationAngle <= -_maxRotationAngle)
            {
                _currentRotationAngle = -_maxRotationAngle;
                _rotatingRight = true;
            }
        }

        Vector3 offset = new Vector3(Mathf.Sin(_currentRotationAngle * Mathf.Deg2Rad), Mathf.Cos(_currentRotationAngle * Mathf.Deg2Rad), 0) * _maxDistance;
        transform.position = _targetObject.position + offset;

        
        transform.up = transform.position - _targetObject.position;
    }
    
}
