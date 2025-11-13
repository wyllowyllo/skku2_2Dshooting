using System;
using System.Linq;
using UnityEngine;
using Random=UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [Header("스폰 포인트")]
    [SerializeField] private Transform[] _spawnPoints;

    [Header("스폰 간격")]
    [SerializeField] private float _minSpawnInterval = 1f;
    [SerializeField] private float _maxSpawnInterval = 3f;
    private float _spawnInterval = 1f;

    [Header("스폰 적 프리펩")]
    [SerializeField] private GameObject[] _enemyPrefab;

    [Header("타입별 스폰 확률")]
    [SerializeField] private float _straightEnemySpawnRate = 0.7f;
    [SerializeField] private float _traceEnemySpawnRate = 0.3f;

    private EnemyFactory _enemyFactory;
    private float _timer = 0f;
    private int _spawnPointIdx = 0;
    private EEnemyType _spawnEnemyType;

    private void Start()
    {
        _enemyFactory = EnemyFactory.Instance;
        SetSpwanInterval();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if(_timer >= _spawnInterval)
        {
            SpawnEnemy();
            RandomlySpawnSet();
            
            _timer = 0f;
        }

    }

    private void RandomlySpawnSet()
    {
        SetSpwanPointIdx();
        SetSpwanInterval();
        SetSpawnEnemyIdx();
    }
    private void SetSpwanInterval()
    {
        float randomInterval = Random.Range(_minSpawnInterval, _maxSpawnInterval);
        _spawnInterval = randomInterval;
    }
    private void SetSpwanPointIdx()
    {
        _spawnPointIdx = Random.Range(0, _spawnPoints.Length);
      
    }
    private void SetSpawnEnemyIdx()
    {
        float spawnRate = Random.Range(0f, 1f);

        if(spawnRate <= _straightEnemySpawnRate)
            _spawnEnemyType = EEnemyType.Straight;
        else
            _spawnEnemyType = EEnemyType.Trace;
        
    }

    private void SpawnEnemy()
    {
        _enemyFactory.MakeEnemy(_spawnEnemyType, _spawnPoints[_spawnPointIdx].position);
    }
}
