using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]

public class BallBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _arrowTransform;
    [SerializeField] public LayerMask _obstacleLayer;
    [SerializeField]private float _moveSpeed = 15f;
    [SerializeField] private ArrowRotation _arrowRotation;
    
    
    private float _rayDistance = 0.1f;
    private Vector3 _moveDirection;
    private Rigidbody2D _rb;
    private bool _isMove = false;
    private readonly float[] _angles = { 0f, 15f, -15f, 30f, -30f, 60f, -60f };

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isMove = true;
            _moveDirection = (_arrowTransform.position - transform.position).normalized;
        }
        MoveBall();
        
        
    }
   

    private void MoveBall()
    {
        if(_isMove == true)
        {
            foreach (float angle in _angles)
            {
                if (CheckCollision(transform.position, RotateVector(_moveDirection, angle)))
                {
                    _rb.velocity = Vector2.zero;
                    _isMove = false;
                    //_arrowRotation.OnCollision(_moveDirection);
                    return;
                }
            }
            _rb.velocity = _moveDirection * _moveSpeed;
            
        }
        
    }
    
    private bool CheckCollision(Vector2 origin, Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, _rayDistance, _obstacleLayer);
        Debug.DrawRay(origin, direction * _rayDistance, Color.red);
        return hit.collider != null;
    }
    private Vector2 RotateVector(Vector2 vector, float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        float cos = Mathf.Cos(radian);
        float sin = Mathf.Sin(radian);
        return new Vector2(vector.x * cos - vector.y * sin, vector.x * sin + vector.y * cos);
    }
    
   
}
