using System;
using System.Collections.Generic;
using UnityEngine;

public partial class OpponentController : MonoBehaviour
{
    public event Action OnBattleEnded;
    [SerializeField] private List<ZonePoints> _spawnPoints = new List<ZonePoints>();
    private Queue<List<Transform>> _spawnPointsQueue = new Queue<List<Transform>>();
    private List<Transform> _currentSpawnPoints = new List<Transform>();
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

    private int currentBattleID = 0;

    private void Awake()
    {
        foreach (ZonePoints point in _spawnPoints)
        {
            _spawnPointsQueue.Enqueue(point.spawnPoints);
        }
        foreach (BattleInfo info in _battleInfos)
        {
            _battleInfosQueue.Enqueue(info);
        }
        ChangeBattle();
        SpawnEnemies();

        OnBattleEnded += ChangeBattle;
    }

    private void OnEnable()
    {
        EnemyHealth.OnDied += HandleEnemyDeath;
        CameraConfinerController.OnPlayerReachedNewBattle += SpawnEnemies;
    }

    private void OnDisable()
    {
        EnemyHealth.OnDied -= HandleEnemyDeath;
        CameraConfinerController.OnPlayerReachedNewBattle -= SpawnEnemies;
    }

    private void ChangeBattle() // Moves to the next battle and spawn logic suitable for the next battle 
    {
        if(_spawnPointsQueue.Count == 0 || _battleInfosQueue.Count == 0)
        {
            LevelManager.Instance.ShowWinScreen();
            return;
        }

        _currentSpawnPoints = _spawnPointsQueue.Dequeue();
        _currentBattle = _battleInfosQueue.Dequeue();
        currentBattleID++;
    }

    private void SpawnEnemies()
    {
        while (IsSpawnAvailable())
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Enemy newEnemy = Instantiate(_enemyPrefab, _enemySpawnTransform);
        newEnemy.transform.position = _currentSpawnPoints[UnityEngine.Random.Range(0, _currentSpawnPoints.Count)].position;
        newEnemy.Initialize(_playerTransform, _enemySpeed, _enemyAttackDistance);
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

    private void HandleEnemyDeath()
    {
        _enemiesSpawned--;

        Debug.Log("New enemy should be spawn now");

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
