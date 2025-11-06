using System;
using System.Linq;
using UnityEngine;
using Random=UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _spawnInterval = 1f;
    [SerializeField] private GameObject _enemyPrefab;

    private float _timer = 0f;
    private int _spawnPointIdx = 0;

    private void Start()
    {
       

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
    }
    private void SetSpwanInterval()
    {
        float randomInterval = Random.Range(1f, 3f);
        _spawnInterval = randomInterval;
    }
    private void SetSpwanPointIdx()
    {
        _spawnPointIdx = Random.Range(0, _spawnPoints.Length);
      
    }

    private void SpawnEnemy()
    {
        Instantiate(_enemyPrefab, _spawnPoints[_spawnPointIdx].position, Quaternion.identity);

    }
}
