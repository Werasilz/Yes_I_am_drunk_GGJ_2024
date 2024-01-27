using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _enemyAmount = 20;
    [SerializeField] private float _distance = 10f;
    [SerializeField] private Transform[] _spawnPoints;

    void Start()
    {
        for (int i = 0; i < _enemyAmount; i++)
        {
            int randomSpawnPoint = Random.Range(0, _spawnPoints.Length);
            Vector3 randomPosition = new Vector3(Random.Range(-_distance, _distance), 0, Random.Range(-_distance, _distance));
            Vector3 finalPosition = _spawnPoints[randomSpawnPoint].position + randomPosition;
            GameObject enemy = Instantiate(_enemyPrefab, finalPosition, Quaternion.identity);
        }
    }
}
