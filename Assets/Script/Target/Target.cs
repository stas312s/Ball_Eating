using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Action<Target> _onDestroy;
    
    
    public void Initialize(Action<Target> onDestroy)
    {
        _onDestroy = onDestroy;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<BallBehaviour>(out var ball))
        {
            _onDestroy?.Invoke(this);
        }
    }
}
