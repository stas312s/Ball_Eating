using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowVisibilty : MonoBehaviour
{
    [SerializeField] private GameObject _arrow;

    private Rigidbody2D _rigidbody;
    private Vector3 _lastPosition;
    private bool _isMoving;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _lastPosition = transform.position;
    }

    void Update()
    {
        Visiblity();
    }

    private void Visiblity()
    {
        
        if (_rigidbody.velocity.magnitude > 0.1f)
        {
            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }
        
        _arrow.SetActive(!_isMoving);
        _lastPosition = transform.position;
    }
}
