using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRotation : MonoBehaviour
{
    [SerializeField] private Transform _targetObject;
    
    [SerializeField] private float _maxDistance = 0.3f;
    [SerializeField] private float _rotationSpeed = 80f; 

    private float MaxRotationAngle => 60f + _angleDifference;
    private float MinRotationAngle => -60f + _angleDifference;
    private float _angleDifference = 0f;
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
            if (_currentRotationAngle >= MaxRotationAngle)
            {
                _currentRotationAngle = MaxRotationAngle;
                _rotatingRight = false;
            }
        }
        else
        {
            _currentRotationAngle -= rotationDelta;
            if (_currentRotationAngle <= MinRotationAngle)
            {
                _currentRotationAngle = MinRotationAngle;
                _rotatingRight = true;
            }
        }

        Vector3 offset = new Vector3(Mathf.Sin(_currentRotationAngle * Mathf.Deg2Rad), Mathf.Cos(_currentRotationAngle * Mathf.Deg2Rad), 0) * _maxDistance;
        transform.position = _targetObject.position + offset;

        transform.up = transform.position - _targetObject.position;
    }

    public void OnBallCollision(Vector2 collisionPoint, float angle)
    {
        Vector3 directionToCollision = (Vector3)collisionPoint - _targetObject.position;
        Vector3 newOffset = directionToCollision.normalized * -_maxDistance;
        transform.position = _targetObject.position + newOffset;
        transform.up = transform.position - _targetObject.position;

       
        _currentRotationAngle = Mathf.Atan2(newOffset.y, newOffset.x) * Mathf.Rad2Deg;

        
        _rotatingRight = !_rotatingRight;

        _angleDifference -= angle;
    }
    
}
