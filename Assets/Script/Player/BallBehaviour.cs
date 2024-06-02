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
    
    private Coroutine moveCoroutine;
    private Vector3 _moveDirection;
    private Rigidbody2D _rb;
    private bool _isMove = false;

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
        Vector3 direction = (_arrowTransform.position - transform.position).normalized;
        _rb.velocity = _moveDirection * _moveSpeed * Time.deltaTime;
        }
    }

    private void MoveToPointer()
    {
        // Направление движения в сторону стрелки
        Vector2 direction = _arrowTransform.up; // Вектор up стрелки указывает направление

       

        // Проверяем наличие препятствий на пути
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, _moveSpeed * Time.deltaTime, _obstacleLayer);
        if (hit.collider != null)
        {
            // Если обнаружено препятствие, перемещаем объект к точке столкновения
            _rb.MovePosition(hit.point);
            _isMove = false;
            // Выводим информацию о столкновении в консоль
            Debug.Log("Столкновение с: " + hit.collider.name + " в точке: " + hit.point);
        }

        _isMove = true;
        // Вычисляем новую позицию объекта
        Vector2 newPosition = (Vector2)transform.position + direction * _moveSpeed * Time.deltaTime;

        // Перемещаем объект
        _rb.MovePosition(newPosition);
    }

   
}
