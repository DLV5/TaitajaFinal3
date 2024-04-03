using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public event Action MoveToTheNextBattle;

    public static LevelManager Instance { get; private set; }

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

    private void Start()
    {
        _controller.OnBattleEnded += ChangeBattle;
    }

    private void ChangeBattle() => MoveToTheNextBattle?.Invoke();
}
