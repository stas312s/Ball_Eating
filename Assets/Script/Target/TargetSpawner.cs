using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField] private Target _targetPrefab;
    [SerializeField] private Transform _topLeftCorner;
    [SerializeField] private Transform _botRightCorner;
    [SerializeField] private float offset = 0.5f;
    [SerializeField] private float _minSpawnDistance = 0.5f;
    
    private float _xMin;
    private float _xMax;
    private float _yMin;
    private float _yMax;

    private Vector3 _lastSpawnPosition;
   

    void Start()
    {
        _xMin = _topLeftCorner.position.x + offset;
        _xMax = _botRightCorner.position.x - offset;
        _yMin = _botRightCorner.position.y + offset;
        _yMax = _topLeftCorner.position.y - offset;
    
        SpawnTarget();
    }

    void SpawnTarget()
    {
        Vector3 spawnPosition;
        do
        {
            float randomX = Random.Range(_xMin, _xMax);
            float randomY = Random.Range(_yMin, _yMax);
            spawnPosition = new Vector3(randomX, randomY, 0f);
        } while (Vector3.Distance(spawnPosition, _lastSpawnPosition) < _minSpawnDistance);
        
        Instantiate(_targetPrefab, spawnPosition, Quaternion.identity).Initialize(RespawnTarget);
        _lastSpawnPosition = spawnPosition;
    }

    private void RespawnTarget(Target target)
    {
        Destroy(target.gameObject);
        SpawnTarget();
    }
}
