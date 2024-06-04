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
    private bool _isCollision = false;
    
    void Update()
    {
        Rotation();
    }

    private void Rotation()
    {
        if (_isCollision)
            return;

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

    public void OnBallCollision(Vector2 collisionPoint)
    {
        
        Vector3 directionToCollision = (Vector3)collisionPoint - _targetObject.position;
        Vector3 newOffset = directionToCollision.normalized * -_maxDistance;
        transform.position = _targetObject.position + newOffset;
        transform.up = transform.position - _targetObject.position;

        
        _isCollision = true;

        StartCoroutine(ResetCollisionFlag());
    }

    private IEnumerator ResetCollisionFlag()
    {
        yield return new WaitForSeconds(0.1f); 
        _isCollision = false;
    }
    
}
