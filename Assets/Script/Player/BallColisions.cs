using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]

public class BallColisions : MonoBehaviour
{
    [SerializeField] private Transform _arrowTransform;
    [SerializeField]private float _moveSpeed = 15f;
    
    private Vector3 _moveDirection;
    private bool _isMove = false;
    private Rigidbody2D _rb;
    private ArrowRotation _arrowRotationScript;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _arrowRotationScript = _arrowTransform.GetComponent<ArrowRotation>();
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
            _rb.velocity = _moveDirection * _moveSpeed;
        }
        else
        {
            _isMove = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if( collision.gameObject.layer == LayerMask.NameToLayer("Barrier"))
        {
            _rb.velocity = Vector2.zero;
            _isMove = false;
            
            Vector2 collisionPoint = collision.GetContact(0).point;
            _arrowRotationScript.OnBallCollision(collisionPoint);
        }
    }
}
