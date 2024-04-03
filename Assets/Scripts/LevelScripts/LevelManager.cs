using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public event Action MoveToTheNextBattle;

    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _looseScreen;

    private OpponentController _controller;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        _controller = GameObject.Find("Opponent Controller").GetComponent<OpponentController>();
    }

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDied += ShowLooseScreen;
    }

    private void Start()
    {
        _controller.OnBattleEnded += ChangeBattle;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDied -= ShowLooseScreen;
    }

    public void ShowWinScreen() => _winScreen.SetActive(true);
    private void ShowLooseScreen() => _winScreen.SetActive(true);

    private void ChangeBattle() => MoveToTheNextBattle?.Invoke();
}
