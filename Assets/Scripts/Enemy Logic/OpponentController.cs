using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentController : MonoBehaviour
{
    public event Action OnBattleEnded;
    [SerializeField] private List<Transform> _spawnPoints = new List<Transform>();
    private Queue<Transform> _spawnPointsQueue = new Queue<Transform>();
    [SerializeField] private List<BattleInfo> _battleInfos = new List<BattleInfo>();
    private Queue<BattleInfo> _battleInfosQueue = new Queue<BattleInfo>();

    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private float _enemySpeed;
    [SerializeField] private float _enemyAttackDistance;
    [SerializeField] private Transform _enemySpawnTransform;
    [SerializeField] private Transform _playerTransform;

    // TODO Subscribe to Battle Change and follow what ID does Battle has.
    // TODO Manage enemy death, spawn
    // TODO spawn enemies from spawn points on each battle if the place available

    // (other script)TODO have poeple that will just run away
    private Transform _currentSpawnPoint;
    private BattleInfo _currentBattle;

    private int _enemiesSpawned = 0;
    [SerializeField] private int _maxEnemiesSpawned =3;

    private void Awake()
    {
        foreach (Transform t in _spawnPoints)
        {
            _spawnPointsQueue.Enqueue(t);
        }
        foreach (BattleInfo info in _battleInfos)
        {
            _battleInfosQueue.Enqueue(info);
        }
        ChangeBattle(0);
        while (IsSpawnAvailable())
        {
            SpawnEnemy();
        }
    }

    private void ChangeBattle(int battleID) // Moves to the next battle and spawn logic suitable for the next battle 
    {
        _currentSpawnPoint = _spawnPointsQueue.Dequeue();
        _currentBattle = _battleInfosQueue.Dequeue();
    }

    private void SpawnEnemy()
    {
        Enemy newEnemy = Instantiate(_enemyPrefab, _enemySpawnTransform);
        newEnemy.transform.position = _currentSpawnPoint.position;
        newEnemy.Initialize(_playerTransform, _enemySpeed, _enemyAttackDistance);
        newEnemy.OnDied += HandleEnemyDeath;
        _enemiesSpawned++;
        _currentBattle.amountOfEnemies--;
    }

    /// <summary>
    /// Tells whether or not spawn is available, depending on the current number of spawned enemies and the total number of available enemies in the battle.
    /// </summary>
    /// <returns></returns>
    private bool IsSpawnAvailable()
    {
        if (_enemiesSpawned < _maxEnemiesSpawned && _currentBattle.amountOfEnemies > 0)
            return true;
        else
            return false;
    }

    private void HandleEnemyDeath(Enemy enemy)
    {
        enemy.OnDied -= HandleEnemyDeath;
        enemy.DestroyEnemy();
        _enemiesSpawned--;

        if (IsSpawnAvailable())
        {
            SpawnEnemy();
        }
        else
        {
            if(_enemiesSpawned == 0)
            {
                OnBattleEnded?.Invoke();
                Debug.Log("The battle is ended");
            }
        }
    }
}

/// Comments 
        // On enemy died Check if spawn available and if yes spawn. If all enemies are ended -- invoke OnBatle Ended
